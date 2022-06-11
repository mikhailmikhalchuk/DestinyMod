using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using System;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class ItemModGranter : DestinyModItem
	{
		public int ItemModType;

        public string ItemModName;

        // TO-DO: This should remove itself from the inventory and grant the mod.
    }
}