using Terraria.ModLoader;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.Items;

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

    public class BackupMagGranter : DestinyModItem, IItemModGranter
    {
        public override string Texture => "DestinyMod/Content/Items/Mods/Weapon/BackupMag";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Backup Mag");
            Tooltip.SetDefault("Increases magazine.");
        }

        public override void DestinySetDefaults()
        {
            Item.value = 30000;
        }

        public int ItemModType() => ModifierBase.GetType<BackupMag>();

        public string ItemModName() => "Backup Mag";
    }
}