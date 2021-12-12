using TheDestinyMod.Core.Autoloading;
using Terraria;
using System.IO;
using System;
using System.Net;

namespace TheDestinyMod.Content.Autoloading.Misc
{
    public class GuardianGamesConfig : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject)
		{
            if (Main.dedServ)
			{
                return;
			}

            TheDestinyMod mod = TheDestinyMod.Instance;

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
                                TheDestinyMod.guardianGames = true;
                            }

                            if (jsonResponse.Contains("T"))
                            {
                                TheDestinyMod.guardianWinner = 1;
                            }
                            else if (jsonResponse.Contains("H"))
                            {
                                TheDestinyMod.guardianWinner = 2;
                            }
                            else if (jsonResponse.Contains("W"))
                            {
                                TheDestinyMod.guardianWinner = 3;
                            }
                        }
                    }
#pragma warning restore IDE0063
                }
                catch (Exception e)
                {
                    mod.Logger.Error($"Failed to receive a response from the server: {e.Message}");
                    TheDestinyMod.guardianGameError = true;
                }
            }
            else
            {
                TheDestinyMod.guardianGameError = true;
            }
        }

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }
    }
}