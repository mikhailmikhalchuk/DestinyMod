using Terraria;
using Terraria.ModLoader;
using System;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using DestinyMod.Common.Items;

namespace DestinyMod.Common.Mono.Modifications
{
	public sealed class ImplementCanEquip : ILoadable
	{
		public void Load(Mod mod)
		{
			IL.Terraria.UI.ItemSlot.ArmorSwap += CanSwapEquip;
			IL.Terraria.UI.ItemSlot.PickItemMovementAction += CanClickEquip;
		}

		public void Unload() { }

		private void CanClickEquip(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			ILLabel breakLabel = cursor.DefineLabel();

			if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdarg(3), i => i.MatchLdfld<Item>("headSlot")))
			{
				DestinyMod.Instance.Logger.Error("Failed to match first target in ImplementCanEquip.CanClickEquip");
				return;
			}
			cursor.Emit(OpCodes.Ldarg_3);
			cursor.EmitDelegate<Func<Item, bool>>(item => item.ModItem is not DestinyModItem destinyModItem || destinyModItem.CanEquip(Main.LocalPlayer));
			cursor.Emit(OpCodes.Brfalse, breakLabel);

			if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdarg(1), i => i.MatchLdcI4(30)))
			{
				DestinyMod.Instance.Logger.Error("Failed to match second target in ImplementCanEquip.CanClickEquip");
				return;
			}
			cursor.MarkLabel(breakLabel);
		}

		private void CanSwapEquip(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchStloc(0)))
			{
				DestinyMod.Instance.Logger.Error("Failed to match first target in ImplementCanEquip.CanSwapEquip");
				return;
			}

			ILLabel normalcy = il.DefineLabel();
			cursor.Emit(OpCodes.Ldarg_0);
			cursor.Emit(OpCodes.Ldloc_0);
			cursor.EmitDelegate<Func<Item, Player, bool>>((item, player) => item.ModItem is not DestinyModItem destinyModItem || destinyModItem.CanEquip(player));
			cursor.Emit(OpCodes.Brtrue, normalcy);
			cursor.Emit(OpCodes.Ldarg_0);
			cursor.Emit(OpCodes.Ret);
			cursor.MarkLabel(normalcy);
		}
	}
}