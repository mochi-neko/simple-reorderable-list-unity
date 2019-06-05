using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies editor layout utilities for reorderable list.
	/// </summary>
	public static class ReorderableListLayoutUtility
	{
	
		/// <summary>
		/// The height of a single element on one side.
		/// </summary>
		public const float singleHeightMargin = 1f;

		/// <summary>
		/// The width of grip marker in the left of reorderable list element. 
		/// </summary>
		public const float gripMarkerWidth = 12f;

		/// <summary>
		/// The width of a left margin on element background.
		/// </summary>
		public const float elementLeftWidthMargin = 1f;

		/// <summary>
		/// The width to adjust overflowing on element background.
		/// </summary>
		public const float elementRightWidthAdjuster = elementLeftWidthMargin + 2f;

		/// <summary>
		/// A single line height with margin.
		/// </summary>
		public static float SinglePropertyHeight
			=> EditorGUIUtility.singleLineHeight
				+ singleHeightMargin * 2f; // top and bottom

		/// <summary>
		/// A multi line height with only margin on top and bottom.
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public static float MultiPropertiesHeight(int count)
			=> EditorGUIUtility.singleLineHeight * count
				+ singleHeightMargin * 2f; // top and bottom


		/// <summary>
		/// Draw texture to <paramref name="rect"/> by <paramref name="color"/>.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public static void DrawElementBackgroundColor(Rect rect, Color color)
		{
			var texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, color);
			texture.Apply();

			// adjust element background layout in reorderable list
			rect.x += elementLeftWidthMargin;
			rect.width -= elementRightWidthAdjuster;

			GUI.DrawTexture(rect, texture as Texture, ScaleMode.StretchToFill);
		}

	}
}
