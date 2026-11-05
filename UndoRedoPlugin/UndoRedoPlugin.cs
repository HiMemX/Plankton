using PluginApi;

namespace UndoRedoPlugin
{
    public class UndoRedoPlugin : IPlugin
    {
        private IHost host;

        public string Id => "plankton.undo-redo-plugin";
        public string Name => "Undo Redo Plugin";
        public string Description => "Exposes commands to undo and redo actions. Stores sent modified, created, destroyed events (not preview events).";

        private List<ReversibleAction> actions = new();

        private int index = 0; // Starts from the back, 0 means end of list, 1 means second last element of list

        public void Initialize(IHost host)
        {
            this.host = host;

            host.Commands.Register(new CommandDefinition(
                "plankton.undo-action",
                "Undo Action",
                UndoLastAction
                ));

            host.Commands.Register(new CommandDefinition(
                "plankton.redo-action",
                "Redo Action",
                RedoLastAction
                ));

            host.UI.AddCommand("plankton.main.edit", "plankton.undo-action");
            host.UI.AddCommand("plankton.main.edit", "plankton.redo-action");

            host.Archive.AssetsModified += AssetsModified;

            //host.UI.AddCommand("plankton.main.view", "plankton.hex-editor.open");

        }

        private void NotifyAssetsModified(IReadOnlyList<AssetInfo> infos)
        {
            host.Archive.NotifyAssetsModified(this, infos);
        }

        public void AssetsModified(object? sender, AssetEventArgs e)
        {
            if (sender == this) return;


            ExpandActionList();
            actions.Add(new AssetsModifiedAction(e.Assets));

            host.Commands.Execute("plankton.console.add-entry", ("AssetsModified: " + sender.ToString(), "New action! " + actions.Count.ToString()));
        }

        private void ExpandActionList()
        {
            List<ReversibleAction> actionstoadd = new();
            for(int i=0; i<index; i++)
            {
                actionstoadd.Add(GetAction(i).ReversedAction());
            }
            actions.AddRange(actionstoadd);
            index = 0;
        }

        public void UndoLastAction(object? p)
        {
            if (actions.Count <= index) return;

            ReversibleAction act = GetUndoAction();
            act.UndoAction();


            index += 1;

            NotifyAssetsModified(act.assetInfos);

            host.Commands.Execute("plankton.console.add-entry", ("UndoLastAction", "Undid action! " + actions.Count.ToString()));
        }

        public void RedoLastAction(object? p)
        {
            if (index == 0) return;

            ReversibleAction act = GetRedoAction();
            act.DoAction();

            index -= 1;

            NotifyAssetsModified(act.assetInfos);

            host.Commands.Execute("plankton.console.add-entry", ("RedoLastAction", "Redid action! " + actions.Count.ToString()));
        }

        private ReversibleAction GetAction(int idx)
        {
            return actions[actions.Count - idx - 1];
        }

        private ReversibleAction GetUndoAction()
        {
            return actions[actions.Count - index - 1];
        }

        private ReversibleAction GetRedoAction()
        {
            return actions[actions.Count - index];
        }

    }

    internal class AssetsModifiedAction : ReversibleAction
    {

        public ActionType type => ActionType.AssetsModified;

        private bool reversed;

        public AssetsModifiedAction(IReadOnlyList<AssetInfo> assetInfos, bool reversed=false)
        {
            this.assetInfos = assetInfos;
            this.reversed = reversed;

        }

        public override AssetsModifiedAction ReversedAction()
        {
            return new AssetsModifiedAction(assetInfos, !reversed);
        }

        public override void DoAction()
        { // Ugly, fix
            if (!reversed)
            {
                foreach (AssetInfo asset in assetInfos)
                {
                    asset.Entry.data = new List<byte>(asset.DataAfter);
                    asset.Entry.CreateEntity();
                }
                return;
            }

            foreach (AssetInfo asset in assetInfos)
            {
                if (asset.DataBefore == null) continue;

                asset.Entry.data = new List<byte>(asset.DataBefore);
                asset.Entry.CreateEntity();
            }
        }

        public override void UndoAction()
        {
            if (reversed)
            {
                foreach (AssetInfo asset in assetInfos)
                {
                    asset.Entry.data = new List<byte>(asset.DataAfter);
                    asset.Entry.CreateEntity();
                }
                return;
            }
            foreach (AssetInfo asset in assetInfos)
            {
                if (asset.DataBefore == null) continue;

                asset.Entry.data = new List<byte>(asset.DataBefore);
                asset.Entry.CreateEntity();
            }
        }
    }

    internal abstract class ReversibleAction
    {
        public IReadOnlyList<AssetInfo> assetInfos;
        public ActionType type { get; }
        public abstract void DoAction();
        public abstract void UndoAction();

        public abstract ReversibleAction ReversedAction();
    }

    internal enum ActionType
    {
        None,
        AssetsModified,
        AssetsCreated,
        AssetsDestroyed
    }
}
