using Terraria.Localization;

namespace DestinyMod.Common.Utils
{
	public static class LanguageUtils
	{
		public static GameCulture Chinese => GameCulture.FromCultureName(GameCulture.CultureName.Chinese);

		public static GameCulture English => GameCulture.FromCultureName(GameCulture.CultureName.English);

		public static GameCulture French => GameCulture.FromCultureName(GameCulture.CultureName.French);

		public static GameCulture German => GameCulture.FromCultureName(GameCulture.CultureName.German);

		public static GameCulture Italian => GameCulture.FromCultureName(GameCulture.CultureName.Italian);

		public static GameCulture Polish => GameCulture.FromCultureName(GameCulture.CultureName.Polish);

		public static GameCulture Portuguese => GameCulture.FromCultureName(GameCulture.CultureName.Portuguese);

		public static GameCulture Russian => GameCulture.FromCultureName(GameCulture.CultureName.Russian);

		public static GameCulture Mirsario => Russian;

		public static GameCulture Spanish => GameCulture.FromCultureName(GameCulture.CultureName.Spanish);

		public static GameCulture Unknown => GameCulture.FromCultureName(GameCulture.CultureName.Unknown);
	}
}