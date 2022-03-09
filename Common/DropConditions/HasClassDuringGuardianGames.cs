using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.ModSystems;
using Terraria.GameContent.ItemDropRules;

namespace DestinyMod.Common.DropConditions
{
    public class HasClassDuringGuardianGames : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => GuardianGamesSystem.Active && info.player.GetModPlayer<ClassPlayer>().ClassType != DestinyClassType.None;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}