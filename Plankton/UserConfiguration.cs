using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Plankton.TypeConverters;

namespace Plankton
{
    public static class UserConfigurationManager
    {
        public static UserConfiguration userConfig;
        public static void Load(string configpath)
        {
            userConfig = UserConfigurationSerializer.Load(configpath);
        }

        public static void Save(string configpath)
        {
            UserConfigurationSerializer.Save(userConfig, configpath);
        }
    }

    internal static class UserConfigurationSerializer
    {
        public static UserConfiguration Load(string configpath)
        {
            if (File.Exists(configpath))
            {
                string json = File.ReadAllText(configpath);
                return JsonSerializer.Deserialize<UserConfiguration>(json) ?? new UserConfiguration();
            }
            return new UserConfiguration();
        }

        public static void Save(UserConfiguration userconfig, string configpath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(userconfig, options);
            File.WriteAllText(configpath, json);
        }
    }

    [Serializable]
    public class UserConfiguration
    {
        public List<string> allPluginPaths { get; set; } = new();
        public List<string> loadedPluginPaths { get; set; } = new();

        public RenderingConfiguration renderingConfiguration { get; set; } = new();
    }

    [Serializable]
    public class RenderingConfiguration
    {
        [DisplayName("Alpha Transparency")]
        [Description("If objects will be rendered with transparency")]
        [Category("Rendering")]
        public bool alphaTransparency { get; set; } = true;
        [DisplayName("Fog")]
        [Description("Wether or not fog gets rendered")]
        [Category("Rendering")]
        public bool enableFog { get; set; } = true;

        [DisplayName("Background Color")]
        [Description("Background color of the 3D viewers")]
        [Category("Rendering")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color backgroundColor { get; set; } = Color.Black;

        [DisplayName("Movement Speed")]
        [Description("How fast the 3D camera moves")]
        [Category("Controls")]
        public float movementSpeed { get; set; } = 0.3f;
        [DisplayName("Rotation Speed")]
        [Description("How fast the 3D camera rotates")]
        [Category("Controls")]
        public float rotationSpeed { get; set; } = 0.07f;

        
    }
}
