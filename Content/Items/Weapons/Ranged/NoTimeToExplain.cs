using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Content.Items.Perks.Weapon.Barrels;
using DestinyMod.Content.Items.Perks.Weapon.Magazines;
using DestinyMod.Content.Items.Perks.Weapon.Traits;
using DestinyMod.Content.Items.Catalysts;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class NoTimeToExplain : Gun
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("No Time to Explain");
			Tooltip.SetDefault("Three round burst"
				+ "\n'A single word etched onto the inside of the weapon's casing: Now.'");

			ItemData ntteItemData = ItemData.InitializeNewItemData(Type, 1370, null, 0);
			ntteItemData.DefaultImpact = 33;
			ntteItemData.DefaultRange = 70;
			ntteItemData.DefaultStability = 55;
			ntteItemData.DefaultRecoil = 90;
			ntteItemData.ItemCatalyst = ModifierBase.GetType<NoTimeToExplainCatalyst>();
			ntteItemData.GeneratePerkPool = () => new List<ItemPerkPool>()
			{
				new ItemPerkPool("Barrel", ModContent.GetInstance<ArrowheadBrake>()),
				new ItemPerkPool("Magazine", ModContent.GetInstance<AccurizedRounds>()),
				new ItemPerkPool("Amongus", ModContent.GetInstance<AccurizedRounds>()), // https://www.reddit.com/r/ComedyNecrophilia/comments/v77n45/soos/
				new ItemPerkPool("Trait", ModContent.GetInstance<HighCaliberRounds>())
			};
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 65;
			Item.autoReuse = true;
			Item.useTime = 4;
			Item.useAnimation = 12;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/NoTimeToExplain");
			Item.shootSpeed = 16f;
			Item.reuseDelay = 10;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			velocity = ItemData.AccountForRecoil(player, velocity);
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-12, 0);
	}
}