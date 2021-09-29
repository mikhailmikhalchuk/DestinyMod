using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace TheDestinyMod.NPCs.Vex.VaultOfGlass
{
    public class Oracle : ModNPC
    {
        public override string Texture => "Terraria/NPC_" + NPCID.DemonEye;

        public override void SetDefaults() {
            npc.CloneDefaults(NPCID.DemonEye);
            aiType = 0;
            npc.value = 0;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.lifeMax = 200;
            npc.defense = 2;
            npc.noGravity = true;
            npc.dontTakeDamage = true;
            npc.knockBackResist = 0f;
            npc.chaseable = false;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit) {
            if (npc.life <= 0 && player.active) {
                Main.NewText($"[c/3EAD5C:{player.name}] has destroyed an Oracle");
                if (DestinyWorld.oraclesKilledOrder == 4 && DestinyWorld.oraclesTimesRefrained == 0 || DestinyWorld.oraclesKilledOrder == 6 && DestinyWorld.oraclesTimesRefrained == 1 || DestinyWorld.oraclesKilledOrder == 8 && DestinyWorld.oraclesTimesRefrained == 2) {
                    Main.NewText("The Oracles recognize their refrain");
                }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit) {
            if (npc.life <= 0 && Main.player[projectile.owner].active) {
                Main.NewText($"[c/3EAD5C:{Main.player[projectile.owner].name}] has destroyed an Oracle");
                if (DestinyWorld.oraclesKilledOrder == 4 && DestinyWorld.oraclesTimesRefrained == 0 || DestinyWorld.oraclesKilledOrder == 6 && DestinyWorld.oraclesTimesRefrained == 1 || DestinyWorld.oraclesKilledOrder == 8 && DestinyWorld.oraclesTimesRefrained == 2) {
                    Main.NewText("The Oracles recognize their refrain");
                }
            }
        }

        public override void NPCLoot() {
            if (DestinyWorld.oraclesKilledOrder == npc.ai[0]) {
                DestinyWorld.oraclesKilledOrder++;
            }
            else {
                Main.NewText("MARKED BY AN ORACLE!");
                foreach (Player player in Main.player) {
                    if (player.active && !player.GetModPlayer<DestinyPlayer>().markedForNegation) {
                        player.AddBuff(ModContent.BuffType<Buffs.Debuffs.MarkedForNegation>(), 1);
                    }
                }
                foreach (NPC npcE in Main.npc) {
                    if (npcE.type == npc.type) {
                        npcE.active = false;
                    }
                }
            }
        }
    }
}