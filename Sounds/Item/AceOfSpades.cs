using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace DestinyMod.Sounds.Item
{
	public class AceOfSpades : ModSound
	{
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan)
        {
			soundInstance = Sound.Value.CreateInstance();
			soundInstance.Volume = volume * .8f;
			soundInstance.Pan = pan;
			return soundInstance;
		}
	}
}