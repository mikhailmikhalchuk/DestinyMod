using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class SleeperBeam : DestinyModProjectile
	{
		public bool Fired;

		public int Counter;

		public int SurfaceHits = 3;

		private SlotId FireSound;

		public override void DestinySetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Ranged;
		}

		public override void Kill(int timeLeft)
		{
			if (!Fired)
			{
				SoundEngine.TryGetActiveSound(FireSound, out ActiveSound fireResult);
				fireResult?.Stop();
				SoundEngine.PlaySound(new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/FusionRifleRelease"), Projectile.Center);
			}
			else
			{
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
		}

		public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, 0f, 0f, lightColor.A);

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (--SurfaceHits <= 0)
			{
				Projectile.Kill();
				return false;
			}
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			if (Projectile.ai[1] == 5f && SurfaceHits == 2)
			{
				for (int i = 0; i < 2; i++)
				{
					Projectile.NewProjectile(Projectile.GetSource_TileInteraction((int)(Projectile.position.X / 16), (int)(Projectile.position.Y / 16)), Projectile.Center, Projectile.velocity + new Vector2(Main.rand.Next(-15, 16) * 0.2f, Main.rand.Next(-15, 16) * 0.2f), ModContent.ProjectileType<SleeperBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 6);
				}
			}
			return false;
		}

		public override void AI()
		{
			Projectile.localAI[1]++;
			if (Projectile.ai[1] == 4f)
			{
				if (Counter <= 0)
				{
					FireSound = SoundEngine.PlaySound(new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/FusionRifleCharge"), Projectile.Center);
				}

				Player player = Main.player[Projectile.owner];
				Projectile.position = player.MountedCenter + Projectile.velocity;
				Counter++;

				if (Projectile.owner == Main.myPlayer)
				{
					Vector2 difference = Vector2.Normalize(Main.MouseWorld - player.MountedCenter);
					Projectile.velocity = difference;
					Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
					Projectile.netUpdate = true;
				}

				int dir = Projectile.direction;
				player.ChangeDir(dir);
				player.heldProj = Projectile.whoAmI;
				player.itemAnimation = player.itemTime = 2;
				player.itemRotation = (Projectile.velocity * dir).ToRotation();

				/*Dust dust = Dust.NewDustDirect(player.MountedCenter + new Vector2(0, -5) + Projectile.velocity.ToRotation().ToRotationVector2() * 40f, 15, 20, DustID.RedTorch);
				dust.noGravity = true;
				dust.scale *= 1 + (float)Counter / 43f;*/

				if (Counter == 43)
				{
					SoundEngine.PlaySound(new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/FusionRifleFire"), Projectile.Center);
					Fired = true;
					player.channel = false;

					Item ammoItem = new Item();
					ammoItem.SetDefaults((int)Projectile.ai[0]);

					if (ammoItem.consumable)
					{
						player.ConsumeItem((int)Projectile.ai[0]);
					}

					Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.MountedCenter + Projectile.velocity * 22, 10 * Projectile.velocity * 2f, ModContent.ProjectileType<SleeperBeam>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 0, 5);

					Projectile.Kill();
					player.itemAnimation = player.itemTime = 15;
				}
				else if (!player.channel && Counter < 43 && !Fired)
				{
					Projectile.Kill();
				}
			}

			if (Projectile.ai[1] >= 5f)
			{
				if (Projectile.localAI[0] <= 0)
				{
					Projectile.extraUpdates = 100;
					Projectile.timeLeft = 300;
					//Projectile.velocity = 10 * Projectile.velocity * 2f;
				}
				Projectile.localAI[0]++;
				if (Projectile.localAI[0] > 2f)
				{
					for (int i = 0; i < 4; i++)
					{
						Vector2 bouncePos = Projectile.position;
						bouncePos -= Projectile.velocity * (i * 0.25f);
						Projectile.alpha = 255;
						Dust dust = Dust.NewDustDirect(bouncePos, 1, 1, DustID.RedTorch);
						dust.position = bouncePos;
						dust.scale = 1.5f;
						dust.velocity = Vector2.Zero;
						dust.noGravity = true;
					}
				}
			}
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox = Projectile.ai[1] >= 5f ? Projectile.Hitbox : Rectangle.Empty;
		}
	}
}