using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DestinyMod.Common.Buffs;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.GlobalNPCs;
using Terraria.DataStructures;
using DestinyMod.Content.Items.Buffers;
using Terraria.ModLoader;

namespace DestinyMod.Content.Buffs.Debuffs
{
    public class NecroticRot : DestinyModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrotic Rot");
            Description.SetDefault("You are poisoned");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

		public override void UpdateBadLifeRegen(Player player)
		{
            DebuffPlayer debuffPlayer = player.GetModPlayer<DebuffPlayer>();
            if (debuffPlayer.NecroticApplier != null)
            {
                ApplyDebuff(player, 40 + (debuffPlayer.NecroticApplier.HasBuff(ModContent.BuffType<MarkOfTheDevourer>()) ? 16 : 0));
            }
        }

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
            DebuffNPC debuffNPC = npc.GetGlobalNPC<DebuffNPC>();
            if (debuffNPC.NecroticApplier != null)
            {
                ApplyDebuff(npc, 40 + (debuffNPC.NecroticApplier.HasBuff(ModContent.BuffType<MarkOfTheDevourer>()) ? 16 : 0));
            }
        }

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
            drawColor = Color.ForestGreen;
            if (Main.rand.NextBool(10))
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.JungleSpore);
                dust.noGravity = true;
            }
        }

		public override bool PreKill(Player player, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
            if (damage == 10 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + "'s cells failed them.");
            }

            return base.PreKill(player, damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
		}

		public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
            DebuffNPC debuffNPC = npc.GetGlobalNPC<DebuffNPC>();
            if (damage >= npc.life && hitDirection == 0 && npc.damage > 0 && !npc.friendly)
            {
                ThornRemnant thornRemnant = Main.item[Item.NewItem(npc.GetSource_Loot(), npc.Hitbox, ModContent.ItemType<ThornRemnant>())].ModItem as ThornRemnant;
                thornRemnant.RemnantOwner = debuffNPC.NecroticApplier;
            }

            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
		}
	}
}