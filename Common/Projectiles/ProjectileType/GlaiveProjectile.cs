using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using DestinyMod.Content.Projectiles.Weapons.Melee.Glaive;
using Terraria.GameInput;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Common.Projectiles.ProjectileType
{
	public abstract class GlaiveProjectile : DestinyModProjectile
	{
		protected int SpriteWidth;

		protected int SpriteHeight;

		public override void AutomaticSetDefaults()
		{
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ownerHitCheck = true;
			Projectile.hide = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.extraUpdates = 1;
			Projectile.aiStyle = -1;
		}

        public override void AI()
        {
			Player owner = Main.player[Projectile.owner];

			owner.heldProj = Projectile.whoAmI;

			GlaiveItem glaive = owner.HeldItem.ModItem as GlaiveItem;
			if (Main.myPlayer == Projectile.owner && PlayerInput.Triggers.Current.MouseRight && glaive.GlaiveCharge > 0)
			{
				owner.GetModPlayer<ItemPlayer>().GlaiveShielded = true;
				glaive.GlaiveCharge -= 0.25f;
			}
			else if ((Main.myPlayer == Projectile.owner && !PlayerInput.Triggers.Current.MouseRight) || glaive.GlaiveCharge <= 0)
            {
				owner.GetModPlayer<ItemPlayer>().GlaiveShielded = false;
			}

			Projectile.rotation = (Main.MouseWorld - owner.MountedCenter).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
			Projectile.Center = owner.RotatedRelativePoint(owner.MountedCenter, false, false);
			Projectile.spriteDirection = (Vector2.Dot(Main.MouseWorld - owner.MountedCenter, Vector2.UnitX) >= 0f).ToDirectionInt();
			owner.direction = Projectile.spriteDirection;

			DrawOriginOffsetX = 0;
			DrawOffsetX = -(SpriteWidth / 2 - Projectile.width / 2);
			DrawOriginOffsetY = -(SpriteHeight / 2 - Projectile.height / 2);
		}

        public override void Kill(int timeLeft)
        {
			Main.player[Projectile.owner].GetModPlayer<ItemPlayer>().GlaiveShielded = false;
		}

        public override bool ShouldUpdatePosition() => false;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
			float discard = 0;
			if (Projectile.ai[1] == 2)
            {
				return false;
            }
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * 6, 10 * Projectile.scale, ref discard);
        }

        public override void CutTiles()
        {
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10, 10 * Projectile.scale, DelegateMethods.CutTiles);
        }
    }
}