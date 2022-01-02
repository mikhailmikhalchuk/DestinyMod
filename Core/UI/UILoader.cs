using System;
using System.Linq;
using Terraria.ModLoader;

namespace DestinyMod
{
	public class UILoader : ModSystem
	{
		public override void Load()
		{
			foreach (Type type in DestinyMod.Instance.Code.GetTypes().Where(t => t.IsSubclassOf(typeof(DestinyModUIState)) 
			&& !t.IsAbstract 
			&& t.GetConstructor(Type.EmptyTypes) != null))
			{
				DestinyModUIState uiState = Activator.CreateInstance(type) as DestinyModUIState;
				uiState.PreLoad();
			}
		}
	}
}