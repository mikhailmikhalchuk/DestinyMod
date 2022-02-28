using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
    public class GuardianSkull : Consumable
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Skull;

        public override void SetStaticDefaults() => Tooltip.SetDefault("An offering to the House of Devils' High Servitor\nSummons Sepiks Prime");

        public override void DestinySetDefaults()
        {
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player) => !NPC.AnyNPCs(ModContent.NPCType<Content.NPCs.SepiksPrime.SepiksPrime>());

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Content.NPCs.SepiksPrime.SepiksPrime>());
            return true;
        }

        public override void AddRecipes() => CreateRecipe(1)
            .AddIngredient(ItemID.Bone, 75)
            .AddTile(TileID.DemonAltar)
            .Register();
    }
}