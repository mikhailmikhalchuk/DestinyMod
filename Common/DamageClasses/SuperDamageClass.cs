using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace DestinyMod.Common.DamageClasses
{
    public class SuperDamageClass : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("super damage");
        }

        /*protected override float GetBenefitFrom(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
            {
                return 1f;
            }

            return 0f;
        }

        public override bool CountsAs(DamageClass damageClass)
        {
            return false;
        }*/
    }
}
