using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using System.Reflection;
using Terraria.ModLoader.IO;
using System;
using Microsoft.Xna.Framework;

namespace DestinyMod.Content.Commands
{
	public class PortItemsCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "portItems";

		public override string Usage => "/portItems";

		public override string Description => "Ports all items from the 1.3 version of the mod to the 1.4 version";

		public override void Action(CommandCaller caller, string input, string[] args)
        {
			int numPorted = 0;
			foreach(Item item in Main.LocalPlayer.inventory)
            {
				try
                {
					if (item.ModItem is UnloadedItem unItem && unItem.ModName == "TheDestinyMod")
					{
						numPorted++;
						unItem.GetType().GetProperty("ModName").SetValue(unItem, "DestinyMod");
						TagCompound returned = (TagCompound)unItem.GetType().GetField("data", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(unItem);
						returned.Remove("mod");
						returned.Set("mod", "DestinyMod");
						unItem.GetType().GetField("data", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(unItem, returned);
					}
				}
				catch (Exception e)
                {
					DestinyMod.Instance.Logger.Error("An exception occurred while trying to port items via PortItemsCommand: \n" + e);
					Main.NewText("An error occurred while porting items. Let the developers know of your issue.", Color.Red);
					return;
                }
            }
			if (numPorted > 0)
            {
				Main.NewText($"Successfully ported {numPorted} items! Reload the world to apply.", Color.Green);
			}
			else
            {
				Main.NewText("No items were found to port.", Color.Yellow);
            }
        }
	}
}