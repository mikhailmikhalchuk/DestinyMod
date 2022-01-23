using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Buffs;

namespace DestinyMod.Content.Buffs
{
    public class HakkeBuff : DestinyModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hakke Craftsmanship");
            Description.SetDefault("Minor improvements to defense, movement speed, and ranged damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 2;
            player.moveSpeed += 0.15f;
            player.GetDamage(DamageClass.Ranged) += 0.1f;
        }
    }
}