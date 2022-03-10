using DestinyMod.Content.Buffs.Debuffs;
using DestinyMod.Content.NPCs.Vex.VaultOfGlass;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModSystems
{
    public sealed class VaultOfGlassSystem : ModSystem
	{
		public static int Checkpoint;

		public static int RaidClears;

		public static Vector2 TilePosition;

		public static int NextOracleToKillInOrder = 1;

		public static int OracleStage;

		public static int Counter;

		private static bool OraclesStart;


		private static Point OracleOffsetPoint;

		private static bool OraclesPlacedThisRotation;

		private static int OraclesShownThisRotation = 1;

		private static bool ShowOraclesAgain = true;

		private static List<Tuple<Point, bool, int>> OraclePotentialPositions;

		public override void PreSaveAndQuit()
		{
			Checkpoint = 0;
			RaidClears = 0;
			TilePosition = Vector2.Zero;
			NextOracleToKillInOrder = 1;
			OracleStage = 0;
			Counter = 0;
			OraclesStart = false;
			OraclesPlacedThisRotation = false;
			OraclesShownThisRotation = 1;
			ShowOraclesAgain = true;
		}

        public override void PostUpdateNPCs()
        {
			if (!OraclesStart)
            {
				return;
            }

			static int OraclesCountHelper() => OracleStage == 2 ? 7 : OracleStage == 1 ? 5 : 3;

			Counter++;

			if (!OraclesPlacedThisRotation && Counter == 0)
            {
				Main.NewText("The Oracles prepare to sing their refrain");

				OraclePotentialPositions = new List<Tuple<Point, bool, int>>()
                {
					new Tuple<Point, bool, int>(new Point(OracleOffsetPoint.X + 150, OracleOffsetPoint.Y), false, 0),
					new Tuple<Point, bool, int>(new Point(OracleOffsetPoint.X + 100, OracleOffsetPoint.Y), false, 0),
					new Tuple<Point, bool, int>(new Point(OracleOffsetPoint.X + 50, OracleOffsetPoint.Y), false, 0),
					new Tuple<Point, bool, int>(new Point(OracleOffsetPoint.X - 50, OracleOffsetPoint.Y), false, 0),
					new Tuple<Point, bool, int>(new Point(OracleOffsetPoint.X - 100, OracleOffsetPoint.Y), false, 0),
					new Tuple<Point, bool, int>(new Point(OracleOffsetPoint.X - 150, OracleOffsetPoint.Y), false, 0),
					new Tuple<Point, bool, int>(new Point(OracleOffsetPoint.X - 200, OracleOffsetPoint.Y), false, 0),
				};

				int maxOracles = OraclesCountHelper();
				int oracleOrder = 1;
				for (int i = 0; i < maxOracles; i++)
                {
					int randIndex = Main.rand.Next(0, OraclePotentialPositions.Count);
					while (OraclePotentialPositions[randIndex].Item2)
                    {
						randIndex = Main.rand.Next(0, OraclePotentialPositions.Count);
						DestinyMod.Instance.Logger.Info(randIndex + " @ " + OracleStage);
					}
					int oracle = NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), OraclePotentialPositions[randIndex].Item1.X, OraclePotentialPositions[randIndex].Item1.Y, ModContent.NPCType<Oracle>(), 0, oracleOrder);
					Main.npc[oracle].hide = true;

                    OraclePotentialPositions[randIndex] = new Tuple<Point, bool, int>(OraclePotentialPositions[randIndex].Item1, true, oracleOrder);
					oracleOrder++;
                }

                OraclesPlacedThisRotation = true;
				Counter = 0;
			}
			if (OraclesPlacedThisRotation && Counter > 90 && OraclesShownThisRotation - 1 != OraclesCountHelper())
			{
				for (int i = 0; i < Main.maxNPCs; i++)
                {
					NPC oracle = Main.npc[i];
					if (oracle.type == ModContent.NPCType<Oracle>() && oracle.active && oracle.ai[0] == OraclesShownThisRotation && oracle.hide)
                    {
						oracle.hide = false;

						SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/NPC/Oracle" + (OraclePotentialPositions.IndexOf(OraclePotentialPositions.Find(p => p.Item3 == OraclesShownThisRotation)) + 1)), oracle.Center);
						OraclesShownThisRotation++;

						Counter = 0;
						break;
                    }
                }
			}
			if (OraclesPlacedThisRotation && Counter > 90 && OraclesShownThisRotation - 1 == OraclesCountHelper())
            {
				if (ShowOraclesAgain)
                {
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC oracle = Main.npc[i];
						if (oracle.type == ModContent.NPCType<Oracle>() && oracle.active && !oracle.hide)
						{
							oracle.hide = true;
						}
					}
					ShowOraclesAgain = false;
					OraclesShownThisRotation = 1;
					Counter = 0;
				}
				else
                {
					if (Counter == 100)
                    {
						for (int i = 0; i < Main.maxNPCs; i++)
						{
							NPC oracle = Main.npc[i];
							if (oracle.type == ModContent.NPCType<Oracle>() && oracle.active && !oracle.hide)
							{
								oracle.hide = true;
							}
						}
					}
					if (Counter == 180)
                    {
						for (int i = 0; i < Main.maxNPCs; i++)
						{
							NPC oracle = Main.npc[i];
							if (oracle.type == ModContent.NPCType<Oracle>() && oracle.active && oracle.hide)
							{
								oracle.hide = false;
								oracle.dontTakeDamage = false;
							}
						}
					}
					if (Counter > 780)
                    {
						FailOracles();
					}
                }
            }
		}

		public static void FailOracles()
        {
			Main.NewText("MARKED BY AN ORACLE!");

			for (int playerCount = 0; playerCount < Main.maxPlayers; playerCount++)
			{
				Player player = Main.player[playerCount];
				if (player.active && !player.HasBuff<MarkedForNegation>())
				{
					player.AddBuff(ModContent.BuffType<MarkedForNegation>(), 1);
				}
			}

			for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC oracle = Main.npc[i];
				if (oracle.type == ModContent.NPCType<Oracle>())
                {
					oracle.active = false;
                }
            }

			ResetOracles();
		}

		public static void StartOracles(Point position)
		{
			OraclesStart = true;
			Counter = -10;
			OracleOffsetPoint = position;
		}

		public static void ResetOracles(bool progress = false)
        {
			if (progress)
            {
				OracleStage++;
            }
			Counter = -90;
			OraclesShownThisRotation = 1;
			OraclesPlacedThisRotation = false;
			ShowOraclesAgain = true;
			NextOracleToKillInOrder = 1;
		}
	}
}