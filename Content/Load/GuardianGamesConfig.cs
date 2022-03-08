using DestinyMod.Common.ModSystems;
using DestinyMod.Core.Utils;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Configs;

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
                    Task.Run(() =>
                    {
                        //run this in the background (because the data being retrieved is not necessary for immediate runtime data)
                        HttpClient client = new HttpClient
                        {
                            Timeout = TimeSpan.FromSeconds(3),
                            BaseAddress = new Uri("https://DestinyModServer.mikhailmcraft.repl.co/")
                        };
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get
                        };
                        request.Headers.Add("VERIFY-MOD", "a7rg53F435h4Ff2fhjWa33gH6j54ag2G");
                        request.Headers.Add("DUPLICATE-CHECK", Steamworks.SteamUser.GetSteamID().ToString());
                        HttpResponseMessage response = client.Send(request);

                        Task.Delay(1500);

                        using Stream s = response.Content.ReadAsStream();

                        Task.Delay(500);

                        using StreamReader sr = new StreamReader(s);
                        var jsonResponse = sr.ReadToEnd();
                        if (jsonResponse.Remove(2) == "ON")
                        {
                            GuardianGames.Active = true;
                        }

                        if (jsonResponse.Length >= 3)
                        {
                            GuardianGames.WinningTeam = (DestinyClassType)(jsonResponse[2] - '0');
                        }
                        /*if (jsonResponse.Contains('T'))
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
                        }*/
                        DestinyMod.Instance.Logger.Info(GuardianGames.WinningTeam);
                    });
                    DestinyMod.Instance.Logger.Info("Test!");
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