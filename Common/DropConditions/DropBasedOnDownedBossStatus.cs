using DestinyMod.Content.Buffs;
using System.Linq;
using Terraria.GameContent.ItemDropRules;
using DestinyMod.Common.ModSystems;

namespace DestinyMod.Common.DropConditions
{
    public class DropBasedOnDownedBossStatus : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !NPCIOSystem.DownedBoss.Any(downedBossData => downedBossData.Type == info.npc.type) && !info.IsInSimulation;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => "Drops on first kill";
    }
}