using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using PluginApi;

namespace Plankton.PluginApiImplementation;

internal sealed class PreferencesService : IPreferencesService
{
    private readonly string filePath;

    private readonly JsonSerializerOptions jsonOptions;
    private readonly JsonObject root;

    private readonly List<PreferenceSection> sections = new();
    private readonly Dictionary<string, PreferenceSection> sectionsById = new();

    public IReadOnlyList<PreferenceSection> Sections => sections;

    public PreferencesService()
    {
        string appData = Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData);

        filePath = Path.Combine(
            appData,
            "Plankton",
            "preferences.json");

        jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        jsonOptions.Converters.Add(
            new JsonStringEnumConverter());

        root = LoadRoot();
    }

    public T Register<T>(string id, string name)
        where T : class, new()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (sectionsById.ContainsKey(id))
        {
            throw new InvalidOperationException(
                $"Preferences section '{id}' has already been registered.");
        }

        T preferences = LoadSection<T>(id);

        var section = new PreferenceSection(
            id,
            name,
            preferences);

        sections.Add(section);
        sectionsById.Add(id, section);

        return preferences;
    }

    public void Save()
    {
        /*
         * Only replace sections that are currently registered.
         *
         * Everything else in 'root' is left alone. This means preferences
         * belonging to plugins that aren't currently installed/loaded are
         * preserved.
         */
        foreach (PreferenceSection section in sections)
        {
            JsonNode? node = JsonSerializer.SerializeToNode(
                section.Value,
                section.Value.GetType(),
                jsonOptions);

            root[section.Id] = node;
        }

        string? directory = Path.GetDirectoryName(filePath);

        if (directory != null)
            Directory.CreateDirectory(directory);

        string tempPath = filePath + ".tmp";

        File.WriteAllText(
            tempPath,
            root.ToJsonString(jsonOptions));

        File.Move(
            tempPath,
            filePath,
            overwrite: true);
    }

    private T LoadSection<T>(string id)
        where T : class, new()
    {
        if (!root.TryGetPropertyValue(id, out JsonNode? node) ||
            node == null)
        {
            return new T();
        }

        try
        {
            return node.Deserialize<T>(jsonOptions)
                ?? new T();
        }
        catch (JsonException)
        {
            /*
             * A malformed/outdated plugin section shouldn't prevent
             * Plankton from starting.
             *
             * The defaults will replace this section the next time
             * preferences are saved.
             */
            return new T();
        }
    }

    private JsonObject LoadRoot()
    {
        if (!File.Exists(filePath))
            return new JsonObject();

        try
        {
            string json = File.ReadAllText(filePath);

            return JsonNode.Parse(json) as JsonObject
                ?? new JsonObject();
        }
        catch (JsonException)
        {
            BackupInvalidFile();

            return new JsonObject();
        }
    }

    private void BackupInvalidFile()
    {
        if (!File.Exists(filePath))
            return;

        string directory =
            Path.GetDirectoryName(filePath)
            ?? string.Empty;

        string fileName =
            Path.GetFileNameWithoutExtension(filePath);

        string extension =
            Path.GetExtension(filePath);

        string backupName =
            $"{fileName}.invalid-{DateTime.Now:yyyyMMdd-HHmmss}{extension}";

        string backupPath =
            Path.Combine(directory, backupName);

        File.Copy(
            filePath,
            backupPath,
            overwrite: false);
    }

    internal object Clone(object value)
    {
        JsonNode? json = JsonSerializer.SerializeToNode(
            value,
            value.GetType(),
            jsonOptions);

        return json?.Deserialize(
            value.GetType(),
            jsonOptions)
            ?? throw new InvalidOperationException(
                $"Could not clone preferences type '{value.GetType()}'.");
    }

    internal void CopyInto(object source, object destination)
    {
        Type type = source.GetType();

        if (destination.GetType() != type)
            throw new InvalidOperationException(
                "Preference objects must have the same type.");

        foreach (PropertyInfo property in type.GetProperties(
                     BindingFlags.Instance | BindingFlags.Public))
        {
            if (!property.CanRead || !property.CanWrite)
                continue;

            object? sourceValue = property.GetValue(source);

            if (sourceValue == null ||
                property.PropertyType.IsValueType ||
                property.PropertyType == typeof(string))
            {
                property.SetValue(destination, sourceValue);
                continue;
            }

            object? destinationValue =
                property.GetValue(destination);

            if (destinationValue == null)
            {
                property.SetValue(destination, Clone(sourceValue));
                continue;
            }

            CopyInto(sourceValue, destinationValue);
        }
    }
}