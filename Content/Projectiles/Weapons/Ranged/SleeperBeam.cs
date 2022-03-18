using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Projectiles;
using Microsoft.Xna.Framework.Audio;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class SleeperBeam : DestinyModProjectile
	{
		public SoundEffectInstance ChargeSound;

		public bool Fired;

		public override void DestinySetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.penetrate = 3;
			Projectile.DamageType = DamageClass.Ranged;
		}

		public override void Kill(int timeLeft)
		{
			if (!Fired)
			{
				ChargeSound?.Stop(true);
				ChargeSound = null;
				SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/FusionRifleRelease"), Projectile.Center);
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
			if (--Projectile.penetrate <= 0)
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
			return false;
		}

		public override void AI()
		{
			Projectile.localAI[1]++;
			if (Projectile.ai[1] == 4f)
            {
				if (ChargeSound == null)
				{
					if (Main.soundVolume <= 0)
					{
						ChargeSound = SoundEngine.LegacySoundPlayer.PlaySound(SoundLoader.CustomSoundType, Style: SoundLoader.GetSoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/VexMythoclastStart"));
						ChargeSound.Volume = 0;
						ChargeSound.Play();
					}
					else
					{
						ChargeSound = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/FusionRifleCharge"), Projectile.Center);
					}
				}

				Player player = Main.player[Projectile.owner];
				Projectile.position = player.Center + Projectile.velocity;

				if (Projectile.owner == Main.myPlayer)
				{
					Vector2 difference = Vector2.Normalize(Main.MouseWorld - player.Center);
					Projectile.velocity = difference;
					Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
					Projectile.netUpdate = true;
				}

				//Dust dust = Dust.NewDustDirect(player, player., 1, DustID.RedTorch);
				//dust.velocity *= 0f;
				//dust.noGravity = true;
				//dust.scale = 0.01f * Projectile.localAI[1];

				int dir = Projectile.direction;
				player.ChangeDir(dir);
				player.heldProj = Projectile.whoAmI;
				player.itemAnimation = player.itemTime = 2;
				player.itemRotation = (Projectile.velocity * dir).ToRotation();

				if (ChargeSound != null)
				{
					if (ChargeSound.State == SoundState.Stopped)
					{
						SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Weapons/Ranged/FusionRifleFire"), Projectile.Center);
						Fired = true;
						ChargeSound?.Stop();
						ChargeSound = null;
						player.channel = false;

						for (int i = 0; i < 3; i++)
                        {
							Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), player.Center, 10 * Projectile.velocity * 2f + new Vector2(Main.rand.Next(-15, 16) * 0.2f), ModContent.ProjectileType<SleeperBeam>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 0, 5);
						}

						Projectile.Kill();
						player.itemAnimation = player.itemTime = 15;
					}
					else if (!player.channel && ChargeSound.State == SoundState.Playing)
					{
						Projectile.Kill();
					}
				}
			}

			if (Projectile.ai[1] == 5f)
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
						dust.scale = Main.rand.Next(70, 110) * 0.013f;
						dust.velocity *= 0.2f;
						dust.noGravity = true;
					}
				}
			}
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox = Projectile.ai[1] == 5f ? Projectile.Hitbox : Rectangle.Empty;
		}
	}
}