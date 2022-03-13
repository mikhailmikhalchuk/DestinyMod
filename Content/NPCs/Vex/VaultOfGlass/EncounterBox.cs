using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DestinyMod.Common.NPCs;

namespace DestinyMod.Content.NPCs.Vex.VaultOfGlass
{
    public class EncounterBox : DestinyModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Encounter Box");
            NPCID.Sets.PositiveNPCTypesExcludedFromDeathTally[NPC.type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
                Hide = true,
            });
        }

        public override void DestinySetDefaults()
        {
            NPC.width = 68;
            NPC.height = 66;
            NPC.lifeMax = 500;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            //NPC.chaseable = false;

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // encounter start thing
        }

        public override void DrawEffects(ref Color drawColor)
        {
            Lighting.AddLight(NPC.Center, Color.LightCyan.ToVector3() * Main.essScale);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position - new Vector2(2), NPC.width + 4, NPC.height + 4, DustID.Electric, 0f, 0f, 100, default, 0.5f);
                dust.velocity *= 1.6f;
                dust.velocity.Y--;
                dust.position = Vector2.Lerp(dust.position, NPC.Center, 0.5f);
            }
        }
    }
}