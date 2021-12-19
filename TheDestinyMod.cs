using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI;
using Terraria.ModLoader;
using Terraria.World.Generation;
using ReLogic.Graphics;
using Terraria.GameContent.Generation;
using TheDestinyMod.NPCs.SepiksPrime;
using TheDestinyMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using System.IO;
using TheDestinyMod.Core.UI;
using TheDestinyMod.Core.Autoloading;

namespace TheDestinyMod
{
	public class TheDestinyMod : Mod
	{
        public static ModHotKey activateSuper;
        public static int CipherCustomCurrencyId;
        public static bool guardianGames = false;
        public static bool guardianGameError = false;
        public static int guardianWinner = 0;

        public static DynamicSpriteFont fontFuturaBold;
        public static DynamicSpriteFont fontFuturaBook;

        public static string currentSubworldID = string.Empty;

        internal UserInterface CryptarchUserInterface;
        internal SubclassUI SubclassUI;
        internal RaidSelectionUI RaidSelectionUI;
        internal ClassSelectionUI ClassSelectionUI;
        internal SuperChargeBar SuperResourceCharge;
        internal UserInterface raidInterface;

        internal UserInterface superChargeInterface;
        internal UserInterface subclassInterface;
        internal UserInterface classSelectionInterface;

        public static TheDestinyMod Instance { get; private set; }

        public static bool classSelecting;
        internal bool wasJustCreating;

        public TheDestinyMod() => Instance = this;
        
        public override void Load() => Autoloading.ImplementIAutoloadable(Code);

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
            Mod census = ModLoader.GetMod("Census");
            bossChecklist?.Call(
                "AddBoss",
                5.5f,
                ModContent.NPCType<SepiksPrime>(),
                this,
                "Sepiks Prime",
                (Func<bool>)(() => DestinyWorld.downedPrime),
                ModContent.ItemType<Items.Summons.GuardianSkull>(),
                new List<int> {
                    ModContent.ItemType<Items.Placeables.SepiksPrimeTrophy>(),
                    ModContent.ItemType<Items.Vanity.SepiksPrimeMask>(),
                    ModContent.ItemType<Items.Placeables.MusicBoxes.SepiksPrimeBox>()
                },
                new List<int> {
                    ModContent.ItemType<Items.Weapons.Summon.ServitorStaff>()
                },
                $"Use a [i:{ModContent.ItemType<Items.Summons.GuardianSkull>()}]",
                "Sepiks Prime retreats back into the House of Devils' lair..."
            );
            subworldLibrary?.Call(
                "Register",
                ModContent.GetInstance<TheDestinyMod>(),
                "Vault of Glass",
                600,
                700,
                VaultOfGlassGenPass(),
                (Action)VaultOfGlassLoad,
                (Action)VaultOfGlassUnload,
                null,
                false
            );
            census?.Call(
                "TownNPCCondition",
                ModContent.NPCType<NPCs.Town.Drifter>(),
                "Have 5 Motes of Dark in your inventory"
            );
            census?.Call(
                "TownNPCCondition",
                ModContent.NPCType<NPCs.Town.Zavala>(),
                "Defeat King Slime"
            );
            census?.Call(
                "TownNPCCondition",
                ModContent.NPCType<NPCs.Town.Cryptarch>(),
                "Have 1 Common Engram in your inventory"
            );
            census?.Call(
                "TownNPCCondition",
                ModContent.NPCType<NPCs.Town.AgentOfNine>(),
                "Traveling Merchant-like NPC that has a 1/10 chance to visit in Hardmode"
            );

            Autoloading.PostSetUpIAutoloadable();
        }

        public override void Unload()
        {
            Autoloading.UnloadIAutoloadable();
            UIHandler.Unload();
            Instance = null;
            NPCs.Town.AgentOfNine.shopItems.Clear();
            NPCs.Town.AgentOfNine.itemPrices.Clear();
            NPCs.Town.AgentOfNine.itemCurrency.Clear();
        }

        public override void UpdateUI(GameTime gameTime) => UIHandler.HandleUpdate(gameTime);

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) => UIHandler.HandleModifyInterfaceLayers(layers);

        public override void PreSaveAndQuit() {
            DestinyWorld.oraclesTimesRefrained = 0;
            DestinyWorld.oraclesKilledOrder = 1;
        }

        public override void Close() {
            void EndMusic(int soundSlot) {
                if (Main.music.IndexInRange(soundSlot)) {
                    var check = Main.music[soundSlot];
                    if (check != null && check.IsPlaying) {
                        Main.music[soundSlot].Stop(AudioStopOptions.Immediate);
                    }
                }
            }
            EndMusic(GetSoundSlot(SoundType.Music, "Sounds/Music/SepiksPrime"));
            base.Close();
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority) {
            if (Main.gameMenu || !Main.LocalPlayer.active)
                return;

            if (currentSubworldID == "TheDestinyMod_Vault of Glass") {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/VoGAmbience");
                priority = MusicPriority.BiomeHigh;
            }
        }

        public static List<GenPass> VaultOfGlassGenPass() {
            Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
            List<GenPass> list = new List<GenPass>
            {
			    new PassLegacy("Adjusting",
                delegate (GenerationProgress progress)
                {
                    progress.Message = "Adjusting world levels";
				    Main.worldSurface = 50;
				    Main.rockLayer = 150;
                    Main.spawnTileX = 273;
                    Main.spawnTileY = 273;
                },
                1f),
			    new PassLegacy("TemplarWell",
                delegate (GenerationProgress progress)
                {
                    progress.Message = "Templar's Well";
                    (int x, int y, Tile[,] tileData) tileData = RaidLoader.ReadRaid("Structures/TemplarsWell");
                    float passes = 0;
                    for (int i = 0; i < tileData.tileData.GetLength(0); i++)
                    {
                        for (int j = 0; j < tileData.tileData.GetLength(1); j++)
                        {
                            passes++;
                            Main.tile[i + 100, j + 200] = tileData.tileData[i, j];
                            progress.Set(passes / tileData.tileData.Length);
                        }
                    }
                },
                1f)
		    };
            subworldLibrary.Call("DrawUnderworldBackground", false);
            return list;
        }

        public static void VaultOfGlassLoad() {
            Main.LocalPlayer.noBuilding = true;
            Main.dayTime = true;
            Main.time = 10000;
            Main.mapEnabled = false;
        }

        public static void VaultOfGlassUnload() {
            currentSubworldID = string.Empty;
            Main.mapEnabled = true;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            DestinyModMessageType type = (DestinyModMessageType)reader.ReadByte();
            switch (type) {
                case DestinyModMessageType.SepiksPrime:
                    if (Main.npc[reader.ReadInt32()].modNPC is SepiksPrime prime && prime.npc.active) {
						prime.HandlePacket(reader);
					}
					break;
                default:
                    Logger.Error($"TheDestinyMod Packet Handler: Encountered unknown packet of type {type}");
                    break;
            }
        }
    }

    internal enum DestinyModMessageType : byte
    {
        SepiksPrime
    }

    /// <summary>Represents a Destiny class (not a language <see langword="class"/>).</summary>
    public enum DestinyClassType : byte
    {
        None,
        Titan,
        Hunter,
        Warlock
    }

    public enum DestinyDamageType : byte
    {
        None,
        Arc,
        Void,
        Solar,
        Stasis
    }
}