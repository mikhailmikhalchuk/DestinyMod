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

        [Label("Guardian Games")]
        [BackgroundColor(26, 70, 143, 192)]
        [DefaultValue(false)]
        [Tooltip("Allows you to participate in the Guardian Games\n-This will make a request to a server every time the mod is reloaded\n-Your public IP address WILL be exposed, but we do not collect or identify it\n-Enables laurel dropping from enemies\nYou must reload the mod after changing this value!")]
        public bool guardianGamesConfig;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message) => true;
    }
}