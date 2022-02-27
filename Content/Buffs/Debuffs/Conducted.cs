using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DestinyMod.Common.Buffs;
using Terraria.DataStructures;

namespace DestinyMod.Content.Buffs.Debuffs
{
    public class Conducted : DestinyModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conducted");
            Description.SetDefault("You are shocked");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public static void GenerateElectricDust(Entity entity)
		{
            Dust dust = Dust.NewDustDirect(entity.position - new Vector2(2), entity.width + 4, entity.height + 4, DustID.Electric, 0f, 0f, 100, default, 0.5f);
            dust.velocity *= 1.6f;
            dust.velocity.Y -= 1f;
            dust.position = Vector2.Lerp(dust.position, entity.Center, 0.5f);
        }

        public override void Update(Player player, ref int buffIndex) => GenerateElectricDust(player);

        public override void Update(NPC npc, ref int buffIndex) => GenerateElectricDust(npc);

		public override bool PreKill(Player player, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
            if (damage == 10 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + "'s energy was dispersed.");
            }
            return base.PreKill(player, damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
		}

        public override void UpdateBadLifeRegen(Player player) => ApplyDebuff(player, 4);

        public override void UpdateLifeRegen(NPC npc, ref int damage) => ApplyDebuff(npc, 4);
	}
}