using PluginApi;
using DiscordRPC;

namespace DiscordRichPresencePlugin
{
    public class DRP : IPlugin
    {
        public string Name => "Discord Rich Presence";
        public string Description => "Makes Plankton show up in your Discord activity";

        private const string DiscordApplicationId =
        "1528543273763868834";

        private DiscordRpcClient _discord;
        private DateTime _sessionStartedAt;

        public void Initialize(IHost host)
        {
            _sessionStartedAt = DateTime.UtcNow;

            _discord = new DiscordRpcClient(
                DiscordApplicationId);

            _discord.OnConnectionFailed += (_, _) =>
            {
                System.Diagnostics.Debug.WriteLine(
                    "Discord is unavailable.");
            };

            _discord.OnError += (_, eventArgs) =>
            {
                System.Diagnostics.Debug.WriteLine(
                    eventArgs.Message);
            };

            _discord.Initialize();

            UpdateDiscordPresence(
                projectName: null,
                editorName: "");

            host.OpenedArchive += (object s, FileEventArgs e) => { UpdateDiscordPresence(e.filename, ""); };
            host.ClosedArchive += (object s, FileEventArgs e) => { UpdateDiscordPresence(null, ""); };
        }

        private void UpdateDiscordPresence(
            string? projectName,
            string editorName)
        {
            string details = projectName is null
                ? "No file open"
                : $"Editing {projectName}";

            _discord.SetPresence(new RichPresence
            {
                Details = details,
                State = editorName,

                Timestamps = new Timestamps
                {
                    Start = _sessionStartedAt
                },

                Assets = new Assets
                {
                    LargeImageKey = "app_logo",
                    LargeImageText = "Plankton"
                }
            });
        }

        
    }
}
