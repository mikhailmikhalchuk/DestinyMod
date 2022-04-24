namespace DestinyMod.Common.Items.PerksAndMods
{
    public abstract class ItemPerk
    {
        public const int TextureSize = 34;

        public virtual string Texture => (GetType().Namespace + "." + Name).Replace('.', '/');

        public string Name { get; internal set; }

        public string DisplayName;

        public string Description;

        public virtual void Load(ref string name) { }

        public abstract void SetDefaults();
    }
}
