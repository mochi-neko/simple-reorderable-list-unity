using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	public static class PropertyHeightCalculator
	{
		public static float Height(this SerializedProperty property)
		{
			return
				EditorLayoutUtility.singleHeightMargin // top
				+ property.CalculateElementsHeight()
				+ EditorLayoutUtility.singleHeightMargin; // bottom
		}
		
		private static float CalculateElementsHeight(this SerializedProperty property)
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
				height += child.CalculateElementsHeight();
			}

			return height;
		}
	}
}