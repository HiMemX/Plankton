using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PluginApi;

namespace Plankton.PluginApiImplementation;

internal sealed class UIService : IUIService
{
    private readonly CommandService commands;

    private readonly Dictionary<string, List<ToolStripLocation>>
        toolStripLocations = new(StringComparer.Ordinal);

    private readonly Dictionary<string, List<ControlLocation>>
        controlLocations = new(StringComparer.Ordinal);

    private readonly List<ToolStripContribution>
        toolStripContributions = new();

    private readonly List<ControlContribution>
        controlContributions = new();

    private long nextSequence;


    public UIService(CommandService commands)
    {
        this.commands = commands;

        commands.CommandRegistered += OnCommandRegistered;
    }


    // ============================================================
    // REGISTER EXTENSION POINTS
    // ============================================================

    public IDisposable RegisterToolStripLocation(
        string locationId,
        ToolStripLocationKind kind,
        ToolStripItemCollection items)
    {
        if (string.IsNullOrWhiteSpace(locationId)) { throw new ArgumentException("locationId cannot be null or whitespace."); }
        ArgumentNullException.ThrowIfNull(items);

        if (controlLocations.ContainsKey(locationId))
        {
            throw new InvalidOperationException(
                $"'{locationId}' is already being used as a control location.");
        }

        var location = new ToolStripLocation(
            locationId,
            kind,
            items);

        if (!toolStripLocations.TryGetValue(
            locationId,
            out var instances))
        {
            instances = new List<ToolStripLocation>();
            toolStripLocations.Add(locationId, instances);
        }
        else if (instances.Count != 0 &&
                 instances[0].Kind != kind)
        {
            throw new InvalidOperationException(
                $"ToolStrip location '{locationId}' was registered " +
                $"as both {instances[0].Kind} and {kind}.");
        }

        instances.Add(location);

        ResolveToolStripLocation(location);

        return new DisposableAction(() =>
        {
            RemoveToolStripLocation(location);
        });
    }


    public IDisposable RegisterControlLocation(
        string locationId,
        Control container)
    {
        if (string.IsNullOrWhiteSpace(locationId)) { throw new ArgumentException("locationId cannot be null or whitespace."); }
        ArgumentNullException.ThrowIfNull(container);

        if (toolStripLocations.ContainsKey(locationId))
        {
            throw new InvalidOperationException(
                $"'{locationId}' is already being used as a ToolStrip location.");
        }

        var location = new ControlLocation(
            locationId,
            container);

        if (!controlLocations.TryGetValue(
            locationId,
            out var instances))
        {
            instances = new List<ControlLocation>();
            controlLocations.Add(locationId, instances);
        }

        instances.Add(location);

        ResolveControlLocation(location);

        return new DisposableAction(() =>
        {
            RemoveControlLocation(location);
        });
    }


    // ============================================================
    // REGISTER TOOLSTRIP CONTRIBUTIONS
    // ============================================================

    public void AddCommand(
        string locationId,
        string commandId,
        int order = 0)
    {
        if (string.IsNullOrWhiteSpace(locationId)) { throw new ArgumentException("locationId cannot be null or whitespace."); }
        if (string.IsNullOrWhiteSpace(commandId)) { throw new ArgumentException("commandId cannot be null or whitespace."); }

        var contribution = new CommandContribution(
            locationId,
            commandId,
            order,
            nextSequence++);

        toolStripContributions.Add(contribution);

        ResolveContribution(contribution);
    }


    public void AddSeparator(
        string locationId,
        int order = 0)
    {
        if (string.IsNullOrWhiteSpace(locationId))
            throw new ArgumentException(
                "Location ID cannot be null or whitespace.",
                nameof(locationId));

        var contribution = new SeparatorContribution(
            locationId,
            order,
            nextSequence++);

        toolStripContributions.Add(contribution);

        ResolveContribution(contribution);
    }


