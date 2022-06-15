using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DestinyMod.Content.UI.MouseText
{
	/// <summary>Container class for objective mouseover info, to enable multi-objective info display.</summary>
	public class MouseText_Objectives : MouseTextElement
	{
		public Texture2D RequiredItemTexture { get; }

		public string RequiredItemName { get; }

		public Vector2 RequiredItemNameDimensions { get; }

		public int RequiredItemType { get; }

		public int RequiredItemStack { get; }

		public override Color BackgroundColor_Default => new Color(20, 20, 20) * CommonOpacity;

		public IList<MouseText_Objective> Objectives { get; private set; }

		public MouseText_Objectives(params MouseText_Objective[] objectives)
		{
			Objectives = objectives;
			for (int i = 0; i < Objectives.Count; i++)
            {
				Append(Objectives[i]);
			}
			WidthPercentage = 1f;
		}

        public override void Recalculate()
        {
			for (int i = 0; i < Objectives.Count; i++)
			{
				MouseText_Objective objective = Objectives[i];
				objective.Left.Pixels = 10;
				objective.Width.Pixels = Width.Pixels - 20;
				objective.Top.Pixels = (objective.Height.Pixels + 4)  * i + MouseTextState.CommonBorder;
			}

			MouseText_Objective lastObjective = Objectives[Objectives.Count - 1];
			Height.Pixels = lastObjective.Top.Pixels + lastObjective.Height.Pixels + MouseTextState.CommonBorder * 2;

			base.Recalculate();
		}
	}
}
