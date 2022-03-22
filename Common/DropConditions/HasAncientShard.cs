using DestinyMod.Content.Buffs;
using Terraria.GameContent.ItemDropRules;

namespace DestinyMod.Common.DropConditions
{
    public class HasAncientShard : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => info.player.HasBuff<AncientShard>() && !info.IsInSimulation;

        public bool CanShowItemDropInUI() => false;

        public string GetConditionDescription() => "Drops with the Ancient Shard buff";
    }
}