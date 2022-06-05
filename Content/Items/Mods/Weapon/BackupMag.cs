using DestinyMod.Common.Items.Modifiers;

namespace DestinyMod.Content.Items.Mods.Weapon
{
    public class BackupMag : ItemMod
    {
        public override void SetDefaults()
        {
            DisplayName = "Backup Mag";
            Description = "Increases magazine.";
            ApplyType = ItemType.Weapon;
        }
    }
}