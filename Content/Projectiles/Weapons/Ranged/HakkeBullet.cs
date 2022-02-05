using Microsoft.Xna.Framework;
using DestinyMod.Common.Projectiles.ProjectileType;

namespace DestinyMod.Content.Projectiles.Weapons.Ranged
{
	public class HakkeBullet : Bullet
	{
		public override Color? GetAlpha(Color lightColor) => new Color(lightColor.R, lightColor.G * 0.5f, lightColor.B * 0.1f, lightColor.A);
	}
}