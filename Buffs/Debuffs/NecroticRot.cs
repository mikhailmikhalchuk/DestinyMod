using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class NecroticRot : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Necrotic Rot");
            Description.SetDefault("You are poisoned");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.DestinyPlayer().necroticRot = true;
        }

        public override void Update(NPC npc, ref int buffIndex) {
            npc.DestinyNPC().necroticRot = true;
        }
    }
}