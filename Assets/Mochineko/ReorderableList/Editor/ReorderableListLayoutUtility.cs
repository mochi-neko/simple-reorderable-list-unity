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
		public const float singleHeightMargin = 2f;
		

		/// <summary>
		/// The width of a left margin on element background.
		/// </summary>
		private const float elementLeftWidthMargin = 1f;

		/// <summary>
		/// The width to adjust overflowing on element background.
		/// </summary>
		private const float elementRightWidthAdjuster = elementLeftWidthMargin + 2f;

		/// <summary>
		/// The width of grip marker in the left of reorderable list element. 
		/// </summary>
		private const float foldoutAdjustingWidth = 12f;


		/// <summary>
		/// Returns <paramref name="property"/> has foldout or not.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static bool HasFoldout(SerializedProperty property)
		{
			switch (property.propertyType)
			{
				case SerializedPropertyType.Generic:
					return true;

				case SerializedPropertyType.Vector4:
					return true;

				case SerializedPropertyType.Quaternion:
					return true;

				default:
					return false;
			}
		}

		/// <summary>
		/// Returns slided rect to adjust foldout.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public static Rect FoldoutSildedRect(Rect rect)
		{
			// slide to the right
			rect.x += foldoutAdjustingWidth;
			rect.width -= foldoutAdjustingWidth;

			return rect;
		}

		/// <summary>
		/// Draw texture to <paramref name="rect"/> by <paramref name="color"/>.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public static void DrawElementBackgroundColor(Rect rect, Color color)
		{
			// set up texture
			var texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, color);
			texture.Apply();

			// adjust element background layout in reorderable list
			rect.x += elementLeftWidthMargin;
			rect.width -= elementRightWidthAdjuster;

			// draw
			GUI.DrawTexture(rect, texture as Texture, ScaleMode.StretchToFill);
		}

	}
}
