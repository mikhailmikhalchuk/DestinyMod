using Terraria;
using Terraria.ModLoader;
using System;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using DestinyMod.Common.Items;

namespace DestinyMod.Common.Mono.Modifications
{
	public class ImplementCanEquip : ILoadable
	{
		public void Load(Mod mod) => IL.Terraria.UI.ItemSlot.ArmorSwap += ModifyArmorSwap;

		public void Unload() { }

		private void ModifyArmorSwap(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchRet()))
			{
				DestinyMod.Instance.Logger.Error("Failed to match target in " + nameof(ImplementCanEquip));
				return;
			}

			ILLabel normalcy = il.DefineLabel();

			cursor.Emit(OpCodes.Ldarg_0);
			cursor.EmitDelegate<Func<Item, bool>>(item => item.ModItem is not DestinyModItem destinyModItem || destinyModItem.CanEquip(Main.LocalPlayer));
			cursor.Emit(OpCodes.Brtrue, normalcy);
			cursor.Emit(OpCodes.Ldarg_0);
			cursor.Emit(OpCodes.Ret);
			cursor.MarkLabel(normalcy);
		}
	}
}