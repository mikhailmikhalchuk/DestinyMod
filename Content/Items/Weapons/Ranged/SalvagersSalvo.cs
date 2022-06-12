using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Items;
using System.Collections.Generic;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Content.Items.Perks.Weapon.Barrels;
using DestinyMod.Content.Items.Perks.Weapon.Traits;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class SalvagersSalvo : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Salvager's Salvo");
			Tooltip.SetDefault("Grenades fired will explode when the fire button is released"
				+ "\n'The only way out is through.'");
			ItemData salvagersItemData = ItemData.InitializeNewItemData(Type, 1370, null, 1);
			salvagersItemData.GeneratePerkPool = () => new List<ItemPerkPool>()
			{
				new ItemPerkPool("Barrel", ItemData.RollRandomPerks(2, ModContent.GetInstance<ArrowheadBrake>(), ModContent.GetInstance<BarrelShroud>(), ModContent.GetInstance<ChamberedCompensator>())),
				new ItemPerkPool("Trait", ModContent.GetInstance<Frenzy>(), ModContent.GetInstance<HighCaliberRounds>())
			};
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 45;
			Item.channel = true;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/SalvagersSalvo");
			Item.shoot = ModContent.ProjectileType<SalvoGrenade>();
			Item.shootSpeed = 12f;
			Item.useAmmo = ItemID.Grenade;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ModContent.ProjectileType<SalvoGrenade>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -5);
	}
}