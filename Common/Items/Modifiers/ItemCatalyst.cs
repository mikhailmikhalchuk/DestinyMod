using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Common.Items.Modifiers
{
    public abstract class ItemCatalyst : ModifierBase
    {
        /// <summary>
        /// Contains data for a catalyst requirement.
        /// </summary>
        public struct ItemCatalystRequirement
        {
            /// <summary>
            /// The name of the requirement, e.g. "Enemies Defeated"
            /// </summary>
            public string RequirementName;

            /// <summary>
            /// The requirement's progress. Should be set to data found within the catalyst.
            /// </summary>
            public float RequirementProgress;

            public ItemCatalystRequirement(string name, float progress)
            {
                RequirementName = name;
                RequirementProgress = progress;
            }
        }

        /// <summary>
        /// Whether or not the catalyst has been discovered; that is, it has been socketed in the weapon but is not active.
        /// </summary>
        public bool IsDiscovered;

        /// <summary>
        /// Whether or not the catalyst has been completed; that is, it is socketed in the weapon and is active.
        /// </summary>
        public bool IsCompleted;

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
        /// Defines catalyst progress mouse text.
        /// </summary>
        /// <returns>List of requirement data.</returns>
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
