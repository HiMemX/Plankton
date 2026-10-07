using PluginApi;
using System;
using System.Collections.Generic;

namespace Plankton.PluginApiImplementation;

internal sealed class CommandService : ICommandService
{
    private readonly Dictionary<string, CommandDefinition> commands =
        new(StringComparer.Ordinal);

    public IReadOnlyList<CommandDefinition> Commands
        => commands.Values.ToList();


    /// <summary>
    /// Internal implementation event.
    /// UIService uses this to resolve pending UI contributions.
    /// Plugins never see it.
    /// </summary>
    internal event Action<string>? CommandRegistered;


    public void Register(CommandDefinition command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.Id))
            throw new ArgumentException("Command ID cannot be empty.");

        if (commands.ContainsKey(command.Id))
        {
            throw new InvalidOperationException(
                $"Command '{command.Id}' is already registered.");
        }

        commands.Add(command.Id, command);

        CommandRegistered?.Invoke(command.Id);
    }


    public bool CanExecute(
        string commandId,
        object? parameter = null)
    {
        if (!commands.TryGetValue(
            commandId,
            out var command))
        {
            return false;
        }

        return command.CanExecute?.Invoke(parameter) ?? true;
    }


    public void Execute(
        string commandId,
        object? parameter = null)
    {
        if (!commands.TryGetValue(
            commandId,
            out var command))
        {
            throw new InvalidOperationException(
                $"Command '{commandId}' is not registered.");
        }

        if (!(command.CanExecute?.Invoke(parameter) ?? true))
            return;

        command.Execute(parameter);
    }


    internal bool TryGet(
        string commandId,
        out CommandDefinition command)
    {
        return commands.TryGetValue(
            commandId,
            out command!);
    }
}