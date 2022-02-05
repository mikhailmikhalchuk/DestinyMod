using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Items.Weapons.Ranged;

namespace DestinyMod.Common.NPCs.NPCTypes
{
	public abstract class GenericTownNPC : DestinyModNPC
	{
		public bool Male;

		public sealed override void SetStaticDefaults()
		{
			AutomaticSetStaticDefaults();
			DestinySetStaticDefaults();
		}

		public virtual void AutomaticSetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 26;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 10;
			NPCID.Sets.DangerDetectRange[NPC.type] = 700;
			NPCID.Sets.AttackType[NPC.type] = 1;
			NPCID.Sets.AttackTime[NPC.type] = 30;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
			NPCID.Sets.HatOffsetY[NPC.type] = 8;
		}

		public virtual void DestinySetStaticDefaults() { }

		public override void AutomaticSetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			AnimationType = NPCID.Guide;
			Male = true;
		}

		public override bool CanGoToStatue(bool toKingStatue) => Male == toKingStatue;

		public override void OnGoToStatue(bool toKingStatue)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				ModPacket packet = Mod.GetPacket();
				packet.Write((byte)NPC.whoAmI);
				packet.Send();
			}
			/*else // Seems useless
			{
				for (int i = 0; i < 30; i++)
				{
					Vector2 position = Main.rand.NextVector2Square(-20, 21);
					if (Math.Abs(position.X) > Math.Abs(position.Y))
					{
						position.X = Math.Sign(position.X) * 20;
					}
					else
					{
						position.Y = Math.Sign(position.Y) * 20;
					}
				}
			}*/
		}

		/// <summary>
		/// See base.TownNPCAttackStrength for default summary
		/// </summary>
		/// <param name="damage">Defaults to 20</param>
		/// <param name="knockback">Defaults to 4f</param>
		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		/// <summary>
		/// See base.DrawTownAttackGun for default summary
		/// </summary>
		/// <param name="scale">Defaults to 0.5f</param>
		/// <param name="item">Defaults to AceOfSpades</param>
		/// <param name="closeness">Defaults to 20</param>
		public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
		{
			scale = 0.5f;
			item = ModContent.ItemType<AceOfSpades>();
			closeness = 20;
		}

		/// <summary>
		/// See base.TownNPCAttackCooldown for default summary
		/// </summary>
		/// <param name="cooldown">Defaults to 30</param>
		/// <param name="randExtraCooldown">Defaults to 30</param>
		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		///<summary>
		/// See base.TownNPCAttackProj for default summary
		///</summary>
		/// <param name="projType">Defaults to ProjectileID.Bullet</param>
		/// <param name="attackDelay">Defaults to 1</param>
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ProjectileID.Bullet;
			attackDelay = 1;
		}

		///<summary>
		/// See base.TownNPCAttackProjSpeed for default summary
		///</summary>
		/// <param name="multiplier">Defaults to 12f</param>
		/// <param name="randomOffset">Defaults to 2f</param>
		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}