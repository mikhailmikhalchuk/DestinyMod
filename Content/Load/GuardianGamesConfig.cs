using DestinyMod.Common.ModSystems;
using System;
using System.IO;
using System.Net;
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

            GuardianGames guardianGames = ModContent.GetInstance<GuardianGames>();

            if (DestinyClientConfig.Instance.GuardianGamesConfig)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://DestinyModServer.mikhailmcraft.repl.co");
                    request.Method = "GET";
                    request.Headers["VERIFY-MOD"] = "a7rg53F435h4Ff2fhjWa33gH6j54ag2G";
                    request.Headers["DUPLICATE-CHECK"] = Steamworks.SteamUser.GetSteamID().ToString();
                    request.Timeout = 1500;

#pragma warning disable IDE0063
                    using (Stream s = request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            if (jsonResponse.Remove(2) == "ON")
                            {
                                GuardianGames.Active = true;
                            }

                            if (jsonResponse.Contains("T"))
                            {
                                GuardianGames.WinningTeam = 1;
                            }
                            else if (jsonResponse.Contains("H"))
                            {
                                GuardianGames.WinningTeam = 2;
                            }
                            else if (jsonResponse.Contains("W"))
                            {
                                GuardianGames.WinningTeam = 3;
                            }
                        }
                    }
#pragma warning restore IDE0063
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