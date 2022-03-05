using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.NPCs;
using DestinyMod.Content.Buffs.Debuffs;

namespace DestinyMod.Content.NPCs.Vex.VaultOfGlass
{
    public class DetainmentBubble : DestinyModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Detainment Bubble");
            NPCID.Sets.PositiveNPCTypesExcludedFromDeathTally[NPC.type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
                Hide = true,
            });
        }

        public override void DestinySetDefaults()
        {
            NPC.width = 300;
            NPC.height = 300;
            NPC.lifeMax = 1000;
            NPC.defense = 5;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
        }

        public override void AI()
        {
            Player player = Main.player[(int)NPC.ai[0]];
            if (player.active && !player.dead && NPC.active)
            {
                NPC.Center = player.Center;
            }
            else if (player.dead && NPC.active)
            {
                // npc.active = false;
                NPC.life = 0;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            Player player = Main.player[(int)NPC.ai[0]];
            player.ClearBuff(ModContent.BuffType<Detained>());
        }
    }
}