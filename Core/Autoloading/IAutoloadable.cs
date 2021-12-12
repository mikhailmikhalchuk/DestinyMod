namespace TheDestinyMod.Core.Autoloading
{
	public interface IAutoloadable
	{
		void IAutoloadable_Load(IAutoloadable createdObject);

		void IAutoloadable_PostSetUpContent();

		void IAutoloadable_Unload();
	}
}