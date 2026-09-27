using System.Reflection;
using System.Runtime.Loader;

namespace Plankton.Core.Plugins;

internal sealed class PluginLoadContext : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver resolver;

    private static readonly HashSet<string> SharedAssemblies =
    [
        typeof(PluginApi.IPlugin).Assembly.GetName().Name!,
        typeof(CSHO.Handler).Assembly.GetName().Name!
    ];

    public PluginLoadContext(string pluginAssemblyPath)
        : base(isCollectible: false)
    {
        resolver = new AssemblyDependencyResolver(pluginAssemblyPath);
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        // These assemblies define objects shared between Plankton
        // and the plugin. Always use the copies already loaded by
        // the host.
        if (assemblyName.Name != null &&
            SharedAssemblies.Contains(assemblyName.Name))
        {
            return null;
        }

        string? path =
            resolver.ResolveAssemblyToPath(assemblyName);

        if (path != null)
            return LoadFromAssemblyPath(path);

        // Let the default context / framework resolve it.
        return null;
    }

    protected override nint LoadUnmanagedDll(
        string unmanagedDllName)
    {
        string? path =
            resolver.ResolveUnmanagedDllToPath(
                unmanagedDllName);

        if (path != null)
            return LoadUnmanagedDllFromPath(path);

        return nint.Zero;
    }
}