    public void AddDropDown(
        string parentLocationId,
        string childLocationId,
        string text,
        int order = 0)
    {
        if (string.IsNullOrWhiteSpace(parentLocationId)) { throw new ArgumentException("ParentLocationID cannot be null or whitespace."); }
        if (string.IsNullOrWhiteSpace(childLocationId)) { throw new ArgumentException("childLocationId cannot be null or whitespace."); }
        if (string.IsNullOrWhiteSpace(text)) { throw new ArgumentException("text cannot be null or whitespace."); }

        var contribution = new DropDownContribution(
            parentLocationId,
            childLocationId,
            text,
            order,
            nextSequence++);

        toolStripContributions.Add(contribution);

        ResolveContribution(contribution);
    }


    // ============================================================
    // REGISTER CONTROL CONTRIBUTIONS
    // ============================================================

    public void AddControl(
        string locationId,
        Func<Control> factory,
        int order = 0)
    {
        if (string.IsNullOrWhiteSpace(locationId)) { throw new ArgumentException("locationId cannot be null or whitespace."); }
        ArgumentNullException.ThrowIfNull(factory);

        var contribution = new ControlContribution(
            locationId,
            factory,
            order,
            nextSequence++);

        controlContributions.Add(contribution);

        ResolveContribution(contribution);
    }


    // ============================================================
    // RESOLUTION
    // ============================================================

    private void ResolveToolStripLocation(
        ToolStripLocation location)
    {
        foreach (var contribution in toolStripContributions
                     .Where(x => x.LocationId == location.Id)
                     .OrderBy(x => x.Order)
                     .ThenBy(x => x.Sequence))
        {
            Materialize(contribution, location);
        }
    }


    private void ResolveControlLocation(
        ControlLocation location)
    {
        foreach (var contribution in controlContributions
                     .Where(x => x.LocationId == location.Id)
                     .OrderBy(x => x.Order)
                     .ThenBy(x => x.Sequence))
        {
            Materialize(contribution, location);
        }
    }


    private void ResolveContribution(
        ToolStripContribution contribution)
    {
        if (!toolStripLocations.TryGetValue(
            contribution.LocationId,
            out var locations))
        {
            // Extension point doesn't exist yet.
            // Keep contribution pending.
            return;
        }

        foreach (var location in locations)
            Materialize(contribution, location);
    }


    private void ResolveContribution(
        ControlContribution contribution)
    {
        if (!controlLocations.TryGetValue(
            contribution.LocationId,
            out var locations))
        {
            return;
        }

        foreach (var location in locations)
            Materialize(contribution, location);
    }


    // ============================================================
    // MATERIALIZE TOOLSTRIP CONTRIBUTIONS
    // ============================================================

    private void Materialize(
        ToolStripContribution contribution,
        ToolStripLocation location)
    {
        if (contribution.Materialized.ContainsKey(
            location.InstanceId))
        {
            return;
        }

        ToolStripMaterialization? materialization =
            contribution switch
            {
                CommandContribution command =>
                    CreateCommandItem(command, location),

                SeparatorContribution =>
                    new ToolStripMaterialization(
                        new ToolStripSeparator()),

                DropDownContribution dropdown =>
                    CreateDropDown(dropdown, location),

                _ => throw new NotSupportedException()
            };

        // For example: command hasn't been registered yet.
        if (materialization == null)
            return;

        InsertToolStripItem(
            location,
            contribution,
            materialization.Item);

        contribution.Materialized.Add(
            location.InstanceId,
            materialization);
    }


    private ToolStripMaterialization? CreateCommandItem(
        CommandContribution contribution,
        ToolStripLocation location)
    {
        if (!commands.TryGet(
            contribution.CommandId,
            out var command))
        {
            // Command can be registered later.
            return null;
        }

        ToolStripItem item;

        if (location.Kind == ToolStripLocationKind.Menu)
        {
            item = new ToolStripMenuItem
            {
                Text = command.Name
            };
        }
        else
        {
            item = new ToolStripButton
            {
                Text = command.Name,
                DisplayStyle = ToolStripItemDisplayStyle.Text
            };
        }

        item.Enabled =
            commands.CanExecute(command.Id);

        item.Click += (_, _) =>
        {
            if (commands.CanExecute(command.Id))
                commands.Execute(command.Id);
        };

        return new ToolStripMaterialization(item);
    }


