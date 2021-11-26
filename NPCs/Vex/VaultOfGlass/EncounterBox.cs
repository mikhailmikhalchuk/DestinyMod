using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDestinyMod.NPCs.Vex.VaultOfGlass
{
    public class EncounterBox : ModNPC
    {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Encounter Box");
            NPCID.Sets.ExcludedFromDeathTally[npc.type] = true;
        }

        public override void SetDefaults() {
            npc.damage = 0;
            npc.width = 68;
            npc.height = 66;
            npc.lifeMax = 500;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
            npc.chaseable = false;
        }

        public override void NPCLoot() {
            //encounter start thing
        }

        public override void DrawEffects(ref Color drawColor) {
            Lighting.AddLight(npc.Center, Color.LightCyan.ToVector3() * Main.essScale);
        }

        public override void HitEffect(int hitDirection, double damage) {
            for (int i = 0; i < 5; i++) {
                Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, DustID.Electric, 0f, 0f, 100, default, 0.5f);
                dust.velocity *= 1.6f;
                dust.velocity.Y -= 1f;
                dust.position = Vector2.Lerp(dust.position, npc.Center, 0.5f);
            }
        }
    }
}