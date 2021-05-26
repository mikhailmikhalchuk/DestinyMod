using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Tiles
{
    public class RelicShard : ModTile
    {
        public override void SetDefaults() {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 280;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            minPick = 50;
            mineResist = 2f;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Relic Shard");
            name.AddTranslation(GameCulture.Polish, "Odłamek Reliktu");
            AddMapEntry(new Color(210, 105, 30), name);
            dustType = 84;
            drop = ModContent.ItemType<RelicIron>();
            soundType = SoundID.Tink;
            soundStyle = 1;
        }
    }
}