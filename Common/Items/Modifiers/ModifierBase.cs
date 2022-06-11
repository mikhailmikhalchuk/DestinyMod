using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.Items.Modifiers
{
    /// <summary>The base of all weapon modifiers (mods), such as <see cref="ItemPerk"/>.</summary>
    public abstract class ModifierBase
    {
        public virtual string Texture => (GetType().Namespace + "." + Name).Replace('.', '/');

        public int Type { get; internal set; }

        public string Name { get; internal set; }

        /// <summary>
        /// The display name of the modifier.
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// The description for the modifier.
        /// </summary>
        public string Description;

        /// <summary>
        /// Creates an instance of the specified type. Note that this will cause all update-related functions on the created type to run, so only create new instances that you will use.
        /// </summary>
        /// <typeparam name="T">The type (inheriting <see cref="ModifierBase"/>) to create an instance of.</typeparam>
        /// <returns>The created instance.</returns>
        public static T CreateInstanceOf<T>() where T : ModifierBase, new()
        {
            T reference = ModContent.GetInstance<T>();
            T outPut = new T
            {
                Type = reference.Type,
                Name = reference.Name
            };
            outPut.SetDefaults();
            return outPut;
        }

        public static int GetType<T>() where T : ModifierBase => ModContent.GetInstance<T>().Type;

        public static string GetName<T>() where T : ModifierBase => ModContent.GetInstance<T>().Name;

        public virtual void Load(ref string name) { }

        public abstract void SetDefaults();

        public virtual void SetItemDefaults(Item item) { }

        public virtual void Update(Player player) { }

        public virtual void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit) { }

        public virtual void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) { }

        public virtual void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit) { }

        /// <summary>
        /// Allows modification of the use speed of the item.
        /// </summary>
        /// <param name="player">The player using the item.</param>
        /// <param name="item">The item being used.</param>
        /// <param name="multiplier">The current multiplier. This is stacking with other perks on the item, and is unsorted - that is, this multiplier can be changed by other perks in the handler call.</param>
        public virtual void UseSpeedMultiplier(Player player, Item item, ref float multiplier)
        {
            multiplier *= 1f;
        }
    }
}
