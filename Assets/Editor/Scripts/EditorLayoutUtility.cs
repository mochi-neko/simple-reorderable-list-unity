using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies editor layout utilities.
	/// </summary>
	internal static class EditorLayoutUtility
	{
	
		public const float singleHeightMargin = 1f;

		/// <summary>
		/// The width of grip marker in the left of reorderable list element. 
		/// </summary>
		public const float gripWidth = 12f;

		private const float elementLeftMargin = 1f;

		private const float elementRightAdjuster = 3f;

		/// <summary>
		/// A single line height with margin.
		/// </summary>
		public static float SinglePropertyHeight
			=> EditorGUIUtility.singleLineHeight
				+ singleHeightMargin * 2f; // top and bottom

		public static float MultiPropertiesHeight(int count)
			=> EditorGUIUtility.singleLineHeight * count
				+ singleHeightMargin * 2f; // top and bottom


		/// <summary>
		/// Draw texture to the rect by color.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public static void DrawElementColor(this Rect rect, Color color)
		{
			var texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, color);
			texture.Apply();

			// adjust element background layout in reorderable list
			rect.x += elementLeftMargin;
			rect.width -= elementRightAdjuster;

			GUI.DrawTexture(rect, texture as Texture, ScaleMode.StretchToFill);
		}
	}
}
