using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Buffs;

namespace DestinyMod.Content.Items.Equipables.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class Dunemarchers : ClassArmor
	{
		public int RunTime;

		public override DestinyClassType ArmorClassType => DestinyClassType.Titan;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases maximum running speed"
				+ "\nRunning for more than 5 seconds straight will grant the Linear Actuators buff" 
				+ "\n\"Whether on solid rock or shifting sand dune, the inexorable Sand Eaters never slow their pace.\"");
		}

		public override void DestinySetDefaults()
		{
			Item.value = Item.sellPrice(silver: 60);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 8;
			RunTime = 0;
		}

		public override void UpdateEquip(Player player)
		{
			player.accRunSpeed = 5f;
			if ((player.velocity.X > 0f - player.accRunSpeed && player.velocity.X < 0f - ((player.accRunSpeed + player.maxRunSpeed) / 2f) || player.velocity.X > ((player.accRunSpeed + player.maxRunSpeed) / 2f) && player.velocity.X < player.accRunSpeed) && player.velocity.Y == 0f && !player.mount.Active && player.dashDelay >= 0)
			{
				if (++RunTime >= 300)
				{
					player.AddBuff(ModContent.BuffType<LinearActuators>(), 60);
				}
			}
			else
			{
				RunTime = 0;
			}
			base.UpdateEquip(player);
		}

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Materials.RelicIron>(), 20)
			.AddIngredient(ModContent.ItemType<Materials.PlasteelPlating>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}