using Microsoft.Xna.Framework;
using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
    public class VexMythoclast : Gun
    {
        private int cooldown;

        private bool alt;

        public override void SetStaticDefaults() => Tooltip.SetDefault("Kills with this weapon grant stacks of Overcharge"
            + "\nRight-click with Overcharge to switch firing modes"
            + "\nHold down the trigger in the alternative firing mode to fire a more powerful shot"
            + "\n\"...a causal loop within the weapon's mechanism, suggesting that the firing process somehow binds space and time into...\"");

        public override void DestinySetDefaults()
        {
            Item.damage = 85;
            Item.autoReuse = true;
            Item.channel = true;
            Item.rare = ItemRarityID.Yellow;
            Item.knockBack = 0;
            Item.useTime = 18;
            Item.crit = 10;
            Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/VexMythoclast"); //thanks, fillinek // WHO :angery:
            Item.useAnimation = 18;
            Item.value = Item.buyPrice(gold: 1);
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<VexChargeBullet>();
                damage *= 2;
            }
            else
            {
                Item.autoReuse = true;
                type = ModContent.ProjectileType<VexBullet>();
            }
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 2), velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !alt && cooldown <= 0 && player.DestinyPlayer().overcharged)
            {
                alt = true;
                // item.autoReuse = false;
                Item.UseSound = null;
                SoundEngine.PlaySound(SoundID.Item101);
                cooldown = 15;
                Item.color = Color.LightPink;
            }
            else if (player.altFunctionUse == 2 && alt && cooldown <= 0)
            {
                alt = false;
                Item.autoReuse = true;
                Item.UseSound = SoundLoader.GetLegacySoundSlot(TheDestinyMod.Instance, "Sounds/Item/VexMythoclast");
                SoundEngine.PlaySound(SoundID.Item101);
                cooldown = 15;
                Item.color = default;
            }
            return player.altFunctionUse != 2;
        }

        public override void UpdateInventory(Player player)
        {
            if ((Main.LocalPlayer.DestinyPlayer().overchargeStacks <= 0 || !Main.LocalPlayer.DestinyPlayer().overcharged) && alt)
            {
                alt = false;
                Main.LocalPlayer.ClearBuff(ModContent.BuffType<Buffs.Overcharge>());
                Item.color = default;
            }
            if (cooldown > 0)
                cooldown--;
        }


        public override bool AltFunctionUse(Player player) => true;

        public override Vector2? HoldoutOffset() => new Vector2(-3, -2);
    }

    public class VexChargeBullet : ModProjectile
    {
        private bool fired;

        private static SoundEffectInstance charge;

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bullet");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            // projectile.tileCollide = false;
            Projectile.hide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return fired;
        }

        public override void AI()
        {
            if (charge == null && !fired)
            {
                charge = SoundLoader.GetLegacySoundSlot("Sounds/Item/VexMythoclastStart").GetRandomSound().CreateInstance();
                charge.Play();
            }
            Player player = Main.player[Projectile.owner];
            if (!fired)
            {
                Projectile.position = player.Center + Projectile.velocity;
                int dir = Projectile.direction;
                player.ChangeDir(dir);
                player.heldProj = Projectile.whoAmI;
                player.itemAnimation = player.itemTime = 2;
                player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir);
            }
            if (Projectile.owner == Main.myPlayer && !fired)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }
            if (charge != null)
            {
                if (charge.State == SoundState.Stopped && !fired)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(TheDestinyMod.Instance, "Sounds/Item/VexMythoclastFire"), Projectile.Center);
                    fired = true;
                    charge?.Stop();
                    charge = null;
                    // Main.player[projectile.owner].channel = false;
                    Vector2 perturbedSpeed = (10 * Projectile.velocity * 2f);
                    Projectile.velocity = perturbedSpeed;
                    Projectile.tileCollide = true;
                    // projectile.hide = false;
                    Main.player[Projectile.owner].DestinyPlayer().overchargeStacks--;
                }
                else if (!player.channel && charge.State == SoundState.Playing && !fired && player.whoAmI == Main.myPlayer)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 10);
        }

        public override void Kill(int timeLeft)
        {
            if (!fired)
            {
                charge?.Stop();
                charge = null;
                // Main.player[projectile.owner].channel = false;
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (!fired)
            {
                hitbox = Rectangle.Empty;
            }
        }
    }

    public class VexBullet : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G * 0.7f, lightColor.B * 0.1f, lightColor.A);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.damage > 0 && target.life <= 0)
            {
                DestinyPlayer player = Main.player[Projectile.owner].DestinyPlayer();
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 2);
                if (player.overchargeStacks < 3)
                    player.overchargeStacks++;
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target.statLife <= 0)
            {
                DestinyPlayer player = Main.player[Projectile.owner].DestinyPlayer();
                Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.Overcharge>(), 2);
                if (player.overchargeStacks < 3)
                    player.overchargeStacks++;
            }
        }
    }
}