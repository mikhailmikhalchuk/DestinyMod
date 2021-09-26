using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Net;
using System.IO;
using System;

namespace TheDestinyMod.Tiles
{
	public class Podium : ModTile
	{
		private string gameClass;
		private int toDeposit;
		private int cooldown;
		private float num99;

		public override void SetDefaults() {
			Main.tileFrameImportant[Type] = true;
			disableSmartCursor = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 9;
			TileObjectData.newTile.Origin = new Point16(5, 8);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16};
			TileObjectData.newTile.Width = 10;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type); 
			dustType = 63;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Podium");
			AddMapEntry(new Color(255, 255, 255), name);
		}

		public override bool HasSmartInteract() => true;

        public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(i * 16, j * 16, 178, 178, ModContent.ItemType<Items.Placeables.Podium>());
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
			if (cooldown > 0) {
				cooldown--;
			}
			Lighting.AddLight(new Vector2(i * 16, j * 16), Color.White.ToVector3() * 0.55f);
		}

		public void FireRocket() {
			DestinyPlayer dPlayer = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			if (dPlayer.classType == DestinyClassType.Warlock) {
				Projectile.NewProjectile(Main.LocalPlayer.position, new Vector2(0, -Main.rand.Next(5, 8)), ProjectileID.RocketFireworkYellow, 0, 0, Main.LocalPlayer.whoAmI);
			}
			else if (dPlayer.classType == DestinyClassType.Hunter) {
				Projectile.NewProjectile(Main.LocalPlayer.position, new Vector2(0, -Main.rand.Next(5, 8)), ProjectileID.RocketFireworkBlue, 0, 0, Main.LocalPlayer.whoAmI);
			}
			else if (dPlayer.classType == DestinyClassType.Titan) {
				Projectile.NewProjectile(Main.LocalPlayer.position, new Vector2(0, -Main.rand.Next(5, 8)), ProjectileID.RocketFireworkRed, 0, 0, Main.LocalPlayer.whoAmI);
			}
		}

        public override void MouseOver(int i, int j) {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = ModContent.ItemType<Items.Laurel>();
		}

        public override bool NewRightClick(int i, int j) {
			/*num99 = 0;
			for (float horiz = -50; horiz < 50; horiz += 2) {
				int num657 = Dust.NewDust(new Vector2(Main.LocalPlayer.position.X + horiz, Main.LocalPlayer.position.Y), 6, 6, 133, 0f, 0f, 100, default(Color), 1.3f);
				Main.dust[num657].velocity *= 0;
				Main.dust[num657].noGravity = true;
			}
			for (float left1 = -40; left1 < 0; left1++) {
				int num657 = Dust.NewDust(new Vector2(Main.LocalPlayer.position.X - 25 + left1, Main.LocalPlayer.position.Y + num99), 6, 6, 133, 0f, 0f, 100, default(Color), 1.3f);
				Main.dust[num657].noGravity = true;
				Main.dust[num657].velocity *= 0;
				num99 -= 2;
			}*/
			toDeposit = 0;
			DestinyPlayer dPlayer = Main.LocalPlayer.GetModPlayer<DestinyPlayer>();
			if (!DestinyConfig.Instance.GuardianGamesConfig) {
				Main.NewText("You must opt-in to the Guardian Games from the config menu.", new Color(255, 0, 0));
				return true;
			}
			if (DestinyConfig.Instance.GuardianGamesConfig && TheDestinyMod.guardianGameError) {
				Main.NewText("You must reload the mod after enabling the config toggle.", new Color(255, 0, 0));
				return true;
			}
			if (TheDestinyMod.guardianWinner > 0) {
				Main.NewText("The Guardian Games have ended. Speak to Zavala for the closing statement.", new Color(0, 170, 255));
				return true;
			}
			Main.NewText("Depositing Laurels...", new Color(255, 255, 0));
			foreach (Item item in Main.LocalPlayer.inventory) {
				if (item.type == ModContent.ItemType<Items.Laurel>()) {
					toDeposit += item.stack;
					item.TurnToAir();
				}
			}
			if (toDeposit == 0) {
				Main.NewText("You have no Laurels to deposit.", new Color(255, 0, 0));
			}
			else if (TheDestinyMod.guardianGameError) {
				Main.NewText("Couldn't connect to the internet to deposit Laurels.", new Color(255, 0, 0));
				Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Laurel>(), toDeposit);
			}
			else if (dPlayer.classType == DestinyClassType.None) {
				Main.NewText("You must create a new character and select a class to participate in the Games.", new Color(255, 0, 0));
				Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Laurel>(), toDeposit);
			}
			else if (cooldown > 0) {
				Main.NewText("Please wait to deposit Laurels again.", new Color(255, 0, 0));
				Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Laurel>(), toDeposit);
			}
			else if (!TheDestinyMod.guardianGames) {
				Main.NewText("The Guardian Games have ended!", new Color(255, 0, 0));
				Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Laurel>(), toDeposit);
			}
			else {
				try {
					if (dPlayer.classType == DestinyClassType.Warlock) {
						gameClass = "WARLOCK";
					}
					else if (dPlayer.classType == DestinyClassType.Titan) {
						gameClass = "TITAN";
					}
					else if (dPlayer.classType == DestinyClassType.Hunter) {
						gameClass = "HUNTER";
					}
					var request = (HttpWebRequest)WebRequest.Create("https://DestinyModServer.mikhailmcraft.repl.co");

					var postData = Uri.EscapeDataString(toDeposit.ToString());
					var data = System.Text.Encoding.ASCII.GetBytes(postData);

					request.Method = "POST";
					request.ContentType = "application/x-www-form-urlencoded";
					request.ContentLength = data.Length;
					request.Headers["VERIFY-MOD"] = "a7rg53F435h4Ff2fhjWa33gH6j54ag2G";
					request.Headers["CLASS"] = gameClass;
					request.Headers["DUPLICATE-CHECK"] = Steamworks.SteamUser.GetSteamID().ToString();
					request.Timeout = 1500;

					using (var stream = request.GetRequestStream()) {
						stream.Write(data, 0, data.Length);
					}

					var response = (HttpWebResponse)request.GetResponse();

					var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

					if (responseString == "TOOMUCH") {
						Main.NewText("You've already deposited 30 Laurels today! Try again tomorrow.", new Color(255, 0, 0));
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Laurel>(), toDeposit);
					}
					else if (responseString.Remove(responseString.IndexOf("RECEIVED")) != "") {
						Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Laurel>(), toDeposit - int.Parse(responseString.Remove(responseString.IndexOf("RECEIVED"))));
						Main.NewText($"Deposited {responseString.Remove(responseString.IndexOf("RECEIVED"))} Laurels!", new Color(255, 255, 0));
						Main.LocalPlayer.QuickSpawnItem(ItemID.GoldCoin, toDeposit - int.Parse(responseString.Remove(responseString.IndexOf("RECEIVED"))));
						FireRocket();
					}
					else {
						Main.NewText($"Deposited {toDeposit} Laurels!", new Color(255, 255, 0));
						Main.LocalPlayer.QuickSpawnItem(ItemID.GoldCoin, toDeposit);
						FireRocket();
					}
				}
				catch (Exception e) {
					TheDestinyMod.Instance.Logger.Error($"Failed to send Laurels to the server: {e.Message}");
					Main.NewText("An error occurred and Laurels couldn't be deposited", new Color(255, 0, 0));
					Main.LocalPlayer.QuickSpawnItem(ModContent.ItemType<Items.Laurel>(), toDeposit);
				}
			}
			if (cooldown == 0) {
				cooldown = 6000;
			}
			return true;
        }
    }
}