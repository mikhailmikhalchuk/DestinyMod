using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
	public class SepiksPrimeBox : TileItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (Sepiks Prime)");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/SepiksPrime"), ModContent.ItemType<SepiksPrimeBox>(), ModContent.TileType<Tiles.MusicBoxes.SepiksPrimeBox>());
		}

		public override void DestinySetDefaults()
		{
			Item.createTile = ModContent.TileType<Tiles.MusicBoxes.SepiksPrimeBox>();
			Item.rare = ItemRarityID.LightRed;
			Item.value = 100000;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}
	}
}