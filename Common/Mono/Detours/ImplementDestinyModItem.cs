using Terraria;
using Terraria.ModLoader;
using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;
using System;
using Terraria.ID;
using DestinyMod.Common.Items;

namespace DestinyMod.Common.Mono.Detours
{
	public sealed class ImplementDestinyModItem : ILoadable
	{
		public Action<Item, bool> ItemLoaderSetDefaults;

		public void Load(Mod mod)
		{
			ItemLoaderSetDefaults = (Action<Item, bool>)typeof(ItemLoader).GetMethod("SetDefaults", BindingFlags.NonPublic | BindingFlags.Static).CreateDelegate(typeof(Action<Item, bool>));
			MethodBase setupContent = typeof(ModItem).GetMethod("SetupContent", BindingFlags.Public | BindingFlags.Instance);
			HookEndpointManager.Add(setupContent, ReplacementSetupContent);
		}

		public void Unload()
		{
			MethodBase setupContent = typeof(ModItem).GetMethod("SetupContent", BindingFlags.Public | BindingFlags.Instance);
			HookEndpointManager.Remove(setupContent, ReplacementSetupContent);
		}

		public void ReplacementSetupContent(Action<ModItem> orig, ModItem modItem)
		{
			if (modItem is not DestinyModItem destinyModItem)
			{
				orig.Invoke(modItem);
				return;
			}

			destinyModItem.AutoStaticDefaults();
			ItemLoaderSetDefaults.Invoke(destinyModItem.Item, false);
			destinyModItem.SetStaticDefaults();
			ItemID.Search.Add(destinyModItem.FullName, destinyModItem.Type);
		}
	}
}