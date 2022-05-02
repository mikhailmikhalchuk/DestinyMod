using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.Items.PerksAndMods
{
    public abstract class ModAndPerkBase
    {
        public virtual string Texture => (GetType().Namespace + "." + Name).Replace('.', '/');

        public int Type { get; internal set; }

        public string Name { get; internal set; }

        public string DisplayName;

        public string Description;

        public static T CreateInstanceOf<T>() where T : ModAndPerkBase, new()
        {
            T reference = ModContent.GetInstance<T>();
            T outPut = new T
            {
                Name = reference.Name
            };
            outPut.SetDefaults();
            return outPut;
        }

        public virtual void Load(ref string name) { }

        public abstract void SetDefaults();

        public virtual void Update(Player player) { }

        public virtual void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) { }
    }
}
