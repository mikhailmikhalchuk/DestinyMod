using DestinyMod.Common.Items.Modifiers;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
    public class HakkeAutoRifleCatalyst : ItemCatalyst
    {
        public int EnemiesDefeated;

        public static readonly int EnemiesDefeatedRequirement = 100;

        public override void SetDefaults()
        {
            EnemiesDefeated = 0;
            DisplayName = "Hakke Auto-Rifle Catalyst";
            Description = "Defeat enemies using Hakke Auto-Rifle to unlock this upgrade.";
        }

        public override List<ItemCatalystRequirement> HandleRequirementMouseText() => new List<ItemCatalystRequirement>
        {
            new ItemCatalystRequirement("Enemies Defeated", EnemiesDefeated / (float)EnemiesDefeatedRequirement)
        };

        public override void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life <= 0 && !IsCompleted)
            {
                EnemiesDefeated++;

                if (EnemiesDefeated > EnemiesDefeatedRequirement)
                {
                    IsCompleted = true;
                }
            }
        }

        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life <= 0 && !IsCompleted)
            {
                EnemiesDefeated++;

                if (EnemiesDefeated > EnemiesDefeatedRequirement)
                {
                    IsCompleted = true;
                }
            }
        }

        public override void SaveInstance(TagCompound tagCompound)
        {
            tagCompound.Add("EnemiesDefeated", EnemiesDefeated);
        }

        public override void LoadInstance(TagCompound tag)
        {
            EnemiesDefeated = tag.Get<int>("EnemiesDefeated");
        }
    }
}