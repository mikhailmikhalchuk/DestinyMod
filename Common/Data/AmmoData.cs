using Terraria.ModLoader.IO;

namespace DestinyMod.Common.Data
{
    /// <summary>Represents data for ammo.</summary>
    public struct AmmoData
    {
        /// <summary>
        /// The type of projectile used for this ammo.
        /// </summary>
        public int ProjectileType;

        /// <summary>
        /// The speed of this ammo.
        /// </summary>
        public float Speed;

        /// <summary>
        /// The damage of this ammo.
        /// </summary>
        public int Damage;

        /// <summary>
        /// The knockback of this ammo.
        /// </summary>
        public float Knockback;

        /// <summary>
        /// The item that corresponds to this ammo.
        /// </summary>
        public int AmmoItemID;

        public AmmoData(int projectileType, float speed, int damage, float knockBack, int ammoItemID)
        {
            ProjectileType = projectileType;
            Speed = speed;
            Damage = damage;
            Knockback = knockBack;
            AmmoItemID = ammoItemID;
        }

        public TagCompound Save() => new TagCompound()
        {
            { "ProjectileType", ProjectileType },
            { "Speed", Speed },
            { "Damage", Damage },
            { "Knockback", Knockback },
            { "AmmoItemID", AmmoItemID }
        };

        public static AmmoData Load(TagCompound tagCompound) => new AmmoData(
            tagCompound.Get<int>("ProjectileType"),
            tagCompound.Get<float>("Speed"),
            tagCompound.Get<int>("Damage"),
            tagCompound.Get<float>("Knockback"),
            tagCompound.Get<int>("AmmoItemID"));
    }
}
