using Terraria;
using Terraria.ID;
using DestinyMod.Common.Items.ItemTypes;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace DestinyMod.Content.Items.Equipables.Dyes
{
    public class GuardianGamesDye : Dye
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            if (!Main.dedServ)
            {
                GameShaders.Armor.BindShader(Item.type, new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Assets/Effects/Dyes/GuardianGames", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value), "GuardianGamesDyePass"))
                    .UseColor(2f, 2f, 0f)
                    .UseSecondaryColor(2f, 0.25f, 0.35f);
            }
        }

        public override void DestinySetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
        }
    }
}