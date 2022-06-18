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
using DestinyMod.Content.Items.Perks.Weapon.Magazines;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using DestinyMod.Common.Data;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
    public class HakkeAutoRifle : Gun
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ItemData hakkeItemData = ItemData.InitializeNewItemData(Type, 1370, null, 1);
			hakkeItemData.GeneratePerkPool = () => new List<ItemPerkPool>()
			{
				new ItemPerkPool("Barrel", ItemData.RollRandomPerks(2, PerkClassifications.GenericBarrels)),
				new ItemPerkPool("Trait", ModContent.GetInstance<Frenzy>(), ModContent.GetInstance<HighCaliberRounds>()),
				new ItemPerkPool("Origin Trait", ModContent.GetInstance<HakkeCraftsmanship>())
			};
			// Throwaway stats for testing
			hakkeItemData.DefaultStability = 80;
			hakkeItemData.DefaultRecoil = 20;
			hakkeItemData.DefaultRange = 50;
			hakkeItemData.DefaultReloadSpeed = 80;
			hakkeItemData.DefaultMagazineCapacity = 30;
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
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 10f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			position += new Vector2(0, -2);
			type = ModContent.ProjectileType<HakkeBullet>();
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -2);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 45)
			.AddIngredient(ItemID.HellstoneBar, 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}