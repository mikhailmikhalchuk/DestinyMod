using DestinyMod.Common.Items.Modifiers;

namespace DestinyMod.Content.Items.Mods
{
    public class NullMod : ItemMod
    {
        public override void SetDefaults()
        {
            DisplayName = "Empty Mod Socket";
            Description = "No mod currently selected.";
        }
    }
}