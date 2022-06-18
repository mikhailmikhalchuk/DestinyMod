namespace DestinyMod.Common.Data
{
    public struct AmmoData
    {
        public int ProjectileType;

        public float Speed;

        public int Damage;

        public float Knockback;

        public int AmmoItemID;

        public AmmoData(int projectileType, float speed, int damage, float knockBack, int ammoItemID)
        {
            ProjectileType = projectileType;
            Speed = speed;
            Damage = damage;
            Knockback = knockBack;
            AmmoItemID = ammoItemID;
        }
    }
}
