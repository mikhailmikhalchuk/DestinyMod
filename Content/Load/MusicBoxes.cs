using DestinyMod.Content.Items.Bosses.SepiksPrime;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.Load
{
    public class MusicBoxes : ILoadable
    {
        public void Load(Mod mod)
        {
            if (Main.dedServ)
            {
                return;
            }

            //MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot("Sounds/Music/SepiksPrime"), ModContent.ItemType<SepiksPrimeBox>(), ModContent.TileType<Tiles.MusicBoxes.SepiksPrimeBox>());
        }

        public void Unload() { }
    }
}