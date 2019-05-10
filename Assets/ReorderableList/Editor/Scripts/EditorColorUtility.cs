using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies editor color utilities.
	/// </summary>
	internal static class EditorColorUtility
	{
		/// <summary>
		/// Active color in personal skin.
		/// </summary>
		private static readonly Color activeColorPersonal = new Color(0.3f, 0.6f, 0.95f, 0.95f);
		/// <summary>
		/// Active color in professional skin.
		/// </summary>
		private static readonly Color activteColorProfessional = new Color(0.2f, 0.4f, 0.7f, 0.95f);
		/// <summary>
		/// Suppies active color for user skin.
		/// </summary>
		public static Color ActiveColor
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return activteColorProfessional;

				return activeColorPersonal;
			}
		}

		/// <summary>
		/// Background color in personal skin.
		/// </summary>
		private static Color backgroundColorPersonal = new Color(0.85f, 0.85f, 0.85f, 1f);
		/// <summary>
		/// Background color in professional skin.
		/// </summary>
		private static Color backgroundColorProfessional = new Color(0.25f, 0.25f, 0.25f, 1f);
		/// <summary>
		/// Supplies background color for user skin.
		/// </summary>
		public static Color DifferentBackgroundColor
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return backgroundColorProfessional;

				return backgroundColorPersonal;
			}
		}

		/// <summary>
		/// Draw texture to the rect by color.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public static void DrawElementColor(Rect rect, Color color)
		{
			var texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, color);
			texture.Apply();

			// adjust element background layout in reorderable list
			rect.x += EditorLayoutUtility.elementLeftWidthMargin;
			rect.width -= EditorLayoutUtility.elementRightWidthAdjuster;

			GUI.DrawTexture(rect, texture as Texture, ScaleMode.StretchToFill);
		}

	}
}
