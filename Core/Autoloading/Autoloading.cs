using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;

namespace TheDestinyMod.Core.Autoloading
{
	public class Autoloading
	{
		public static IList<IAutoloadable> LoadedClasses;

		public static void ImplementIAutoloadable(Assembly assembly)
		{
			if (assembly is null)
			{
				throw new Exception("Given assembly when setting up IAutoloadable appears to be null");
			}

			LoadedClasses = new List<IAutoloadable>();

			foreach (Type type in assembly.GetTypes())
			{
				if (!type.IsAbstract && type.GetConstructor(new Type[0]) != null)
				{
					if (typeof(IAutoloadable).IsAssignableFrom(type))
					{
						IAutoloadable autoload = Activator.CreateInstance(type) as IAutoloadable;
						autoload.IAutoloadable_Load(autoload);
						LoadedClasses.Add(autoload);
						ContentInstance.Register(autoload);
					}
				}
			}
		}

		public static void PostSetUpIAutoloadable()
		{
			if (LoadedClasses is null)
			{
				return;
			}

			foreach (IAutoloadable autoload in LoadedClasses)
			{
				autoload.IAutoloadable_PostSetUpContent();
			}
		}

		public static void UnloadIAutoloadable()
		{
			if (LoadedClasses != null)
			{
				foreach (IAutoloadable autoload in LoadedClasses)
				{
					autoload.IAutoloadable_Unload();
				}

				LoadedClasses.Clear();
			}
		}
	}
}