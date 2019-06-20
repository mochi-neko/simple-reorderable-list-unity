using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList
{
	/// <summary>
	/// Supplies element background drawing method for reorderable list.
	/// </summary>
	public static class BackgroundUtility
	{

		#region Color

		/// <summary>
		/// Active color on personal skin.
		/// </summary>
		private static readonly Color activeColorPersonal = new Color(0.3f, 0.6f, 0.95f, 0.95f);
		/// <summary>
		/// Active color on professional skin.
		/// </summary>
		private static readonly Color activteColorProfessional = new Color(0.2f, 0.4f, 0.7f, 0.95f);
		/// <summary>
		/// Supplies active color on user skin.
		/// </summary>
		private static Color ActiveColor
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return activteColorProfessional;
				else
					return activeColorPersonal;
			}
		}

		/// <summary>
		/// Background color on personal skin.
		/// </summary>
		private static Color backgroundColorPersonal = new Color(0.85f, 0.85f, 0.85f, 1f);
		/// <summary>
		/// Background color on professional skin.
		/// </summary>
		private static Color backgroundColorProfessional = new Color(0.25f, 0.25f, 0.25f, 1f);
		/// <summary>
		/// Supplies background color for user skin.
		/// </summary>
		private static Color DifferentBackgroundColor
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return backgroundColorProfessional;
				else
					return backgroundColorPersonal;
			}
		}

		#endregion

		#region Margin

		/// <summary>
		/// The width of left side margin on element background.
		/// </summary>
		private static float ElementLeftMargin
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return 1f;
				else
					return 2f;
			}
		}
		/// <summary>
		/// The width of right side extrusion on element background.
		/// </summary>
		private static float ElementRightExtrusion
			=> 2f;

		/// <summary>
		/// The width of left side margin on active element.
		/// </summary>
		private static float ActiveLeftMargin
			=> 1f;
		/// <summary>
		/// The width of right side extrusion on active element.
		/// </summary>
		private static float ActiveRightExtrusion
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return 2f;
				else
					return 1f;
			}
		}

		#endregion

		#region Drawing

		/// <summary>
		/// Draws different color element background to <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		public static void DrawElementBackgroundColorDifferent(Rect rect)
			=> DrawElementBackgroundColor(
				rect,
				DifferentBackgroundColor,
				ElementLeftMargin,
				ElementRightExtrusion
				);

		/// <summary>
		/// Draws active color element background to <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		public static void DrawElementBackgroundColorActive(Rect rect)
			=> DrawElementBackgroundColor(
				rect,
				ActiveColor,
				ActiveLeftMargin,
				ActiveRightExtrusion
				);

		/// <summary>
		/// Draw color texture to <paramref name="rect"/> as element background.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		/// <param name="leftMargin"></param>
		/// <param name="rightExtrusion"></param>
		private static void DrawElementBackgroundColor(
			Rect rect, 
			Color color, 
			float leftMargin,
			float rightExtrusion)
		{
			GUI.DrawTexture(
				HorizontalAdjusted(rect, leftMargin, rightExtrusion),
				CreateColorTexture(color),
				ScaleMode.StretchToFill
			);
		}

		/// <summary>
		/// Adjusts horisontal parameters of <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="leftMargin"></param>
		/// <param name="rightExtrusion"></param>
		/// <returns></returns>
		private static Rect HorizontalAdjusted(
			Rect rect,
			float leftMargin,
			float rightExtrusion)
		{
			rect.x += leftMargin;

			rect.width -= (leftMargin + rightExtrusion);

			return rect;
		}

		/// <summary>
		/// Creates mono color texture with minimum size.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		private static Texture2D CreateColorTexture(Color color)
		{
			var texture = new Texture2D(1, 1);

			texture.SetPixel(0, 0, color);

			texture.Apply();

			return texture;
		}

		#endregion

	}
}