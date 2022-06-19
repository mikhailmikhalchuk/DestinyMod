using Terraria;
using Terraria.ID;
using DestinyMod.Common.Items.ItemTypes;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace DestinyMod.Content.Items.Equipables.Dyes
{
    // This is done using a dye because I'm lazy
    public class MysteriousDye : Dye
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            if (!Main.dedServ)
            {
                GameShaders.Armor.BindShader(Item.type, new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Assets/Effects/Dyes/White", AssetRequestMode.ImmediateLoad).Value), "White"))
                    .UseSaturation(0.5f); // Adjust saturation to adjust how white the funny mag display is
            }
        }

        public override void DestinySetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
        }
    }
}