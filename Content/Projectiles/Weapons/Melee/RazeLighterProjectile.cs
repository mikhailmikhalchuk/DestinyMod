using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace DestinyMod.Content.Projectiles.Weapons.Melee
{
    public class RazeLighterProjectile : AnimatedSwordProjectile
    {
        public override void DestinySetDefaults()
        {
            FrameCycleSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Melee/RazeLighter");
        }

        public override Color? GetAlpha(Color lightColor) => new Color(219, 117, 61, 0) * (1f - Projectile.alpha / 255f);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
        }
    }
}