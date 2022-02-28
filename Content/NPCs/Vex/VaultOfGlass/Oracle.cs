using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Common.NPCs;
using DestinyMod.Common.ModSystems;
using DestinyMod.Content.Buffs.Debuffs;

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

        public static void HandleOnHit(Player striker)
		{
            Main.NewText($"[c/3EAD5C:{striker.name}] has destroyed an Oracle");
            if (VaultOfGlassSystem.OraclesKilledOrder == 4 && VaultOfGlassSystem.OraclesTimesRefrained == 0
                || VaultOfGlassSystem.OraclesKilledOrder == 6 && VaultOfGlassSystem.OraclesTimesRefrained == 1
                || VaultOfGlassSystem.OraclesKilledOrder == 8 && VaultOfGlassSystem.OraclesTimesRefrained == 2)
            {
                Main.NewText("The Oracles recognize their refrain");
            }
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (NPC.life <= 0 && player.active)
            {
                HandleOnHit(player);
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (NPC.life <= 0 && player.active)
            {
                HandleOnHit(player);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (VaultOfGlassSystem.OraclesKilledOrder == NPC.ai[0])
            {
                VaultOfGlassSystem.OraclesKilledOrder++;
                return;
            }

            Main.NewText("MARKED BY AN ORACLE!");
            for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
            {
                Player player = Main.player[playerCount];
                if (player.active && !player.HasBuff<MarkedForNegation>())
                {
                    player.AddBuff(ModContent.BuffType<MarkedForNegation>(), 1);
                }
            }

            /*foreach (NPC npcE in Main.npc)
            {
                if (npcE.type == NPC.type)
                {
                    // npcE.active = false;
                }
            }*/
        }
    }
}