using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class Detained : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Detained");
            Description.SetDefault("You are trapped");
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.velocity.X = 0;
            player.velocity.Y = 0.000001f;
            player.gravity = 0f;
            player.controlJump = false;
            player.dashDelay = 10;
            if (player.buffTime[buffIndex] <= 1) {
                Terraria.DataStructures.PlayerDeathReason deathReason = new Terraria.DataStructures.PlayerDeathReason
                {
                    SourceCustomReason = player.name + "'s death was correctly predicted." //, SourceNPCIndex = AtheonTypeHere
                };
                player.KillMe(deathReason, 0, 0);
            }
        }
    }
}