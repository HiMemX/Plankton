using System.Reflection;
using Plankton.Core;
using PluginApi;

namespace Plankton.Core.Plugins;

internal sealed class PluginLoader
{
    private readonly IHost host;

    private readonly List<IPlugin> plugins = [];
    private readonly List<PluginLoadContext> loadContexts = [];

    public IReadOnlyList<IPlugin> Plugins => plugins;

    public PluginLoader(IHost host)
    {
        this.host = host;
    }

    public void LoadAll(string pluginDirectory)
    {
        Directory.CreateDirectory(pluginDirectory);

        foreach (string directory in
                 Directory.GetDirectories(pluginDirectory))
        {
            LoadDirectory(directory);
        }
    }

    public void LoadAll(List<string> plugins)
    {
        foreach (string directory in plugins)
        {
            try
            {
                LoadPluginAssembly(directory);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    private void LoadDirectory(string directory)
    {
        string name =
            Path.GetFileName(directory);

        string assemblyPath =
            Path.Combine(directory, name + ".dll");

        if (!File.Exists(assemblyPath))
        {
            Console.WriteLine(
                $"Plugin directory '{name}' has no '{name}.dll'.");

            return;
        }


        try
        {
            LoadPluginAssembly(assemblyPath);

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void LoadPluginAssembly(string assemblyPath)
    {
        assemblyPath =
            Path.GetFullPath(assemblyPath);

        var context =
            new PluginLoadContext(assemblyPath);

        Assembly assembly =
            context.LoadFromAssemblyPath(assemblyPath);

        Type[] pluginTypes =
            assembly
                .GetTypes()
                .Where(type =>
                    !type.IsAbstract &&
                    !type.IsInterface &&
                    typeof(IPlugin).IsAssignableFrom(type))
                .ToArray();

        if (pluginTypes.Length == 0)
        {
            throw new InvalidOperationException(
                $"Assembly '{Path.GetFileName(assemblyPath)}' " +
                $"does not contain an IPlugin implementation.");
        }

        foreach (Type type in pluginTypes)
        {
            if (Activator.CreateInstance(type)
                is not IPlugin plugin)
            {
                throw new InvalidOperationException(
                    $"Could not create plugin '{type.FullName}'.");
            }

            if (plugins.Any(
                    p => p.Id.Equals(
                        plugin.Id,
                        StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    $"A plugin with ID '{plugin.Id}' " +
                    $"is already loaded.");
            }

            plugin.Initialize(host);

            plugins.Add(plugin);

            Console.WriteLine(
                $"Loaded plugin: {plugin.Name} ({plugin.Id})");
        }

        // Keep the load context alive for the lifetime of Plankton.
        loadContexts.Add(context);
    }
}