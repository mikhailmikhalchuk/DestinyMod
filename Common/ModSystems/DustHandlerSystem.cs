using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModSystems
{
    public sealed class DustHandlerSystem : ModSystem
    {
        public static List<(Dust, int, int)> DustList = new List<(Dust, int, int)>();

        public override void PostUpdateWorld()
        {
            for (int i = 0; i < DustList.Count; i++)
            {
                (Dust, int, int) item = DustList[i];
                if (++item.Item2 >= item.Item3)
                {
                    item.Item1.active = false;
                }
                DustList[i] = item;
            }
            DustList.RemoveAll(tup => !tup.Item1.active);
        }
    }

    public static class DustHelper
    {
        /// <summary>
        /// Causes dust to disappear after a set time.
        /// </summary>
        /// <param name="dust">The <see cref="Dust"/> in which to apply the duration to.</param>
        /// <param name="timeLeft">The amount of time before the <see cref="Dust"/> should disappear.</param>
        public static void SetDustTimeLeft(this Dust dust, int timeLeft)
        {
            if (DustHandlerSystem.DustList.Any(tup => tup.Item1.dustIndex == dust.dustIndex))
            {
                DustHandlerSystem.DustList[DustHandlerSystem.DustList.FindIndex(tup => tup.Item1.dustIndex == dust.dustIndex)] = (dust, 0, timeLeft);
            }
            else
            {
                DustHandlerSystem.DustList.Add((dust, 0, timeLeft));
            }
        }
    }
}
