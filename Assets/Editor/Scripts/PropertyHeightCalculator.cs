using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies height of a property.
	/// </summary>
	internal static class PropertyHeightCalculator
	{
		/// <summary>
		/// Calculates height of a property.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static float Height(this SerializedProperty property)
		{
			return
				EditorLayoutUtility.singleHeightMargin // top
				+ property.CalculatePropertyHeightRecursively()
				+ EditorLayoutUtility.singleHeightMargin; // bottom
		}
		
		/// <summary>
		/// Calculates height of a property including its children by recursion.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		private static float CalculatePropertyHeightRecursively(this SerializedProperty property)
		{
			// single property
			if (!property.IsMultiProperty())
			{
				return 
					EditorLayoutUtility.MultiPropertiesHeight(
						property.IrregularSinglePropertyCount()
					);
			}

			// fold out
			if (!property.isExpanded)
				return EditorLayoutUtility.SinglePropertyHeight;

			var parent = property.Copy();
			var child = property.Copy();
			var height = EditorLayoutUtility.SinglePropertyHeight;

			// count only direct children
			while (child.NextVisible(true))
			{
				if (!child.IsChildOf(parent))
					break;
				if (!child.IsDirectChildOf(parent))
					continue;

				// count recursively
				height += child.CalculatePropertyHeightRecursively();
			}

			return height;
		}
	}
}