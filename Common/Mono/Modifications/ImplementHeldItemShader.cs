using Terraria;
using Terraria.ModLoader;
using System;
using MonoMod.Cil;
using Terraria.DataStructures;
using System.Reflection;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using DestinyMod.Common.GlobalItems;
using Terraria.Graphics.Shaders;

namespace DestinyMod.Common.Mono.Modifications
{
	public sealed class ImplementHeldItemShader : ILoadable
	{
		public void Load(Mod mod) => IL.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_27_HeldItem += PlayerDrawLayers_DrawPlayer_27_HeldItem;

		public void Unload() { }

		// God tier IL that's totally not unstable
		private void PlayerDrawLayers_DrawPlayer_27_HeldItem(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			FieldInfo drawDataCache = typeof(PlayerDrawSet).GetField("DrawDataCache", BindingFlags.Public | BindingFlags.Instance);
			int numberOfPatches = 0;
			while (cursor.TryGotoNext(MoveType.After, i => i.MatchLdarg(0),
				i => i.MatchLdfld(drawDataCache),
				i => i.MatchLdloc(12)))
			{
				cursor.Emit(OpCodes.Ldloc_0);
				cursor.EmitDelegate<Func<DrawData, Item, DrawData>>((drawData, heldItem) =>
				{
					ItemDataItem heldItemData = heldItem.GetGlobalItem<ItemDataItem>();
					if (heldItemData.Shader == null || heldItemData.Shader.dye <= 0)
					{
						return drawData;
					}
					drawData.shader = GameShaders.Armor.GetShaderIdFromItemId(heldItemData.Shader.type);
					return drawData;
				});
				numberOfPatches++;
			}

			DestinyMod.Instance.Logger.Error(nameof(ImplementHeldItemShader) + " number of IL patches applied: " + numberOfPatches);
			/*void applyShaderPatch()
            {
				cursor.Emit(OpCodes.Ldloc_0);
				cursor.Emit(OpCodes.Ldloc_S, 12);
				cursor.EmitDelegate<Action<Item, DrawData>>((heldItem, drawData) =>
				{
					//Item heldItem = drawInfo.drawPlayer.HeldItem;
					ItemDataItem heldItemData = heldItem.GetGlobalItem<ItemDataItem>();
					if (heldItemData.Shader == null || heldItemData.Shader.dye <= 0)
					{
						return;
					}
					drawData.shader = GameShaders.Armor.GetShaderIdFromItemId(heldItemData.Shader.type);
				});
			}

			FieldInfo drawDataCache = typeof(PlayerDrawSet).GetField("DrawDataCache", BindingFlags.Public | BindingFlags.Instance);
			MethodInfo add = typeof(List<DrawData>).GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
			if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdarg(0), 
				i => i.MatchLdfld(drawDataCache), 
				i => i.MatchLdloc(12), 
				i => i.MatchCallvirt(add),
				i => i.MatchLdloc(1),
				i => i.MatchLdcI4(3870)))
            {
				DestinyMod.Instance.Logger.Error("First IL failed to find target in " + nameof(ImplementHeldItemShader));
				return;
            }

			for (int ilIndexer = 0; ilIndexer < 3; ilIndexer++)
			{
				if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdarg(0),
					i => i.MatchLdfld(drawDataCache),
					i => i.MatchLdloc(12)))
				{
					DestinyMod.Instance.Logger.Error("1.5 IL ( iter: " + ilIndexer + " ) failed to find target in " + nameof(ImplementHeldItemShader));
					return;
				}
				cursor.Emit(OpCodes.Ldloc_0);
				cursor.EmitDelegate<Func<DrawData, Item, DrawData>>((drawData, heldItem) =>
				{
					ItemDataItem heldItemData = heldItem.GetGlobalItem<ItemDataItem>();
					if (heldItemData.Shader == null || heldItemData.Shader.dye <= 0)
					{
						return drawData;
					}
					drawData.shader = GameShaders.Armor.GetShaderIdFromItemId(heldItemData.Shader.type);
					return drawData;
				});

				if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdarg(0),
					i => i.MatchLdfld(drawDataCache),
					i => i.MatchLdloc(12),
					i => i.MatchCallvirt(add),
					i => i.MatchLdloc(0),
					i => i.MatchLdfld<Item>("color")))
				{
					DestinyMod.Instance.Logger.Error("Second IL ( iter: " + ilIndexer + " ) failed to find target in " + nameof(ImplementHeldItemShader));
					return;
				}

				applyShaderPatch();

				if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdarg(0),
					i => i.MatchLdfld(drawDataCache),
					i => i.MatchLdloc(12),
					i => i.MatchCallvirt(add)))
				{
					DestinyMod.Instance.Logger.Error("Third IL ( iter: " + ilIndexer + " ) failed to find target in " + nameof(ImplementHeldItemShader));
					return;
				}

				applyShaderPatch();

				if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdarg(0),
					i => i.MatchLdfld(drawDataCache),
					i => i.MatchLdloc(12),
					i => i.MatchCallvirt(add)))
				{
					DestinyMod.Instance.Logger.Error("Fourth IL ( iter: " + ilIndexer + " ) failed to find target in " + nameof(ImplementHeldItemShader));
					return;
				}

				applyShaderPatch();
			}*/
		}
	}
}