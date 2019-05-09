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
		/// Active color in defalt skin.
		/// </summary>
		private static readonly Color activeColor = new Color(0.5f, 0.7f, 0.95f, 0.95f);
		/// <summary>
		/// Active color in pro skin.
		/// </summary>
		private static readonly Color activteColorPro = new Color(0.2f, 0.4f, 0.7f, 0.95f);
		/// <summary>
		/// Suppies active color for user skin.
		/// </summary>
		public static Color ActiveColor
		{
			get
			{
				if (!EditorGUIUtility.isProSkin)
					return activeColor;

				return activteColorPro;
			}
		}

		/// <summary>
		/// Background color in defalut skin.
		/// </summary>
		private static Color backgroundColor = new Color(0.85f, 0.85f, 0.85f, 1f);
		/// <summary>
		/// Background color in pro skin.
		/// </summary>
		private static Color backgroundColorPro = new Color(0.25f, 0.25f, 0.25f, 1f);
		/// <summary>
		/// Supplies background color for user skin.
		/// </summary>
		public static Color DifferentBackgroundColor
		{
			get
			{
				if (!EditorGUIUtility.isProSkin)
					return backgroundColor;

				return backgroundColorPro;
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
