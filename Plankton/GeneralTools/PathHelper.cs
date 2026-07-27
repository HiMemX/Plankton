using System;
using System.IO;
using System.Windows.Forms;

namespace Plankton.GeneralTools;

// Author: Chatty G.

public static class PluginPathHelper
{
    /// <summary>
    /// Returns a relative path when the selected file is inside the
    /// application directory. Otherwise returns the absolute path.
    /// </summary>
    public static string MakePortable(string selectedPath)
    {
        
        string baseDirectory = Path.GetFullPath(AppContext.BaseDirectory);
        string fullPath = Path.GetFullPath(selectedPath);

        string relativePath = Path.GetRelativePath(baseDirectory, fullPath);

        bool isOutsideBaseDirectory =
            Path.IsPathRooted(relativePath) ||
            relativePath.Equals("..", StringComparison.Ordinal) ||
            relativePath.StartsWith(
                ".." + Path.DirectorySeparatorChar,
                StringComparison.Ordinal) ||
            relativePath.StartsWith(
                ".." + Path.AltDirectorySeparatorChar,
                StringComparison.Ordinal);

        return isOutsideBaseDirectory
            ? fullPath
            : relativePath;
    }

    /// <summary>
    /// Converts a stored relative or absolute path into an absolute path.
    /// </summary>
    public static string Resolve(string storedPath)
    {
        
        return Path.IsPathRooted(storedPath)
            ? Path.GetFullPath(storedPath)
            : Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, storedPath));
    }
}