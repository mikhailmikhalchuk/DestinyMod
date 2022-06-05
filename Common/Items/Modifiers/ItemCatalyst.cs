using System.Collections.Generic;
using Terraria;

namespace DestinyMod.Common.Items.Modifiers
{
    public abstract class ItemCatalyst : ModifierBase
    {
        /// <summary>
        /// Expect extensive usage of this function as ItemCatalyst logic will always be considered as to cleanly implement requirements
        /// </summary>
        /// <returns> Whether or not requirements are fulfilled and the catalyst is active</returns>
        public virtual bool IsUnlocked => false;

        /// <summary>
        /// Requirements will need to be handled individually on each derivative of ItemCatalyst.
        /// Because of this limitation -- which quite frankly, reduces complexity by a lot -- this thingo exists
        /// </summary>
        /// <returns>List of requirement data</returns>
        public virtual List<ItemCatalystRequirement> HandleRequirementMouseText() => null;

        public struct ItemCatalystRequirement
        {
            public string RequirementName;

            public float RequirementProgress;

            public ItemCatalystRequirement(string name, float progress)
            {
                RequirementName = name;
                RequirementProgress = progress;
            }
        }
    }
}
