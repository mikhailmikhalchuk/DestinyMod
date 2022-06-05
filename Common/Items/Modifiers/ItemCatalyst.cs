using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.Items.Modifiers
{
    public abstract class ItemCatalyst : ModifierBase
    {
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

        public bool IsDiscovered;

        public bool IsCompleted;

        /// <summary>
        /// Expect extensive usage of this function as ItemCatalyst logic will always be considered as to cleanly implement requirements
        /// </summary>
        /// <returns> Whether or not the catalyst's requirements have been completed and this catalyst is active</returns>
        // public virtual bool IsCompleted => false;

        public TagCompound Save()
        {
            TagCompound savedData = new TagCompound
            {
                { "Discovered", IsDiscovered },
                { "Completed", IsCompleted },
            };

            TagCompound instancedData = new TagCompound();
            SaveInstance(instancedData);
            savedData.Add("Data", instancedData);
            return savedData;
        }

        public virtual void SaveInstance(TagCompound tagCompound) { }

        public void Load(TagCompound tag)
        {
            IsDiscovered = tag.Get<bool>("Discovered");
            IsCompleted = tag.Get<bool>("Completed");
            TagCompound instancedData = tag.Get<TagCompound>("Data");
            LoadInstance(instancedData);
        }

        public virtual void LoadInstance(TagCompound tag) { }

        /// <summary>
        /// Requirements will need to be handled individually on each derivative of ItemCatalyst.
        /// Because of this limitation -- which quite frankly, reduces complexity by a lot -- this thingo exists
        /// </summary>
        /// <returns>List of requirement data</returns>
        public virtual List<ItemCatalystRequirement> HandleRequirementMouseText() => null;

        public static ItemCatalyst GetInstance(int type) => ModAndPerkLoader.ItemCatalysts[type];

        public static ItemCatalyst GetInstance(string name) => ModAndPerkLoader.ItemCatalystsByName.TryGetValue(name, out ItemCatalyst catalyst) ? catalyst : null;

        public static ItemCatalyst CreateInstance(string name)
        {
            ItemCatalyst reference = GetInstance(name);

            if (reference == null)
            {
                return null;
            }

            ItemCatalyst outPut = Activator.CreateInstance(reference.GetType()) as ItemCatalyst;
            outPut.Type = reference.Type;
            outPut.Name = reference.Name;
            outPut.SetDefaults();
            return outPut;
        }
    }
}
