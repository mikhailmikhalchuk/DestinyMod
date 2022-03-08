using DestinyMod.Content.Buffs;
using Terraria.GameContent.ItemDropRules;

namespace DestinyMod.Common.DropConditions
{
    public class HasAncientShard : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => info.player.HasBuff<AncientShard>();

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}