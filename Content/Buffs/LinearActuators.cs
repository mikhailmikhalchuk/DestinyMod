using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Buffs;

namespace DestinyMod.Content.Buffs
{
    public class LinearActuators : DestinyModBuff
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Linear Actuators");
            Description.SetDefault("300% increased damage for next melee strike");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
        }

        public void ImplementLinearActuators(Player player, ref int damage)
		{
            damage *= 4;
            player.ClearBuff(Type);
        }

		public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
            if (item.DamageType == DamageClass.Melee)
            {
                ImplementLinearActuators(player, ref damage);
            }
        }

		public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            if (proj.DamageType == DamageClass.Melee)
            {
                ImplementLinearActuators(player, ref damage);
            }
        }

		public override void ModifyHitPvp(Player player, Item item, Player target, ref int damage, ref bool crit)
		{
            if (item.DamageType == DamageClass.Melee)
            {
                ImplementLinearActuators(player, ref damage);
            }
        }

		public override void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit)
		{
            if (proj.DamageType == DamageClass.Melee)
            {
                ImplementLinearActuators(player, ref damage);
            }
        }
	}
}