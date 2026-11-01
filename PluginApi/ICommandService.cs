using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginApi
{
    public interface ICommandService
    {
        IReadOnlyList<CommandDefinition> Commands { get; }

        void Register(CommandDefinition command);
        bool CanExecute(
        string commandId,
        object? arguments = null);

        object? Execute(string commandId, object? arguments = null);
    }

    public sealed class CommandDefinition
    {
        public string Id { get; }
        public string Name { get; }

        public Func<object?, object?> Execute { get; }
        public Func<object?, bool>? CanExecute { get; }

        // Existing commands use this.
        public CommandDefinition(
            string id,
            string name,
            Action<object?> execute,
            Func<object?, bool>? canExecute = null)
        {
            Id = id;
            Name = name;
            CanExecute = canExecute;

            Execute = args =>
            {
                execute(args);
                return null;
            };
        }

        // Commands which return something use this.
        public CommandDefinition(
            string id,
            string name,
            Func<object?, object?> execute,
            Func<object?, bool>? canExecute = null)
        {
            Id = id;
            Name = name;
            Execute = execute;
            CanExecute = canExecute;
        }
    }
}
