using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.NPCs;

namespace DestinyMod.Content.NPCs.Vex.VaultOfGlass
{
    public class Oracle : DestinyModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.DemonEye;

        public override void DestinySetDefaults()
        {
            NPC.CloneDefaults(NPCID.DemonEye);
            AIType = 0;
            NPC.value = 0;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.lifeMax = 200;
            NPC.defense = 2;
            NPC.noGravity = true;
            NPC.dontTakeDamage = true;
            NPC.knockBackResist = 0f;
            // npc.chaseable = false;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (NPC.life <= 0 && player.active)
            {
                Main.NewText($"[c/3EAD5C:{player.name}] has destroyed an Oracle");
                if (DestinyWorld.oraclesKilledOrder == 4 && DestinyWorld.oraclesTimesRefrained == 0 || DestinyWorld.oraclesKilledOrder == 6 && DestinyWorld.oraclesTimesRefrained == 1 || DestinyWorld.oraclesKilledOrder == 8 && DestinyWorld.oraclesTimesRefrained == 2)
                {
                    Main.NewText("The Oracles recognize their refrain");
                }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (NPC.life <= 0 && Main.player[projectile.owner].active)
            {
                Main.NewText($"[c/3EAD5C:{Main.player[projectile.owner].name}] has destroyed an Oracle");
                if (DestinyWorld.oraclesKilledOrder == 4 && DestinyWorld.oraclesTimesRefrained == 0 || DestinyWorld.oraclesKilledOrder == 6 && DestinyWorld.oraclesTimesRefrained == 1 || DestinyWorld.oraclesKilledOrder == 8 && DestinyWorld.oraclesTimesRefrained == 2)
                {
                    Main.NewText("The Oracles recognize their refrain");
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (DestinyWorld.oraclesKilledOrder == NPC.ai[0])
            {
                DestinyWorld.oraclesKilledOrder++;
            }
            else
            {
                Main.NewText("MARKED BY AN ORACLE!");
                foreach (Player player in Main.player)
                {
                    if (player.active && !player.DestinyPlayer().markedForNegation)
                    {
                        player.AddBuff(ModContent.BuffType<Buffs.Debuffs.MarkedForNegation>(), 1);
                    }
                }
                foreach (NPC npcE in Main.npc)
                {
                    if (npcE.type == NPC.type)
                    {
                        // npcE.active = false;
                    }
                }
            }
        }
    }
}