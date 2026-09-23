using System;
using System.Diagnostics;
using System.IO;
using DiscordRPC;
using DiscordRPC.Message;
using PluginApi;
using CSHO;

namespace DiscordRichPresencePlugin
{
    public sealed class DRP : IPlugin
    {
        public string Id => "plankton.discord-rich-presence";

        public string Name => "Discord Rich Presence";

        public string Description =>
            "Makes Plankton show up in your Discord activity";

        private const string DiscordApplicationId =
            "1528543273763868834";

        private IHost? _host;
        private DiscordRpcClient? _discord;
        private DateTime _sessionStartedAt;

        public void Initialize(IHost host)
        {
            _host = host;
            _sessionStartedAt = DateTime.UtcNow;

            _discord = new DiscordRpcClient(
                DiscordApplicationId);

            _discord.OnConnectionFailed += OnConnectionFailed;
            _discord.OnError += OnError;

            _discord.Initialize();

            host.Archive.Opened += OnArchiveOpened;
            host.Archive.Closed += OnArchiveClosed;

            UpdateDiscordPresence();
        }

        private void OnArchiveOpened(
            object? sender,
            ArchiveEventArgs e)
        {
            UpdateDiscordPresence();
        }

        private void OnArchiveClosed(
            object? sender,
            ArchiveEventArgs e)
        {
            UpdateDiscordPresence();
        }

        private void UpdateDiscordPresence()
        {
            if (_discord == null || _host == null)
                return;

            string details;

            if (_host.Archive.Current.Archive == null)
            {
                details = "No file open";
            }
            else
            {
                string filePath =
                    _host.Archive.Current.path;

                string fileName =
                    Path.GetFileName(filePath);

                details = $"Editing {fileName}";
            }

            _discord.SetPresence(
                new RichPresence
                {
                    Details = details,

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

        private void OnConnectionFailed(
            object? sender,
            ConnectionFailedMessage e)
        {
            Debug.WriteLine(
                "Discord is unavailable.");
        }

        private void OnError(
            object? sender,
            ErrorMessage e)
        {
            Debug.WriteLine(e.Message);
        }

    }
}