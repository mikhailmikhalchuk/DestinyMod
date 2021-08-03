using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class DeepFreeze : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Deep Freeze");
            Description.SetDefault("You are frozen!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.controlLeft = false;
            player.controlRight = false;
            player.controlUp = false;
            player.controlDown = false;
            player.controlHook = false;
            player.controlQuickHeal = false;
            player.controlQuickMana = false;
            player.controlJump = false;
            player.gravDir = 1f;
            player.pulley = false;
            if (player.mount.Active) {
                player.mount.Dismount(player);
            }
            player.velocity.Y += player.gravity;
            if (player.velocity.Y > player.maxFallSpeed) {
                player.velocity.Y = player.maxFallSpeed;
            }
            player.sandStorm = false;
            Dust.NewDust(player.Center, player.width, player.height, DustID.Clentaminator_Blue);
        }
    }
}