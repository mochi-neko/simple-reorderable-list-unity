using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies calculation of height of a property.
	/// </summary>
	public static class PropertyHeightCalculator
	{
		/// <summary>
		/// Calculates height of <paramref name="property"/>.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static float Height(this SerializedProperty property)
		{
			return
				ReorderableListLayoutUtility.singleHeightMargin // top margin
				+ property.CalculatePropertyHeightRecursively() // properties
				+ ReorderableListLayoutUtility.singleHeightMargin; // bottom margin
		}

		/// <summary>
		/// Calculates height of <paramref name="property"/> including its descendant by recursion.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		private static float CalculatePropertyHeightRecursively(this SerializedProperty property)
		{
			// single property
			if (!property.HasMultiPropertiesWithNoGeneric())
			{
				return ReorderableListLayoutUtility.MultiPropertiesHeight(
					property.PropertyCountInSingleProperty()
				);
			}

			// fold out
			if (!property.isExpanded)
				return ReorderableListLayoutUtility.SinglePropertyHeight;

			// make copied
			var child = property.Copy();
			var height = ReorderableListLayoutUtility.SinglePropertyHeight;

			// count only children
			while (child.NextVisible(true))
			{
				if (!child.IsDescendantOf(property))
					break;
				if (!child.IsChildOf(property))
					continue;

				// count recursively
				height += child.CalculatePropertyHeightRecursively();
			}

			return height;
		}
	}
}