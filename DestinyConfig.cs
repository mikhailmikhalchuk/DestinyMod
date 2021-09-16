using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader.Config;

namespace TheDestinyMod
{
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
        [Tooltip("Adds text under the super resource bar.")]
        public bool SuperBarText { get; set; }

        [Label("Notify on Charge")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [Tooltip("Notifies you in chat when your super is charged.")]
        public bool NotifyOnSuper { get; set; }

        [Header("Gameplay")]
        [Label("Restrict Items")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [Tooltip("Restricts certain items to specific classes.")]
        public bool RestrictClassItems { get; set; }

        [Label("Guardian Games")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [ReloadRequired]
        [Tooltip("Allows you to participate in the Guardian Games\n-This will make a request to a server every time the mod is reloaded\n-Your public IP address WILL be exposed, but we do not collect or identify it\n-Enables laurel dropping from enemies")]
        public bool GuardianGamesConfig { get; set; }

        [Label("Sepiks Death Animation")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [Tooltip("Enables Sepiks Prime's unique death animation.")]
        public bool SepiksDeathAnimation { get; set; }

        [Header("UI")]
        [Label("Class Labels")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(true)]
        [Tooltip("Displays character classes on the character selection screen.")]
        public bool CharacterClassLabels { get; set; }
    }
}