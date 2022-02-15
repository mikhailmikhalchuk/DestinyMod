using DestinyMod.Common.ModSystems;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
	public class GuardianGamesConfig : ILoadable
    {
        public void Load(Mod mod)
        {
            if (Main.dedServ)
            {
                return;
            }

            if (DestinyClientConfig.Instance.GuardianGamesConfig)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(3);
                    client.BaseAddress = new Uri("https://DestinyModServer.mikhailmcraft.repl.co/");
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("VERIFY-MOD", "a7rg53F435h4Ff2fhjWa33gH6j54ag2G");
                    request.Headers.Add("DUPLICATE-CHECK", Steamworks.SteamUser.GetSteamID().ToString());
                    HttpResponseMessage response = client.Send(request);

                    using Stream s = response.Content.ReadAsStream();
                    using StreamReader sr = new StreamReader(s);

                    var jsonResponse = sr.ReadToEnd();
                    if (jsonResponse.Remove(2) == "ON")
                    {
                        GuardianGames.Active = true;
                    }

                    if (jsonResponse.Contains('T'))
                    {
                        GuardianGames.WinningTeam = DestinyClassType.Titan;
                    }
                    if (jsonResponse.Contains('H'))
                    {
                        GuardianGames.WinningTeam = DestinyClassType.Hunter;
                    }
                    if (jsonResponse.Contains('W'))
                    {
                        GuardianGames.WinningTeam = DestinyClassType.Warlock;
                    }
                    DestinyMod.Instance.Logger.Info(GuardianGames.WinningTeam);
                }
                catch (Exception exception)
                {
                    mod.Logger.Error("Failed to receive a response from the server: " + exception.Message);
                    GuardianGames.GameError = true;
                }
            }
            else
            {
                GuardianGames.GameError = true;
            }
        }

        public void Unload() { }
    }
}