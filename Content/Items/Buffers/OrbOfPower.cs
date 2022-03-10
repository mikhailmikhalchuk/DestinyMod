using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Audio;
using DestinyMod.Common.Items;
using DestinyMod.Common.ModPlayers;

namespace DestinyMod.Content.Items.Buffers
{
    public class OrbOfPower : DestinyModItem
    {
        public Player OrbOwner { get; set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Power");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 19));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void DestinySetDefaults()
        {
            Item.height = 16;
            Item.rare = ItemRarityID.Gray;
        }

        public override bool ItemSpace(Player player) => true;

        public override bool CanPickup(Player player)
        {
            SuperPlayer superPlayer = player.GetModPlayer<SuperPlayer>();
            return superPlayer.SuperChargeCurrent < 100 && superPlayer.SuperActiveTime == 0 && player != OrbOwner;
        }

        public override bool OnPickup(Player player)
        {
            SuperPlayer superPlayer = player.GetModPlayer<SuperPlayer>();
            superPlayer.SuperChargeCurrent += 4 + superPlayer.OrbOfPowerAdd;
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Item/Buffers/OrbOfPower"), player.Center);
            return false;
        }
    }
}