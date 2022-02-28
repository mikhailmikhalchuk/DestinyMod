using Terraria;
using DestinyMod.Common.Buffs;

namespace DestinyMod.Content.Buffs.Debuffs
{
    public class Detained : DestinyModBuff
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Detained");
            Description.SetDefault("You are trapped");
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity.X = 0;
            player.velocity.Y = 0.000001f;
            player.gravity = 0f;
            player.controlJump = false;
            player.dashDelay = 10;

            if (player.buffTime[buffIndex] <= 1) 
            {
                player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("'s death was correctly predicted."), 0, 0);
            }
        }
    }
}