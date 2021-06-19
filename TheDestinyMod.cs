using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using TheDestinyMod.NPCs.SepiksPrime;
using TheDestinyMod.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using log4net;

namespace TheDestinyMod
{
	public class TheDestinyMod : Mod
	{
        public TheDestinyMod() {
            Instance = this;
        }

        public static ModHotKey activateSuper;
        public static int CipherCustomCurrencyId;
        public static bool guardianGames = false;
        public static bool guardianGameError = false;
        public static int guardianWinner = 0;
        public static new ILog Logger = LogManager.GetLogger("TheDestinyMod");

        public static string currentSubworldID = string.Empty;

        internal UserInterface CryptarchUserInterface;
        internal SubclassUI SubclassUI;
        internal SuperChargeBar SuperResourceCharge;

        private UserInterface superChargeInterface;
        private UserInterface subclassInterface;

        public static Mod Instance
        {
            get;
            set;
        }

        public override void Load() {
            activateSuper = RegisterHotKey("Activate Super", "U");
            CipherCustomCurrencyId = CustomCurrencyManager.RegisterCurrency(new ExoticCipher(ModContent.ItemType<Items.ExoticCipher>(), 30L));
            On.Terraria.Player.DropTombstone += Player_DropTombstone;
            if (!Main.dedServ) {

                try {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://DestinyModServer.mikhailmcraft.repl.co");
                    request.Method = "GET";
                    request.Headers["VERIFY-MOD"] = "a7rg53F435h4Ff2fhjWa33gH6j54ag2G";
                    request.Headers["DUPLICATE-CHECK"] = Steamworks.SteamUser.GetSteamID().ToString();
                    request.Timeout = 1500;

                    using (Stream s = request.GetResponse().GetResponseStream()) {
                        using (StreamReader sr = new StreamReader(s)) {
                            var jsonResponse = sr.ReadToEnd();
                            if (jsonResponse.Remove(2) == "ON") {
                                guardianGames = true;
                            }
                            if (jsonResponse.Contains("T")) {
                                guardianWinner = 1;
                            }
                            else if (jsonResponse.Contains("H")) {
                                guardianWinner = 2;
                            }
                            else if (jsonResponse.Contains("W")) {
                                guardianWinner = 3;
                            }
                        }
                    }
                }
                catch (Exception e) {
                    Logger.Error($"Failed to receive a response from the server: {e.Message}");
                    guardianGameError = true;
                }

                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SepiksPrime"), ModContent.ItemType<Items.Placeables.MusicBoxes.SepiksPrimeBox>(), ModContent.TileType<Tiles.MusicBoxes.SepiksPrimeBox>());

                GameShaders.Armor.BindShader(ModContent.ItemType<Items.Dyes.GambitDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/Dyes/Gambit")), "GambitDyePass")).UseColor(0, 1f, 0);
                GameShaders.Armor.BindShader(ModContent.ItemType<Items.Dyes.GuardianGamesDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/Dyes/GuardianGames")), "GuardianGamesDyePass")).UseColor(2f, 2f, 0f).UseSecondaryColor(2f, 0.25f, 0.35f);
                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/Shaders/ShockwaveEffect"));
                Filters.Scene["TheDestinyMod:Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["TheDestinyMod:Shockwave"].Load();
                SubclassUI = new SubclassUI();
                SubclassUI.Activate();
                subclassInterface = new UserInterface();
                subclassInterface.SetState(SubclassUI);
                CryptarchUserInterface = new UserInterface();
                SuperResourceCharge = new SuperChargeBar();
				superChargeInterface = new UserInterface();
				superChargeInterface.SetState(SuperResourceCharge);
            }
            #region Translations
            int taxCollector = NPC.FindFirstNPC(NPCID.TaxCollector);
            ModTranslation text = CreateTranslation("AgentOfNine1");
            text.SetDefault("I may be here when you return.");
            text.AddTranslation(GameCulture.Polish, "Będę tutaj jak powrócisz.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine2");
            text.SetDefault("These are from the Nine.");
            text.AddTranslation(GameCulture.Polish, "Oni są z tej dziewiątki.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine3");
            text.SetDefault("My will is not my own. Is yours?");
            text.AddTranslation(GameCulture.Polish, "Moja wola nie jest moją własną. A twoja?");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine4");
            text.SetDefault("The Traveler's song echoes on.");
            text.AddTranslation(GameCulture.Polish, "Pieśń Wędrowcy rozbrzmiewa echem.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine5");
            text.SetDefault("I bring a message from the Nine.");
            text.AddTranslation(GameCulture.Polish, "Przybywam z wiadomością od dziewiątki.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine6");
            text.SetDefault("Do not be alarmed. I know no reason to cause you harm.");
            text.AddTranslation(GameCulture.Polish, "Nie przejmuj się. Nie mam powodów żeby cię skrzywdzić.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine7");
            text.SetDefault("To do what you say, is to speak in a language of pure meaning.");
            text.AddTranslation(GameCulture.Polish, "Robienie tego co mówisz to mówienie w języku o czystym znaczeniu.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine8");
            text.SetDefault("Do not go looking for the Nine. They will come to you.");
            text.AddTranslation(GameCulture.Polish, "Nie szukaj dziewiątki. To ona znajdzie ciebie.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine9");
            text.SetDefault("You must stop eating salted popcorn.");
            text.AddTranslation(GameCulture.Polish, "Musisz przestać jeść ten słony popcorn.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine10");
            text.SetDefault("I am here for a reason, I just...cannot remember it.");
            text.AddTranslation(GameCulture.Polish, "Jestem tutaj z powodu tylko nie pamiętam jakiego.");
            AddTranslation(text);
            text = CreateTranslation("AgentOfNine11");
            text.SetDefault("So much Light here, I suppose I feel...pain.");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch1");
            text.SetDefault("A party, you say? I'm much too busy decrypting these Vex artifacts, thank you.");
            text.AddTranslation(GameCulture.Polish, "Impreza powiadasz? Jestem bardziej zajęty deszyfrowaniem tych artefaktów od Vexów, dziękuje.");
            text.AddTranslation(GameCulture.Spanish, "¿Una escuadra, dices? Estoy muy ocupado desencriptando estos artefactos Vex, gracias.");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch2");
            text.SetDefault("Ah! This lack of light makes it hard to do anything around here.");
            text.AddTranslation(GameCulture.Polish, "Przez brak światła sprawia, że trudno tu co kolwiek zrobić.");
            text.AddTranslation(GameCulture.Spanish, "¡Ah! la falta de luz hace mas dificil hacer cualquier cosa por aquí.");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch3");
            text.SetDefault("Vex encryption. Unbreakable? Ha, so they say.");
            text.AddTranslation(GameCulture.Polish, "Szyfr Vexów nie do złamania? Ha tylko tak mówią.");
            text.AddTranslation(GameCulture.Spanish, "Encriptación vex. ¿Irrompible? Ja, eso dicen.");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch4");
            text.SetDefault("What have you got for me, Guardian?");
            text.AddTranslation(GameCulture.Polish, "Co masz dla mnie, Strażniku?");
            text.AddTranslation(GameCulture.Spanish, "¿Que me trajiste, Guardián?");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch5");
            text.SetDefault("Rasputin's fingerprints are all over this data. He doesn't even care if we know.");
            text.AddTranslation(GameCulture.Polish, "Odciski Rasputina są wszędzie. Go nie obchodzi że my wiemy.");
            text.AddTranslation(GameCulture.Spanish, "Las huellas de Rasputin estan sobre todos estos datos. Ni siquiera le importa si nosotros lo sabemos.");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch6");
            text.SetDefault("What challenges have you brought today, Guardian?");
            text.AddTranslation(GameCulture.Polish, "Jakie wyzwanie przyniosłeś dzisiaj, Strażniku?");
            text.AddTranslation(GameCulture.Spanish, "¿Que desafio me trajiste hoy, Guardián?");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch7");
            text.SetDefault("These are forgeries. Someone is wasting our time!");
            text.AddTranslation(GameCulture.Polish, "To są podróbki. Ktoś marnuje nasz czas!");
            text.AddTranslation(GameCulture.Spanish, "Estas son falsificaciones. ¡Alguien nos está haciendo perder el tiempo!");
            AddTranslation(text);
            text = CreateTranslation("Cryptarch8");
            text.SetDefault("Hmm, this one is labeled \"Bigm55\"...yes, Guardian?");
            AddTranslation(text);
            text = CreateTranslation("RelicShard");
            text.SetDefault("Relic Shards have begun to grow!");
            text.AddTranslation(GameCulture.Polish, "Odłamki reliktów zaczęły rosnąć!");
            text.AddTranslation(GameCulture.Spanish, "¡Fragmentos de Reliquia empezaron a aparecer!");
            AddTranslation(text);
            text = CreateTranslation("Zavala1");
            text.SetDefault("Guardian! This is your chance to take down this profound threat, do not get distracted!");
            text.AddTranslation(GameCulture.Polish, "Strażniku! to jest twoja szansa na pokonanie tego gruntownego zagrożenia, nie rozpraszaj się!");
            text.AddTranslation(GameCulture.Spanish, "¡Guardián! ¡Esta es tu oportunidad para acabar con esta gran amenaza, no te distraigas!");
            AddTranslation(text);
            text = CreateTranslation("Zavala2");
            text.SetDefault("Get back out there, Guardian, and eliminate this threat!");
            text.AddTranslation(GameCulture.Polish, "Wracaj tam strażniku i pozbądź się tego zagrożenia!");
            text.AddTranslation(GameCulture.Spanish, "¡Sal afuera, Guardián, y elimina esta amenaza!");
            AddTranslation(text);
            text = CreateTranslation("Zavala3");
            text.SetDefault("Guardian, you can do this. I believe in you, as does the rest of the Vanguard.");
            text.AddTranslation(GameCulture.Polish, "Uda ci się strażniku, wierzę w ciebie tak samo jak cała reszta Straży przedniej.");
            text.AddTranslation(GameCulture.Spanish, "Guardián, puedes hacer esto. Creo en ti, como el resto de la Vanguardia.");
            AddTranslation(text);
            text = CreateTranslation("Zavala4");
            text.SetDefault("Guardian, you've slain some of the worst enemies the City and the Vanguard have ever seen, and for that, I thank you.");
            text.AddTranslation(GameCulture.Polish, "Strażniku, zabiłeś jednych z najgorszych wrogów jakich Miasto i Straż Przednia kiedykolwiek widziały i za to, Dziękuje tobie.");
            text.AddTranslation(GameCulture.Spanish, "Guardián, eliminaste a unos de los peores enemigos que la Ciudad y la Vanguardia hayan visto, y por eso, te agradezco.");
            AddTranslation(text);
            text = CreateTranslation("Zavala5");
            text.SetDefault("Guardian, it's been brought to my attention that you may be partaking in...unsolicited activities. As long as it's for the good of the City.");
            text.AddTranslation(GameCulture.Polish, "Strażniku, zwrócono mi uwagę że możesz brać udział w niedozwolonych zajęć. O ile to dla dobra miasta.");
            text.AddTranslation(GameCulture.Spanish, "Guardián, me llamó la atencion que tal vez estes completando... actividades no solicitadas. Mientras que sea por el bien de la Ciudad.");
            AddTranslation(text);
            text = CreateTranslation("Zavala6");
            text.SetDefault("I didn't authorize any party...but I guess we can take advantage of the moment while it lasts.");
            text.AddTranslation(GameCulture.Polish, "Nie autoryzowałem żadnej imprezy… ale myślę, że możemy wykorzystać ten moment, dopóki trwa.");
            text.AddTranslation(GameCulture.Spanish, "No autorizé ninguna escuadra... pero supongo que podriamos tomar provecho del momento mientras dura.");
            AddTranslation(text);
            text = CreateTranslation("Zavala7");
            text.SetDefault("The Darkness is extremely strong here, Guardian.");
            text.AddTranslation(GameCulture.Polish, "Ciemność jest tutaj wysoce silna Strażniku.");
            text.AddTranslation(GameCulture.Spanish, "La Oscuridad es demasiado fuerte aqui, Guardián.");
            AddTranslation(text);
            text = CreateTranslation("Zavala8");
            text.SetDefault("Do you feel that, Guardian? The Traveler's raw energies, scattered across this land.");
            text.AddTranslation(GameCulture.Polish, "Czujesz to Strażniku? Surowa energia Podróżnika, została rozproszona po całej tej krainie.");
            text.AddTranslation(GameCulture.Spanish, "¿Sientes eso, Guardián? La energia pura del Viajero, esparcida por esa tierra.");
            AddTranslation(text);
            text = CreateTranslation("Zavala9");
            text.SetDefault("Guardian.");
            text.AddTranslation(GameCulture.Polish, "Strażnik.");
            text.AddTranslation(GameCulture.Spanish, "Guardián.");
            AddTranslation(text);
            text = CreateTranslation("Zavala10");
            text.SetDefault("The Traveler graces us, Guardian.");
            text.AddTranslation(GameCulture.Polish, "Podróżnik łaskawi nas, Strażniku.");
            text.AddTranslation(GameCulture.Spanish, "El Viajero nos agradece, Guardián.");
            AddTranslation(text);
            text = CreateTranslation("Zavala11");
            text.SetDefault("Let us begin.");
            text.AddTranslation(GameCulture.Polish, "Zacznijmy zatem.");
            AddTranslation(text);
            text = CreateTranslation("Zavala12");
            text.SetDefault("Report, Guardian.");
            text.AddTranslation(GameCulture.Polish, "Raportuj, Strażniku.");
            AddTranslation(text);
            text = CreateTranslation("Zavala13");
            text.SetDefault("The Darkness approaches, Guardian. We must be ready.");
            text.AddTranslation(GameCulture.Polish, "Ciemność nadchodzi strażniku. Musimy być gotowi.");
            AddTranslation(text);
            text = CreateTranslation("ZavalaGG");
            text.SetDefault("The Guardian Games are on, Guardian! Show everyone which class is the greatest.");
            AddTranslation(text);
            text = CreateTranslation("Bounty");
            text.SetDefault("Bounty");
            text.AddTranslation(GameCulture.Polish, "Nagroda");
            AddTranslation(text);
            text = CreateTranslation("GiveMotes");
            text.SetDefault("Give Motes");
            text.AddTranslation(GameCulture.Polish, "Daj okruchy");
            text.AddTranslation(GameCulture.Spanish, "Dar Motas");
            AddTranslation(text);
            text = CreateTranslation("CheckMotes");
            text.SetDefault("Check Motes");
            text.AddTranslation(GameCulture.Polish, "Sprawdź okruchy");
            text.AddTranslation(GameCulture.Spanish, "Inspeccionar Motas");
            AddTranslation(text);
            text = CreateTranslation("SuperCharge");
            text.SetDefault("Super charged!");
            text.AddTranslation(GameCulture.Spanish, "¡Súper cargada!");
            AddTranslation(text);
            text = CreateTranslation("SuperInventory");
            text.SetDefault("You must have an inventory slot free to activate your super.");
            text.AddTranslation(GameCulture.Spanish, "Debes tener un espacio del inventario vacio para activar tu súper.");
            AddTranslation(text);
            #endregion
        }

        public override void Unload() {
            activateSuper = null;
            Instance = null;
            NPCs.Town.AgentOfNine.shopItems.Clear();
			NPCs.Town.AgentOfNine.itemPrices.Clear();
        }

        private void Player_DropTombstone(On.Terraria.Player.orig_DropTombstone orig, Player self, int coinsOwned, NetworkText deathText, int hitDirection) {
            if (currentSubworldID == string.Empty) {
                orig.Invoke(self, coinsOwned, deathText, hitDirection);
            }
        }

        public override void UpdateUI(GameTime gameTime) {
            subclassInterface?.Update(gameTime);
            superChargeInterface?.Update(gameTime);
            CryptarchUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1) {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "TheDestinyMod: Subclass UI",
                    delegate {
                        if (Main.playerInventory) {
                            subclassInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
            if (inventoryIndex != -1) {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "TheDestinyMod: Cryptarch UI",
                    delegate {
                        CryptarchUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1) {
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"TheDestinyMod: Super Charge UI",
					delegate {
						superChargeInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
        }

        public override void PreSaveAndQuit() {
            DestinyWorld.oraclesTimesRefrained = 0;
            DestinyWorld.oraclesKilledOrder = 1;
            if (CryptarchUI._vanillaItemSlot != null) {
                Item clone = CryptarchUI._vanillaItemSlot.Item.Clone();
                var slot = 0;
                foreach (Item item in Main.LocalPlayer.inventory) {
                    if (item.IsAir) {
                        Main.LocalPlayer.inventory[slot] = clone;
                        Main.PlaySound(SoundID.Grab);
                        break;
                    }
                    else if (item.type == clone.type && ++item.stack != item.maxStack) {
                        clone.stack = item.stack;
                        Main.LocalPlayer.inventory[slot] = clone;
                        Main.PlaySound(SoundID.Grab);
                        break;
                    }
                    slot++;
                }
            }
            CryptarchUI._vanillaItemSlot?.Item.TurnToAir();
        }

        public override void Close() {
            int soundSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/SepiksPrime");
            if (Main.music.IndexInRange(soundSlot)) {
                var check = Main.music[soundSlot];
                if (check != null && check.IsPlaying) {
                    Main.music[soundSlot].Stop(AudioStopOptions.Immediate);
                }
            }
            base.Close();
        }

        public override void PostSetupContent() {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
            Mod census = ModLoader.GetMod("Census");
            if (bossChecklist != null) {
                bossChecklist.Call(
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
            }
            if (subworldLibrary != null) {
                subworldLibrary.Call(
                    "Register",
                    ModContent.GetInstance<TheDestinyMod>(),
                    "Vault of Glass",
                    600,
                    400,
                    VaultOfGlassGenPass(),
                    (Action)VaultOfGlassLoad,
                    null,
                    null,
                    true
                );
            }
            if (census != null) {
                census.Call(
                    "TownNPCCondition",
                    ModContent.NPCType<NPCs.Town.Drifter>(),
                    "Have 5 Motes of Dark in your inventory"
                );
                census.Call(
                    "TownNPCCondition",
                    ModContent.NPCType<NPCs.Town.Zavala>(),
                    "Defeat King Slime"
                );
                census.Call(
                    "TownNPCCondition",
                    ModContent.NPCType<NPCs.Town.Cryptarch>(),
                    "Have 1 Common Engram in your inventory"
                );
                census.Call(
                    "TownNPCCondition",
                    ModContent.NPCType<NPCs.Town.AgentOfNine>(),
                    "Traveling Merchant-like NPC that has a 1/10 chance to visit in Hardmode"
                );
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority) {
            Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
            if (subworldLibrary != null) {
                if (currentSubworldID == "TheDestinyMod_Vault Of Glass") {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/VoGAmbience");
                    priority = MusicPriority.BossHigh;
                }
            }
        }

        public static List<GenPass> VaultOfGlassGenPass() {
                List<GenPass> list = new List<GenPass>
                {
			        new PassLegacy("Adjusting",
                    delegate (GenerationProgress progress)
                    {
                        progress.Message = "Adjusting world levels";
				        Main.worldSurface = Main.maxTilesY - 42;
				        Main.rockLayer = Main.maxTilesY;
                        Main.spawnTileX = 100;
			        },
                    1f),
			        new PassLegacy("AddingIntro",
                    delegate (GenerationProgress progress)
                    {
                        progress.Message = "The start of time...";
                        if (ModLoader.GetMod("StructureHelper") != null) {
                            DestinyHelper.StructureHelperGenerateStructure(new Vector2(Main.spawnTileX, Main.spawnTileY), "VaultOfGlass");
                        }
                    },
                    1f)
		        };
                return list;
        }

        public static void VaultOfGlassLoad() {
            Main.LocalPlayer.noBuilding = true;
            Main.dayTime = true;
            Main.time = 10000;
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
}