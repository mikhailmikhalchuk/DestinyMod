using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace TheDestinyMod.Sounds.Item
{
	public class AceOfSpades : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type) {
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * .8f;
			soundInstance.Pan = pan;
			return soundInstance;
		}
	}
}