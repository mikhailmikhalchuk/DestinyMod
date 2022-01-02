using Terraria.ModLoader;
using Terraria.UI;

namespace DestinyMod
{
	public abstract class DestinyModUIState : UIState
	{
		public string Name;

		protected bool AutoCreateInterface;

		public UserInterface UserInterface { get; private set; }

		public virtual void PreLoad()
		{
			Name = GetType().Name;
			AutoCreateInterface = true;
		}

		public virtual void Load(DestinyModUIState uiState)
		{
			UserInterface = new UserInterface();
			UserInterface.SetState(uiState);
		}
	}
}