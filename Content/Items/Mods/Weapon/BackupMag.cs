using Terraria.ModLoader;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.Items.ItemTypes;

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

    public class BackupMagGranter : ModItem, IItemModGranter
    {
        public override string Texture => "DestinyMod/Content/Items/Mods/Weapon/BackupMag";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Backup Mag");
            Tooltip.SetDefault("Increases magazine.");
        }

        public int ItemModType() => ModifierBase.GetType<BackupMag>();

        public string ItemModName() => "Backup Mag";
    }
}