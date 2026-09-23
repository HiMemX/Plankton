using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginApi
{
    public interface ICommandService
    {
        void Register(CommandDefinition command);

        bool CanExecute(string commandId, object? parameter = null);

        void Execute(string commandId, object? parameter = null);
    }

    public sealed class CommandDefinition
    {
        public string Id { get; }
        public string Name { get; }

        public Action<object?> Execute { get; }
        public Func<object?, bool>? CanExecute { get; }

        public CommandDefinition(
            string id,
            string name,
            Action<object?> execute,
            Func<object?, bool>? canExecute = null)
        {
            Id = id;
            Name = name;
            Execute = execute;
            CanExecute = canExecute;
        }
    }
}
