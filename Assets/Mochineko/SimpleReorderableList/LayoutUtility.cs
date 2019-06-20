using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList
{
	/// <summary>
	/// Supplies layout utilities for reorderable list.
	/// </summary>
	public static class LayoutUtility
	{

		#region Foldout

		/// <summary>
		/// The width of grip marker in the left of reorderable list element. 
		/// </summary>
		private const float foldoutAdjustingWidth = 12f;

		/// <summary>
		/// Returns <paramref name="property"/> has foldout or not.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		private static bool HasFoldout(SerializedProperty property)
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
		private static Rect FoldoutSildedRect(Rect rect)
		{
			// slide to the right
			rect.x += foldoutAdjustingWidth;
			rect.width -= foldoutAdjustingWidth;

			return rect;
		}

		#endregion

		#region Rect and Height

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
			if (HasFoldout(property))
				rect = FoldoutSildedRect(rect);

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

		#endregion

	}
}
