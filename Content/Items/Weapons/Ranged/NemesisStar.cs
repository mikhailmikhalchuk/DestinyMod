using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class NemesisStar : Gun
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires faster on initial trigger pull"
			+ "\n'What is the answer when the question is extinction?'");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 30;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Red;
			Item.knockBack = 0;
			Item.useTime = 12;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/NemesisStar");
			Item.shootSpeed = 20f;
			Item.useAnimation = 12;
			Item.value = Item.buyPrice(gold: 1);
			Item.channel = true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override float UseTimeMultiplier(Player player) => player.GetModPlayer<StatsPlayer>().ChannelTime < 36 ? 0.5f : 1f;

		public override Vector2? HoldoutOffset() => new Vector2(-15, 0);
	}
}