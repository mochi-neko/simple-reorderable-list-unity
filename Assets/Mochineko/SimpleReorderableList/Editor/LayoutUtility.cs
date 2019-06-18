using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList
{
	/// <summary>
	/// Supplies layout utilities for reorderable list.
	/// </summary>
	public static class LayoutUtility
	{
	
		/// <summary>
		/// The height margin of a single element.
		/// </summary>
		private const float singleElementHeightMargin = 2f;
	
		/// <summary>
		/// Adjusts rect with foldout and height margin.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		public static Rect AdjustedRect(Rect rect, SerializedProperty property)
		{
			// adjust with foldout
			if (FoldoutUtility.HasFoldout(property))
				rect = FoldoutUtility.FoldoutSildedRect(rect);

			// adjust vertical center position with margin
			rect.y += singleElementHeightMargin;

			return rect;
		}

		/// <summary>
		/// Calculates element height of a <paramref name="property"/> with height margin.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static float ElementHeight(SerializedProperty property)
			=> EditorGUI.GetPropertyHeight(property)
				+ singleElementHeightMargin * 2f; // top and bottom
	}
}
