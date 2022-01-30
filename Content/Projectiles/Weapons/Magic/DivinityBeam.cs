using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using DestinyMod.Common.Projectiles;

namespace DestinyMod.Content.Projectiles.Weapons.Melee
{
	public class DivinityBeam : DestinyModProjectile
	{
		private bool done;

		private int counter;

		private static SoundEffectInstance fire; //thanks, solstice // WHO :Angery:

		private static SoundEffectInstance start;

		public float Distance
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

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
			DrawLaser(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, new Vector2(player.Center.X, player.Center.Y - 4),
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
			float point = 0f;
			Player player = Main.player[Projectile.owner];
			Vector2 collisionBox = new Vector2(player.Center.X, player.Center.Y - 4) + (Distance + 10) * Projectile.velocity;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), collisionBox,
				new Vector2(collisionBox.X + 22, collisionBox.Y + 26), 22, ref point);
		}

		public override void Kill(int timeLeft)
		{
			fire?.Stop(true);
			start?.Stop(true);
			SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/DivinityStop"), Projectile.Center);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[Projectile.owner] = 5;
			if (counter > 120)
			{
				target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Judgment>(), 150);
				counter = 0;
			}
		}

		public override void AI()
		{
			Projectile.localAI[1]++;
			Player player = Main.player[Projectile.owner];
			Projectile.position = player.Center + Projectile.velocity * 60;
			Projectile.timeLeft = 4;
			if (!done && start == null)
			{
				if (Main.soundVolume <= 0)
				{
					start = SoundLoader.GetLegacySoundSlot("Sounds/Item/DivinityStart").GetRandomSound().CreateInstance();
					start.Volume = 0;
					start.Play();
				}
				else
				{
					start = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/DivinityStart"), Projectile.Center);
				}
				done = true;
			}
			else if (!done && start != null)
			{
				start.Play();
				done = true;
			}

			if (fire == null && start.State != SoundState.Playing)
			{
				if (Main.soundVolume <= 0)
				{
					fire = SoundLoader.GetLegacySoundSlot("Sounds/Item/DivinityFire").GetRandomSound().CreateInstance();
					fire.IsLooped = true;
					fire.Volume = 0;
					fire.Play();
				}
				else
				{
					fire = SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("Sounds/Item/DivinityFire"), Projectile.Center);
				}
			}
			else if (fire != null && start.State != SoundState.Playing && fire.State == SoundState.Stopped)
			{
				fire.Play();
			}

			if (!player.channel && player.whoAmI == Main.myPlayer || Main.time % 10 == 0 && !player.CheckMana(player.inventory[player.selectedItem].mana, true) && player.whoAmI == Main.myPlayer)
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
				var start = player.Center + Projectile.velocity * Distance;
				if (!Collision.CanHitLine(player.Center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					counter = 0;
					break;
				}
				if (Main.npc.Any(npc => npc.active && npc.Hitbox.Intersects(new Rectangle((int)start.X, (int)start.Y, 1, 1)) && !npc.townNPC && !npc.dontTakeDamage) || Main.player.Any(playeR => playeR.active && playeR.Hitbox.Intersects(new Rectangle((int)start.X, (int)start.Y, 1, 1)) && playeR.team != player.team && playeR.hostile))
				{
					counter++;
					break;
				}
			}
			Vector2 dustPos = player.Center + Projectile.velocity * Distance;

			for (int i = 0; i < 2; ++i)
			{
				float num1 = Projectile.velocity.ToRotation() + (Main.rand.NextBool() ? -MathHelper.PiOver2 : MathHelper.PiOver2);
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Electric, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
			}

			if (Main.rand.NextBool(5))
			{
				Vector2 unit = dustPos - player.Center;
				unit.Normalize();
				Dust dust = Main.dust[Dust.NewDust(player.Center + 55 * unit, 8, 8, DustID.Electric, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.noGravity = true;
				dust.scale = 0.5f;
			}
			if (Projectile.localAI[1] > 1)
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