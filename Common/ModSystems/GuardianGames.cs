using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Net;
using System.IO;
using System;
using DestinyMod.Common.ModPlayers;
using Terraria.ModLoader.IO;
using DestinyMod.Content.Items.Misc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DestinyMod.Common.ModSystems
{
	public class GuardianGames : ModSystem
	{
		public static bool Active;

		public static int DepositCooldown;

		public static DestinyClassType WinningTeam;

		public static bool ClaimedItem;

		public static bool GameError;

		public static bool TryDeposit(Player player)
		{
			if (DepositCooldown > 0)
			{
				Main.NewText("Please wait to deposit Laurels again.", Color.Red);
				return false;
			}
			ClassPlayer classPlayer = player.GetModPlayer<ClassPlayer>();

			if (!DestinyClientConfig.Instance.GuardianGamesConfig)
            {
				Main.NewText("You must opt-in to the Guardian Games from the config menu.", Color.Red);
				return true;
            }
			if (GameError)
            {
				Main.NewText("Couldn't connect to the internet to deposit Laurels. Try restarting the mod.", Color.Red);
				return true;
			}
			if (!player.HasItem(ModContent.ItemType<Laurel>()))
            {
				Main.NewText("You have no Laurels to deposit.", Color.Red);
				return true;
			}
			if (classPlayer.ClassType == DestinyClassType.None)
            {
				Main.NewText("Your character does not have a class.", Color.Red);
				return true;
			}
			if (!Active)
            {
				Main.NewText("The Guardian Games have ended!", Color.Red);
				return true;
			}
			if (WinningTeam != DestinyClassType.None)
            {
				Main.NewText("The Guardian Games have ended. Speak to Zavala for the closing statement.", new Color(0, 170, 255));
				return true;
			}

			try
			{
				int laurelCount = 0;
				int laurelType = ModContent.ItemType<Laurel>();
				foreach (Item item in player.inventory)
				{
					if (item.type == laurelType)
					{
						laurelCount += item.stack;
					}

					item.SetDefaults();
				}

				Task.Run(async () =>
				{
					HttpClient client = new HttpClient
					{
						Timeout = TimeSpan.FromSeconds(4),
						BaseAddress = new Uri("https://DestinyModServer.mikhailmcraft.repl.co")
					};
					HttpRequestMessage request = new HttpRequestMessage
					{
						Method = HttpMethod.Post
					};
					request.Headers.Add("VERIFY-MOD", "a7rg53F435h4Ff2fhjWa33gH6j54ag2G");
					request.Headers.Add("CLASS", classPlayer.ClassType.ToString().ToUpper());
					request.Headers.Add("STEAM-USER", Steamworks.SteamUser.GetSteamID().ToString());

					FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
					{
						new KeyValuePair<string, string>("data", Uri.EscapeDataString(laurelCount.ToString()))
					});

					HttpResponseMessage response = await client.PostAsync("/stuff", content);

					await Task.Delay(3000);

					string responseString = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();

					if (responseString == "TOOMUCH")
					{
						Main.NewText("You've already deposited 30 Laurels today! Try again tomorrow.", Color.Red);
						player.QuickSpawnItem(laurelType, laurelCount);
					}
					else
					{
						string depositedLaurelsString = responseString.Remove(responseString.IndexOf("RECEIVED"));
						if (string.IsNullOrEmpty(depositedLaurelsString))
						{
							int depositedLaurels = int.Parse(depositedLaurelsString);
							player.QuickSpawnItem(laurelType, laurelCount - depositedLaurels);
							Main.NewText("Deposited " + depositedLaurels + " Laurels!", new Color(255, 255, 0));
							player.QuickSpawnItem(ItemID.GoldCoin, laurelCount - depositedLaurels);
						}
						else
						{
							Main.NewText("Deposited " + laurelCount + "Laurels!", new Color(255, 255, 0));
							player.QuickSpawnItem(ItemID.GoldCoin, laurelCount);
						}

						int firedRocket = 0;
						switch (classPlayer.ClassType)
						{
							case DestinyClassType.Titan:
								firedRocket = ProjectileID.RocketFireworkRed;
								break;

							case DestinyClassType.Hunter:
								firedRocket = ProjectileID.RocketFireworkBlue;
								break;

							case DestinyClassType.Warlock:
								firedRocket = ProjectileID.RocketFireworkYellow;
								break;

							default:
								DepositCooldown = 6000;
								break;
						}

						Projectile.NewProjectile(player.GetProjectileSource_Misc(0), player.Center, new Vector2(0, -Main.rand.Next(5, 8)), firedRocket, 0, 0, player.whoAmI);
					}
				});

				return true;
			}
			catch
			{
				return false;
			}
		}


		public override void SaveWorldData(TagCompound tag) => tag.Add("Cooldown", DepositCooldown);

		public override void LoadWorldData(TagCompound tag) => DepositCooldown = tag.Get<int>("Cooldown");
	}
}