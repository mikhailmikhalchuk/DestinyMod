using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Projectiles.Super
{
    public class HammerOfSol : ModProjectile
    {
        public override void SetDefaults() {
            projectile.width = 48;
            projectile.height = 46;
            projectile.damage = 50;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.timeLeft = 500;
        }

        public override Color? GetAlpha(Color lightColor) {
            return new Color(252, 148, 3, 0);
        }

        public override void AI() {
            projectile.rotation += 0.25f;
            projectile.velocity.Y += 0.35f;
            Lighting.AddLight(projectile.Center, Color.OrangeRed.ToVector3());
        }

        public override void Kill(int timeLeft) {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if (!target.friendly && target.damage > 0 && target.life <= 0) {
                Item.NewItem(Main.player[projectile.owner].Hitbox, ModContent.ItemType<Items.OrbOfPower>());
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Point checkTile = projectile.Center.ToTileCoordinates();
            bool youFailed = false;
            if (!WorldGen.EmptyTileCheck(checkTile.X, checkTile.X, checkTile.Y, checkTile.Y + 2)) {
                foreach (Projectile proj in Main.projectile) {
                    if (proj.type == ModContent.ProjectileType<Sunspot>()) {
                        if ((projectile.Center - proj.Center).Length() <= 200) {
                            youFailed = true;
                        }
                    }
                }
                if (!youFailed) {
                    Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y + 40), new Vector2(0, 0), ModContent.ProjectileType<Sunspot>(), 0, 0, projectile.owner);
                }
            }
            return true;
        }
    }

    public class Sunspot : ModProjectile
    {
        public override string Texture => "TheDestinyMod/Projectiles/Super/SunspotSummon";

        private int turnProgress = 1;

        private int frameSkip = 0;

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 60;
            projectile.height = 48;
            projectile.timeLeft = 500;
            projectile.damage = 0;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        public override void AI() {
            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<DestinyPlayer>().superActiveTime <= 0) {
                projectile.Kill();
                return;
            }
            if ((owner.Center - projectile.Center).Length() < 200 && !owner.HasBuff(ModContent.BuffType<Buffs.SunWarrior>())) {
                owner.AddBuff(ModContent.BuffType<Buffs.SunWarrior>(), 3);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            Texture2D texture = ModContent.GetTexture("TheDestinyMod/Projectiles/Super/SunspotSummon");
            if (frameSkip >= 2 && turnProgress <= 11 || frameSkip >= 5 && turnProgress == 12) {
                frameSkip = -1;
                turnProgress++;
            }
            else if (frameSkip >= 5 && turnProgress == 13) {
                frameSkip = -1;
                turnProgress--;
            }
            frameSkip++;
            if (turnProgress <= 11) {
                spriteBatch.Draw(texture, projectile.position - Main.screenPosition, new Rectangle(0, 48 * turnProgress, 60, 48), lightColor, projectile.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            else if (turnProgress >= 12) {
                spriteBatch.Draw(texture, projectile.position - Main.screenPosition, new Rectangle(0, 48 * (turnProgress - 2), 60, 48), lightColor, projectile.rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}