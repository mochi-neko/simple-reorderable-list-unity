using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies utilities for <see cref="SerializedProperty"/>.
	/// </summary>
	internal static class SerializedPropertyUtility
	{
		/// <summary>
		/// Returns the input is child of the parent or not.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		private static bool IsChildOf(this SerializedProperty property, SerializedProperty parent)
			=> property.depth > parent.depth;

		/// <summary>
		/// Returns the input is direct child of the parent or not.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		private static bool IsDirectChildOf(this SerializedProperty property, SerializedProperty parent)
			=> property.depth == parent.depth + 1;

		/// <summary>
		/// Counts the number of active elements included in the property.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static int CountActiveElements(this SerializedProperty property)
		{
			if (IsSingleProperty(property))
				return 1;
			if (!property.isExpanded)
				return 1;

			var parent = property.Copy();
			var copy = property.Copy();
			var count = 1;

			// count only direct children
			while (copy.NextVisible(true))
			{
				if (!copy.IsChildOf(parent))
					break;
				if (!copy.IsDirectChildOf(parent))
					continue;

				// count recursively
				count += CountActiveElements(copy);
			}

			return count;
		}

		/// <summary>
		/// Returns the property is singlet or not.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static bool IsSingleProperty(this SerializedProperty property)
			=> property.propertyType != SerializedPropertyType.Generic;
	
	}
}
