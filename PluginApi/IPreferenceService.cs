using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginApi
{
    public interface IPreferencesService
    {
        T Register<T>(string id, string name)
            where T : class, new();

        IReadOnlyList<PreferenceSection> Sections { get; }

        void Save();
    }

    public sealed class PreferenceSection
    {
        public string Id { get; }
        public string Name { get; }
        public object Value { get; }

        public PreferenceSection(
            string id,
            string name,
            object value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
    }
}
