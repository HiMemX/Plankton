using System.Reflection.Metadata;
using System.Windows.Forms;
using PluginApi;

namespace ConsolePlugin
{
    public sealed class ConsolePlugin : IPlugin
    {
        private ConsoleProvider provider = null!;
        public string Id => "plankton.console-plugin";
        public string Name => "Console";
        public string Description => "Gives the user a Console Window that they can execute commands in as well as look at debug messages.";


        public void Initialize(IHost host)
        {
            provider = new ConsoleProvider(host);
            host.Editors.RegisterProvider(provider);

            host.Commands.Register(new CommandDefinition(
                "plankton.console.open",
                "Open Console...",
                _ => { if (provider.instance == null) host.Editors.Open(provider); }
                ));

            host.Commands.Register(new CommandDefinition(
                "plankton.console.add-entry",
                "Add Entry to Console",
                AddNormalEntryToConsole,
                CanExecute

                )
            );
            host.Commands.Register(new CommandDefinition(
                "plankton.console.add-error",
                "Add Error Entry to Console",
                AddErrorEntryToConsole,
                CanExecute
                )
            );
            host.Commands.Register(new CommandDefinition(
                "plankton.console.add-warning",
                "Add Warning Entry to Console",
                AddWarningEntryToConsole,
                CanExecute
                )
            );
            host.Commands.Register(new CommandDefinition(
                "plankton.console.add-success",
                "Add Success Entry to Console",
                AddSuccessEntryToConsole,
                CanExecute
                )
            );

            host.UI.AddCommand("plankton.main.view", "plankton.console.open");

        }
        
        private bool CanExecute(object? parameter)
        {

            return parameter is (string sender, string message);
        }

        private void AddWarningEntryToConsole(object? parameter)
        {
            AddEntryToConsole(parameter, DebugEntryType.WARNING);
        }

        private void AddSuccessEntryToConsole(object? parameter)
        {
            AddEntryToConsole(parameter, DebugEntryType.SUCCESS);
        }

        private void AddErrorEntryToConsole(object? parameter)
        {
            AddEntryToConsole(parameter, DebugEntryType.ERROR);
        }

        private void AddNormalEntryToConsole(object? parameter)
        {
            AddEntryToConsole(parameter);
        }

        private void AddEntryToConsole(object? parameter, DebugEntryType type = DebugEntryType.NORMAL)
        {
            if(parameter is not (string sender, string message)) return;

            provider.AddEntry(sender, message, type);
        }

    }
}
