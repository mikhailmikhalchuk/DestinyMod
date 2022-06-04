using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class WhisperOfTheWorm : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Whisper of the Worm");
			Tooltip.SetDefault("Missing a shot has a chance to return ammo"
				+ "\n'A Guardian's power makes a rich feeding ground. Do not be revolted.'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 250;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/WhisperOfTheWorm");
			Item.shootSpeed = 16f;
			DestinyModReuseDelay = 15;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextBool(4))
			{
				Dust.NewDust(position += Vector2.Normalize(velocity) * 90f, 1, 1, DustID.WhiteTorch);
			}
			DestinyModProjectile.NewAmmoReturnProjectile(source, position + new Vector2(0, -2), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -2);
	}
}