    private ToolStripMaterialization CreateDropDown(
        DropDownContribution contribution,
        ToolStripLocation location)
    {
        ToolStripDropDownItem item;

        if (location.Kind == ToolStripLocationKind.Menu)
        {
            item = new ToolStripMenuItem(
                contribution.Text);
        }
        else
        {
            item = new ToolStripDropDownButton(
                contribution.Text);
        }

        // Dropdown contents are themselves another menu extension point.
        IDisposable childLocationRegistration =
            RegisterToolStripLocation(
                contribution.ChildLocationId,
                ToolStripLocationKind.Menu,
                item.DropDownItems);

        return new ToolStripMaterialization(
            item,
            childLocationRegistration);
    }


    // ============================================================
    // MATERIALIZE CONTROL CONTRIBUTIONS
    // ============================================================

    private void Materialize(
        ControlContribution contribution,
        ControlLocation location)
    {
        if (contribution.Materialized.ContainsKey(
            location.InstanceId))
        {
            return;
        }

        Control control = contribution.Factory();

        if (control == null)
        {
            throw new InvalidOperationException(
                $"Control factory for '{contribution.LocationId}' " +
                "returned null.");
        }

        location.Container.Controls.Add(control);

        contribution.Materialized.Add(
            location.InstanceId,
            control);

        ReorderControls(location);
    }


    // ============================================================
    // ORDERING
    // ============================================================

    private void InsertToolStripItem(
        ToolStripLocation location,
        ToolStripContribution contribution,
        ToolStripItem item)
    {
        int contributedItemsBefore =
            toolStripContributions.Count(x =>
                x.LocationId == contribution.LocationId &&
                IsBefore(x, contribution) &&
                x.Materialized.ContainsKey(location.InstanceId));

        int index =
            location.BaseItemCount +
            contributedItemsBefore;

        index = Math.Min(
            index,
            location.Items.Count);

        location.Items.Insert(index, item);
    }


    private void ReorderControls(
        ControlLocation location)
    {
        var materialized =
            controlContributions
                .Where(x =>
                    x.LocationId == location.Id &&
                    x.Materialized.ContainsKey(location.InstanceId))
                .OrderBy(x => x.Order)
                .ThenBy(x => x.Sequence)
                .ToList();

        for (int i = 0; i < materialized.Count; i++)
        {
            Control control =
                materialized[i].Materialized[
                    location.InstanceId];

            int targetIndex =
                location.BaseControlCount + i;

            targetIndex = Math.Min(
                targetIndex,
                location.Container.Controls.Count - 1);

            location.Container.Controls.SetChildIndex(
                control,
                targetIndex);
        }
    }


    private static bool IsBefore(
        Contribution a,
        Contribution b)
    {
        if (a.Order != b.Order)
            return a.Order < b.Order;

        return a.Sequence < b.Sequence;
    }


    // ============================================================
    // COMMAND WAS REGISTERED AFTER UI CONTRIBUTION
    // ============================================================

    private void OnCommandRegistered(string commandId)
    {
        foreach (var contribution in toolStripContributions
                     .OfType<CommandContribution>()
                     .Where(x => x.CommandId == commandId))
        {
            ResolveContribution(contribution);
        }
    }


    // ============================================================
    // REMOVE LOCATIONS
    // ============================================================

    private void RemoveToolStripLocation(
        ToolStripLocation location)
    {
        foreach (var contribution in toolStripContributions)
        {
            if (!contribution.Materialized.Remove(
                location.InstanceId,
                out var materialization))
            {
                continue;
            }

            materialization.Dispose();

            location.Items.Remove(
                materialization.Item);

            materialization.Item.Dispose();
        }

        if (!toolStripLocations.TryGetValue(
            location.Id,
            out var locations))
        {
            return;
        }

        locations.Remove(location);

        if (locations.Count == 0)
            toolStripLocations.Remove(location.Id);
    }


