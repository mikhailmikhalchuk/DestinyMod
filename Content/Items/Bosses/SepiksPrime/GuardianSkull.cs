using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Common.Items.ItemTypes;
using Terraria.GameContent.Creative;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
    public class GuardianSkull : Consumable
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Skull;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("An offering to the House of Devils' High Servitor\nSummons Sepiks Prime");

            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void DestinySetDefaults()
        {
            Item.maxStack = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player) => !NPC.AnyNPCs(ModContent.NPCType<NPCs.SepiksPrime.SepiksPrime>());

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position, 0);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.SepiksPrime.SepiksPrime>());
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: ModContent.NPCType<NPCs.SepiksPrime.SepiksPrime>());
                }
            }
            return true;
        }

        public override void AddRecipes() => CreateRecipe(1)
            .AddIngredient(ItemID.Bone, 75)
            .AddTile(TileID.DemonAltar)
            .Register();
    }
}