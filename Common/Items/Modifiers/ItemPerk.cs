using System;

namespace DestinyMod.Common.Items.Modifiers
{
    public abstract class ItemPerk : ModifierBase
    {
        public const int TextureSize = 34;

        public virtual bool IsInstanced => false;

        public static ItemPerk GetInstance(int type) => ModAndPerkLoader.ItemPerks[type];

        public static ItemPerk GetInstance(string name) => ModAndPerkLoader.ItemPerksByName.TryGetValue(name, out ItemPerk perk) ? perk : null;

        public static ItemPerk CreateInstance(string name)
        {
            ItemPerk reference = GetInstance(name);

            if (reference == null)
            {
                return null;
            }

            ItemPerk outPut = Activator.CreateInstance(reference.GetType()) as ItemPerk;
            outPut.Type = reference.Type;
            outPut.Name = reference.Name;
            outPut.SetDefaults();
            return outPut;
        }
    }
}
