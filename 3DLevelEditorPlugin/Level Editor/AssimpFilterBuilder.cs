using System;
using System.Linq;
using System.Text;
using Assimp;

// [ChatGPT generated]

public static class AssimpFilterBuilder
{
    public static string BuildSaveFileDialogFilter()
    {
        using var ctx = new AssimpContext();
        var formats = ctx.GetSupportedExportFormats();

        var sb = new StringBuilder();

        // Build individual format entries
        foreach (var fmt in formats.OrderBy(f => f.Description))
        {
            // fmt.FileExtension can sometimes contain multiple extensions separated by space or semicolon
            var extensions = fmt.FileExtension
                .Split(new[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => "*." + e.Trim());

            string extList = string.Join(";", extensions);

            sb.Append($"{fmt.Description} ({extList})|{extList}|");
        }

        // Add combined "All supported formats"
        var allExtensions = formats
            .SelectMany(f => f.FileExtension
                .Split(new[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries))
            .Distinct()
            .Select(e => "*." + e.Trim());

        string allExtList = string.Join(";", allExtensions);

        sb.Insert(0, $"All Supported Formats ({allExtList})|{allExtList}|");

        // Remove trailing '|'
        if (sb.Length > 0)
            sb.Length--;

        return sb.ToString();
    }
}