using TheDestinyMod.Core.Autoloading;
using Terraria;
using Terraria.ModLoader;

namespace TheDestinyMod.Content.Autoloading.Misc
{
    public class MusicBoxes : IAutoloadable
    {
        public void IAutoloadable_Load(IAutoloadable createdObject)
        {
            if (Main.dedServ)
            {
                return;
            }

            TheDestinyMod mod = TheDestinyMod.Instance;
            mod.AddMusicBox(mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SepiksPrime"), ModContent.ItemType<Items.Placeables.MusicBoxes.SepiksPrimeBox>(), ModContent.TileType<Tiles.MusicBoxes.SepiksPrimeBox>());
        }

        public void IAutoloadable_PostSetUpContent() { }

        public void IAutoloadable_Unload() { }
    }
}