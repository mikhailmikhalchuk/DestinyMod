﻿using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class DeadMansTale : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dead Man's Tale");
			Tooltip.SetDefault("'Long, short, they all end the same way. -Katabasis'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/MidaMultiTool");
			Item.shootSpeed = 30f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile projectile = Projectile.NewProjectileDirect(source, new Vector2(position.X, position.Y - 7), velocity, type, damage, knockback, player.whoAmI);
			if (projectile.extraUpdates < 3)
			{
				projectile.extraUpdates = 3;
			}
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-12, 0);
	}
}