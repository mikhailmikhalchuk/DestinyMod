using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using DestinyMod.Common.ModPlayers;
using Terraria.GameInput;
using DestinyMod.Content.Projectiles.Weapons.Melee.Glaive;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class GlaiveItem : DestinyModItem
	{
		public float GlaiveCharge { get; set; }

		private int _proj;

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			Tooltip.SetDefault("Fires a projectile that grants glaive energy on hit"
				+ "\nRight Click to raise a shield, consuming glaive energy");
		}

		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			Item.DamageType = DamageClass.Melee;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.shootSpeed = 10f;
			Item.reuseDelay = 20;
			Item.holdStyle = ItemUseStyleID.Thrust;
		}

		public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player) => !(player.altFunctionUse == 2 && GlaiveCharge == 0);

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2)
            {
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<GlaiveShot>(), 10, 0, player.whoAmI);
			}
			return false;
		}

        public override void OnHold(Player player)
        {
			_proj = Projectile.NewProjectile(player.GetProjectileSource_Item(Item), player.Center, Vector2.Zero, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
		}

        public override void OnRelease(Player player)
        {
			Main.projectile[_proj].Kill();
        }

		public override ItemPlayer.IterationContext PostUpdateRunSpeedsContext(Player player) => ItemPlayer.IterationContext.HeldItem;

        public override void PostUpdateRunSpeeds(Player player)
        {
			if (player.GetModPlayer<ItemPlayer>().GlaiveShielded)
            {
				player.maxRunSpeed /= 2;
				player.accRunSpeed /= 2;
				player.dashDelay = 10;
			}
        }

        public override ItemPlayer.IterationContext ModifyDrawInfoContext(Player player) => ItemPlayer.IterationContext.HeldItem;

		public override void ModifyDrawInfo(Player player, ref PlayerDrawSet drawInfo)
		{
			if (GlaiveCharge > 0)
			{
				drawInfo.DrawDataCache.Add(new DrawData(ModContent.Request<Texture2D>("Terraria/Images/MagicPixel").Value, new Rectangle((int)(drawInfo.drawPlayer.Center.X - Main.screenPosition.X - GlaiveCharge / 2), (int)(drawInfo.drawPlayer.Center.Y - Main.screenPosition.Y) + 30, (int)GlaiveCharge, 10), Color.White));
			}
		}
	}
}