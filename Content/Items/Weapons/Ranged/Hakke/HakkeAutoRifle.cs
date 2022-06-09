using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.Items;
using System.Collections.Generic;
using DestinyMod.Content.Items.Perks.Weapon.Barrels;
using DestinyMod.Content.Items.Perks.Weapon.Traits;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkeAutoRifle : HakkeCraftsmanshipWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ItemData hakkeItemData = ItemData.InitializeNewItemData(Type, 1370, null, 1);
			hakkeItemData.Recoil = -1;
			hakkeItemData.DefaultRange = -1;
			hakkeItemData.DefaultStability = -1;
			hakkeItemData.DefaultHandling = -1;
		}

        public override void DestinySetDefaults()
		{
			Item.damage = 7;
			Item.useTime = 9;
			Item.useAnimation = 9;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/HakkeAutoRifle");
			Item.autoReuse = true;
			Item.shootSpeed = 30f;
			ShootOffset = new Vector2(0, -2);
			SpreadRadians = MathHelper.ToRadians(5);
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -2);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 45)
			.AddIngredient(ItemID.HellstoneBar, 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}