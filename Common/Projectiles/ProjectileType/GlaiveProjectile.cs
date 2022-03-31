using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using DestinyMod.Content.Projectiles.Weapons.Melee.Glaive;

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
			Projectile.timeLeft = 360;
			Projectile.aiStyle = -1;
		}

        public override void AI()
        {
			Player owner = Main.player[Projectile.owner];

			if (++Projectile.ai[0] >= 16 && Projectile.ai[1] != 2) //important
			{
				Projectile.NewProjectile(owner.GetProjectileSource_Misc(0), owner.Center, Projectile.velocity * 3, ModContent.ProjectileType<GlaiveShot>(), 10, 0, owner.whoAmI);
				Projectile.Kill();
			}
			else
			{
				owner.heldProj = Projectile.whoAmI;
				owner.itemTime = owner.itemAnimation = 1;
			}

			Projectile.Opacity = Utils.GetLerpValue(0, 7, Projectile.ai[0], true) * Utils.GetLerpValue(16, 12, Projectile.ai[0], true);
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
			Projectile.Center = owner.RotatedRelativePoint(owner.MountedCenter, false, false) + Projectile.velocity * (Projectile.ai[0] - 1);
			Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

			DrawOriginOffsetX = 0;
			DrawOffsetX = -(SpriteWidth / 2 - Projectile.width / 2);
			DrawOriginOffsetY = -(SpriteHeight / 2 - Projectile.height / 2);
        }

		public override bool ShouldUpdatePosition() => false;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
			float discard = 0;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * 6, 10 * Projectile.scale, ref discard);
        }

        public override void CutTiles()
        {
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10, 10 * Projectile.scale, DelegateMethods.CutTiles);
        }
    }
}