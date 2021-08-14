using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Buffs.Debuffs
{
    public class Conducted : ModBuff
    {
        public override void SetDefaults() {
            DisplayName.SetDefault("Conducted");
            Description.SetDefault("You are shocked");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) {
            Dust dust = Dust.NewDustDirect(new Vector2(player.position.X - 2f, player.position.Y - 2f), player.width + 4, player.height + 4, DustID.Electric, 0f, 0f, 100, default, 0.5f);
            dust.velocity *= 1.6f;
            dust.velocity.Y -= 1f;
            dust.position = Vector2.Lerp(dust.position, player.Center, 0.5f);
        }

        public override void Update(NPC npc, ref int buffIndex) {
            Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, DustID.Electric, 0f, 0f, 100, default, 0.5f);
            dust.velocity *= 1.6f;
            dust.velocity.Y -= 1f;
            dust.position = Vector2.Lerp(dust.position, npc.Center, 0.5f);
        }
    }
}