using System.Collections.Generic;
using Terraria;

namespace DestinyMod.Common.Items.Modifiers
{
    public abstract class ItemCatalyst : ModifierBase
    {
        /// <summary>
        /// Expect extensive usage of this function as ItemCatalyst logic will always be considered as to cleanly implement requirements
        /// </summary>
        /// <returns> Whether or not the catalyst's requirements have been completed and this catalyst is active</returns>
        public virtual bool IsCompleted => false;

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

        public static ItemCatalyst GetInstance(int type) => ModAndPerkLoader.ItemCatalysts[type];
    }
}