    private void RemoveControlLocation(
        ControlLocation location)
    {
        foreach (var contribution in controlContributions)
        {
            if (!contribution.Materialized.Remove(
                location.InstanceId,
                out var control))
            {
                continue;
            }

            location.Container.Controls.Remove(control);

            control.Dispose();
        }

        if (!controlLocations.TryGetValue(
            location.Id,
            out var locations))
        {
            return;
        }

        locations.Remove(location);

        if (locations.Count == 0)
            controlLocations.Remove(location.Id);
    }

    internal sealed class DisposableAction : IDisposable
    {
        private Action? action;

        public DisposableAction(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            action?.Invoke();
            action = null;
        }
    }

    // ============================================================
    // INTERNAL TYPES
    // ============================================================

    private abstract class Contribution
    {
        public string LocationId { get; }
        public int Order { get; }
        public long Sequence { get; }

        protected Contribution(
            string locationId,
            int order,
            long sequence)
        {
            LocationId = locationId;
            Order = order;
            Sequence = sequence;
        }
    }


    private abstract class ToolStripContribution
        : Contribution
    {
        public Dictionary<Guid, ToolStripMaterialization>
            Materialized
        { get; } = new();

        protected ToolStripContribution(
            string locationId,
            int order,
            long sequence)
            : base(locationId, order, sequence)
        {
        }
    }


    private sealed class CommandContribution
        : ToolStripContribution
    {
        public string CommandId { get; }

        public CommandContribution(
            string locationId,
            string commandId,
            int order,
            long sequence)
            : base(locationId, order, sequence)
        {
            CommandId = commandId;
        }
    }


    private sealed class SeparatorContribution
        : ToolStripContribution
    {
        public SeparatorContribution(
            string locationId,
            int order,
            long sequence)
            : base(locationId, order, sequence)
        {
        }
    }


    private sealed class DropDownContribution
        : ToolStripContribution
    {
        public string ChildLocationId { get; }
        public string Text { get; }

        public DropDownContribution(
            string parentLocationId,
            string childLocationId,
            string text,
            int order,
            long sequence)
            : base(parentLocationId, order, sequence)
        {
            ChildLocationId = childLocationId;
            Text = text;
        }
    }


    private sealed class ControlContribution
        : Contribution
    {
        public Func<Control> Factory { get; }

        public Dictionary<Guid, Control>
            Materialized
        { get; } = new();

        public ControlContribution(
            string locationId,
            Func<Control> factory,
            int order,
            long sequence)
            : base(locationId, order, sequence)
        {
            Factory = factory;
        }
    }


    private sealed class ToolStripLocation
    {
        public Guid InstanceId { get; } =
            Guid.NewGuid();

        public string Id { get; }
        public ToolStripLocationKind Kind { get; }

        public ToolStripItemCollection Items { get; }

        public int BaseItemCount { get; }

        public ToolStripLocation(
            string id,
            ToolStripLocationKind kind,
            ToolStripItemCollection items)
        {
            Id = id;
            Kind = kind;
            Items = items;

            BaseItemCount = items.Count;
        }
    }


    private sealed class ControlLocation
    {
        public Guid InstanceId { get; } =
            Guid.NewGuid();

        public string Id { get; }
        public Control Container { get; }

        public int BaseControlCount { get; }

        public ControlLocation(
            string id,
            Control container)
        {
            Id = id;
            Container = container;

            BaseControlCount =
                container.Controls.Count;
        }
    }


    private sealed class ToolStripMaterialization
        : IDisposable
    {
        public ToolStripItem Item { get; }

        private readonly IDisposable?
            childLocationRegistration;

        public ToolStripMaterialization(
            ToolStripItem item,
            IDisposable? childLocationRegistration = null)
        {
            Item = item;
            this.childLocationRegistration =
                childLocationRegistration;
        }

        public void Dispose()
        {
            childLocationRegistration?.Dispose();
        }
    }
}