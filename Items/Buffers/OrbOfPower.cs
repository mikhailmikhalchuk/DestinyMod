using Terraria;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items.Buffers
{
    public class OrbOfPower : ModItem
    {
        public Player OrbOwner { get; set; }

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Orb of Power");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 19));
        }

        public override void SetDefaults() {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Gray;
        }

        public override bool ItemSpace(Player player) => true;

        public override bool CanPickup(Player player) {
            DestinyPlayer dPlayer = player.DestinyPlayer();
            return dPlayer.superChargeCurrent < 100 && dPlayer.superActiveTime == 0 && player != OrbOwner;
        }

        public override bool OnPickup(Player player) {
            var modPlayer = player.DestinyPlayer();
            modPlayer.superChargeCurrent += 4 + modPlayer.orbOfPowerAdd;
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/OrbOfPower"), player.Center);
            return false;
        }
    }
}