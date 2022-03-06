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

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            // npc.chaseable = false;
        }

        public override void AI()
        {
            if (NPC.ai[0] < 1)
            {
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
            }
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (NPC.life <= 0 && player.active)
            {
                Main.NewText($"[c/3EAD5C:{player.name}] has destroyed an Oracle");
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (NPC.life <= 0 && player.active)
            {
                Main.NewText($"[c/3EAD5C:{player.name}] has destroyed an Oracle");
            }
        }

        public override void OnKill()
        {
            if (VaultOfGlassSystem.NextOracleToKillInOrder == NPC.ai[0])
            {
                VaultOfGlassSystem.NextOracleToKillInOrder++;
                if (VaultOfGlassSystem.NextOracleToKillInOrder == 4 && VaultOfGlassSystem.OracleStage == 0 ||
                    VaultOfGlassSystem.NextOracleToKillInOrder == 6 && VaultOfGlassSystem.OracleStage == 1 ||
                    VaultOfGlassSystem.NextOracleToKillInOrder == 8 && VaultOfGlassSystem.OracleStage == 2)
                {
                    Main.NewText("The Oracles recognize their refrain");
                    VaultOfGlassSystem.ResetOracles(true);
                }
                return;
            }

            VaultOfGlassSystem.FailOracles();
        }
    }
}