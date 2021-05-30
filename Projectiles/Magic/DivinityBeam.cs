using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Magic
{
	public class DivinityBeam : ModProjectile
	{
		private const float MOVE_DISTANCE = 60f;

		private bool done;

		private static SoundEffectInstance fire;

		private static SoundEffectInstance start;

		public float Distance
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public override void SetDefaults() {
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.magic = true;
			projectile.hide = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], new Vector2(Main.player[projectile.owner].Center.X, Main.player[projectile.owner].Center.Y - 4),
				projectile.velocity, 10, projectile.damage, -1.57f, 1f, (int)MOVE_DISTANCE);
			return false;
		}

		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, int transDist = 50) {
			float r = unit.ToRotation() + rotation;

			for (float i = transDist; i <= Distance; i += step) {
				Color c = Color.White;
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			}

			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
			Player player = Main.player[projectile.owner];
			Vector2 unit = projectile.velocity;
			float point = 0f;

			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
				player.Center + unit * Distance, 22, ref point);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.immune[projectile.owner] = 5;
			target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Judgment>(), 120);
		}

		public override void AI() {
			projectile.localAI[1]++;
			Player player = Main.player[projectile.owner];
			projectile.position = player.Center + projectile.velocity * MOVE_DISTANCE;
			projectile.timeLeft = 2;
			if (!done && start == null) {
				start = mod.GetSound("Sounds/Item/DivinityStart").CreateInstance();
				start.Play();
				done = true;
			}
			else if (!done && start != null) {
				start.Play();
				done = true;
			}
			if (fire == null && start.State != SoundState.Playing) {
				fire = mod.GetSound("Sounds/Item/DivinityFire").CreateInstance();
				fire.IsLooped = true;
				fire.Play();
			}
			else if (fire != null && start.State != SoundState.Playing && fire.State == SoundState.Stopped) {
				fire.Play();
			}
			if (!player.channel || Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true)) {
				projectile.Kill();
				if (fire != null) {
					fire.Stop(true);
				}
				if (start != null) {
					start.Stop(true);
				}
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/DivinityStop"), projectile.Center);
			}

			if (projectile.owner == Main.myPlayer) {
				Vector2 diff = Main.MouseWorld - player.Center;
				diff.Normalize();
				projectile.velocity = diff;
				projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
				projectile.netUpdate = true;
			}
			int dir = projectile.direction;
			player.ChangeDir(dir);
			player.heldProj = projectile.whoAmI;
			player.itemTime = player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f) {
				var start = player.Center + projectile.velocity * Distance;
				NPC checkPC = Main.npc.FirstOrDefault(npc => npc.Hitbox.Contains(new Rectangle((int)start.X, (int)start.Y, 1, 1)));
				if (!Collision.CanHitLine(player.Center, 1, 1, start, 1, 1)) {
					Distance -= 5f;
					break;
				}
				else if (checkPC != null) {
					if (checkPC.active && !checkPC.friendly && checkPC.lifeMax > 5 && checkPC.damage > 0 || checkPC.active && checkPC.type == Terraria.ID.NPCID.TargetDummy) {
						break;
					}
				}
			}
			Vector2 unit = projectile.velocity * -1;
			Vector2 dustPos = player.Center + projectile.velocity * Distance;

			for (int i = 0; i < 2; ++i) {
				float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 226, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
			}

			if (Main.rand.NextBool(5)) {
				Vector2 offset = projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
				unit = dustPos - Main.player[projectile.owner].Center;
				unit.Normalize();
				Dust dust = Main.dust[Dust.NewDust(Main.player[projectile.owner].Center + 55 * unit, 8, 8, 226, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.noGravity = true;
				dust.scale = 0.5f;
			}
			if (projectile.localAI[1] > 1) {
				DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
				Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
			}
		}

		public override bool ShouldUpdatePosition() => false;

		public override void CutTiles() {
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
		}
	}
}