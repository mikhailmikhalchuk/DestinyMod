using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader.Config;

namespace TheDestinyMod
{
    public enum SuperNotificationStatus
    {
        None = 0,
        OnCharge = 1,
        OnRespawn = 2
    }

    [Label("Config")]
    [BackgroundColor(66, 109, 179, 216)]

    public class DestinyConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static DestinyConfig Instance;

        [Header("Super")]
        [Label("Super Bar Text")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [Tooltip("Adds text under the super resource bar")]
        public bool superBarText;

        [Label("Notify on Charge")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [Tooltip("Notifies you in chat when your super is charged")]
        public bool notifyOnSuper;

        [Header("Gameplay")]
        [Label("Restrict Items")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [Tooltip("Restricts certain items to specific classes")]
        public bool restrictClassItems;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message) => true;
    }
}