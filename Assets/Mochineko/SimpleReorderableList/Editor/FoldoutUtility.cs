using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList
{
	// Todo
	public class FoldoutUtility
	{
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


	}
}
