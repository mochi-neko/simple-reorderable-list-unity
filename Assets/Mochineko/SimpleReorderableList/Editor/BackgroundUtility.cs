using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList
{
	/// <summary>
	/// Supplies element background drawing method on reorderable list.
	/// </summary>
	public static class BackgroundUtility
	{

		/// <summary>
		/// The width of left side margin on element background.
		/// </summary>
		private static float LeftMargin
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
		private const float rightExtrusion = 2f;

		/// <summary>
		/// Draw color texture to <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public static void DrawElementBackgroundColor(Rect rect, Color color)
		{
			GUI.DrawTexture(
				HorizontalAdjusted(rect),
				CreateColorTexture(color),
				ScaleMode.StretchToFill
			);
		}

		/// <summary>
		/// Adjusts horisontal parameters of <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private static Rect HorizontalAdjusted(Rect rect)
		{
			rect.x += LeftMargin;

			rect.width -= (LeftMargin + rightExtrusion);

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

	}
}