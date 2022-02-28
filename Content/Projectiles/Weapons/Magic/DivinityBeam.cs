using DestinyMod.Common.Projectiles;
using DestinyMod.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Weapons.Magic
{
	public class DivinityBeam : DestinyModProjectile
	{
		private SoundEffectInstance Fire; //thanks, solstice

		private SoundEffectInstance Start;

		public float Distance { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }

		public float Counter { get => Projectile.ai[1]; set => Projectile.ai[1] = value; }

		public bool Done { get => Projectile.localAI[0] != 0; set => Projectile.localAI[0] = value ? 1 : 0; }

		public override void DestinySetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			// projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.hide = true;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Player player = Main.player[Projectile.owner];
			DrawLaser(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, new Vector2(player.Center.X, player.Center.Y - 5),
				Projectile.velocity, 10, Projectile.damage, -MathHelper.PiOver2, 1f, 60);
			return false;
		}

		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, int transDist = 50)
		{
			float adjustedRotation = unit.ToRotation() + rotation;
			Vector2 origin = new Vector2(10, 13);

			for (float i = transDist; i <= Distance; i += step)
			{
				Vector2 drawPos = start + i * unit;
				spriteBatch.Draw(texture, drawPos - Main.screenPosition,
					new Rectangle(0, 26, 20, 26), i < transDist ? Color.Transparent : Color.White, adjustedRotation,
					origin, scale, 0, 0);
			}

			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 20, 26), Color.White, adjustedRotation, origin, scale, 0, 0);

			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 20, 26), Color.White, adjustedRotation, origin, scale, 0, 0);

		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Player player = Main.player[Projectile.owner];
			Vector2 collisionBox = new Vector2(player.Center.X, player.Center.Y - 5) + Projectile.velocity * (Distance + 10);
			float discard = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
				collisionBox, 22, ref discard);
		}

		public override void Kill(int timeLeft)
		{
			Fire?.Stop(true);
			Start?.Stop(true);
			SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/DivinityStop"), Projectile.Center);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[Projectile.owner] = 5;
			if (Counter > 120)
			{
				target.AddBuff(ModContent.BuffType<Judgment>(), 150);
				Counter = 0;
			}
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.position = player.Center + Projectile.velocity * 60;
			Projectile.timeLeft = 4;
			if (!Done && Start == null)
			{
				if (Main.soundVolume <= 0)
				{
					Start = SoundEngine.LegacySoundPlayer.PlaySound(SoundLoader.CustomSoundType, Style: SoundLoader.GetSoundSlot(Mod, "Sounds/Item/DivinityStart"));
					Start.Volume = 0;
					Start.Play();
				}
				else
				{
					Start = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/DivinityStart"), Projectile.Center);
				}
				Done = true;
			}
			else if (!Done && Start != null)
			{
				Start.Play();
				Done = true;
			}

			if (Fire == null && Start != null && Start.State != SoundState.Playing)
			{
				if (Main.soundVolume <= 0)
				{
					Fire = SoundEngine.LegacySoundPlayer.PlaySound(SoundLoader.CustomSoundType, Style: SoundLoader.GetSoundSlot(Mod, "Sounds/Item/DivinityFire"));
					Fire.IsLooped = true;
					Fire.Volume = 0;
					Fire.Play();
				}
				else
				{
					Fire = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/DivinityFire"), Projectile.Center);
				}
			}
			else if (Fire != null && Start.State != SoundState.Playing && Fire.State == SoundState.Stopped)
			{
				Fire.Play();
			}

			if (!player.channel && player.whoAmI == Main.myPlayer || Main.time % 10 == 0 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
			{
				Projectile.Kill();
			}

			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.velocity = Vector2.Normalize(Main.MouseWorld - player.Center);
				Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
				Projectile.netUpdate = true;
			}

			int dir = Projectile.direction;
			player.ChangeDir(dir);
			player.heldProj = Projectile.whoAmI;
			player.itemTime = player.itemAnimation = 4;
			player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir);
			for (Distance = 60; Distance <= 2200f; Distance += 5f)
			{
				Vector2 start = player.Center + Projectile.velocity * Distance;
				if (!Collision.CanHitLine(player.Center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					Counter = 0;
					break;
				}

				for (int npcCount = 0; npcCount < Main.maxNPCs; npcCount++)
				{
					NPC npc = Main.npc[npcCount];
					if (npc.active && !npc.townNPC && !npc.dontTakeDamage && npc.Hitbox.Contains((int)start.X, (int)start.Y))
					{
						Counter++;
						break;
					}
				}

				for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
				{
					if (playerCount == Main.myPlayer)
					{
						continue;
					}

					Player otherPlayer = Main.player[playerCount];

					if (otherPlayer.active && otherPlayer.team != player.team && otherPlayer.hostile && otherPlayer.Hitbox.Contains((int)start.X, (int)start.Y))
					{
						Counter++;
						break;
					}
				}
			}
			Vector2 dustPos = player.Center + Projectile.velocity * Distance;

			for (int i = 0; i < 2; i++)
			{
				float velInput = Projectile.velocity.ToRotation() + (Main.rand.NextBool() ? -MathHelper.PiOver2 : MathHelper.PiOver2);
				float velMultiplier = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(velInput) * velMultiplier, (float)Math.Sin(velInput) * velMultiplier);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Electric, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
			}

			if (Main.rand.NextBool(5))
			{
				Dust dust = Main.dust[Dust.NewDust(player.Center + 55 * Vector2.Normalize(dustPos - player.Center), 8, 8, DustID.Electric, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.noGravity = true;
				dust.scale = 0.5f;
			}

			if (++Projectile.localAI[1] > 1)
			{
				DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
				Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - 60), 26, DelegateMethods.CastLight);
			}
		}

		public override bool ShouldUpdatePosition() => false;

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - 60), (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}
	}
}