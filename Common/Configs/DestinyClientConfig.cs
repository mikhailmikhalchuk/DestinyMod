using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace DestinyMod.Common.Configs
{
    [Label("Client Config")]
    [BackgroundColor(66, 109, 179, 216)]
    public class DestinyClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static DestinyClientConfig Instance => ModContent.GetInstance<DestinyClientConfig>();

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

        [Header("UI")]
        [Label("Class Labels")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(true)]
        [Tooltip("Displays character classes on the character selection screen.")]
        public bool CharacterClassLabels { get; set; }
    }
}