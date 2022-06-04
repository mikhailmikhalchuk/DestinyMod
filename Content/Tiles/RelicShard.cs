using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using DestinyMod.Content.Items.Materials;

namespace DestinyMod.Content.Tiles
{
    public class RelicShard : ModTile
    {
		public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            MinPick = 50;
            MineResist = 2f;
            DustType = 144;
            ItemDrop = ModContent.ItemType<RelicIron>();
            HitSound = SoundID.Tink;
            //SoundStyle = 1;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Relic Shard");
            name.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Odłamek Reliktu");
            AddMapEntry(new Color(210, 105, 30), name);
        }
    }
}