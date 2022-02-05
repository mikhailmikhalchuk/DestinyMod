using DestinyMod.Common.Buffs;

namespace DestinyMod.Content.Buffs
{
    public class AncientShard : DestinyModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Shard");
            Description.SetDefault("Enemy Mote drop chance increased");
        }
    }
}