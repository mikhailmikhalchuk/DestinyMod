using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.ModSystems;
using Terraria.GameContent.ItemDropRules;

namespace DestinyMod.Content.DropRules
{
    public class HasClassDuringGuardianGames : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => GuardianGames.Active && info.player.GetModPlayer<ClassPlayer>().ClassType != DestinyClassType.None;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}