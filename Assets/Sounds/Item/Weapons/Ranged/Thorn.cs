using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace DestinyMod.Assets.Sounds.Item.Weapons.Ranged
{
	public class Thorn : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan)
		{
			soundInstance = Sound.Value.CreateInstance();
			soundInstance.Volume = volume * .5f;
			soundInstance.Pan = pan;
			return soundInstance;
		}
	}
}