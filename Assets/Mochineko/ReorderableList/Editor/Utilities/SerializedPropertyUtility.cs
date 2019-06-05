using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies utilities for <see cref="SerializedProperty"/>.
	/// </summary>
	public static class SerializedPropertyUtility
	{

		/// <summary>
		/// Returns <paramref name="property"/> has multi properties in children or not.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static bool HasMultiPropertiesWithNoGeneric(this SerializedProperty property)
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
		/// A number of property in a single property's <paramref name="property"/>.
		/// Returns 
		/// 1 for ordinary single properties,
		/// -1 for generic properties and 
		/// other for irregular single properties.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static int PropertyCountInSingleProperty(this SerializedProperty property)
		{
			switch (property.propertyType)
			{
				// irregulars
				case SerializedPropertyType.Rect:
					return 2;
				case SerializedPropertyType.RectInt:
					return 2;

				case SerializedPropertyType.Bounds:
					return 3;
				case SerializedPropertyType.BoundsInt:
					return 3;

				// generics
				case SerializedPropertyType.Generic:
					return -1;

				// ordinaries
				default:
					return 1;
			}
		}

		/// <summary>
		/// Returns <paramref name="property"/> is a descendant of <paramref name="parent"/> or not.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static bool IsDescendantOf(this SerializedProperty property, SerializedProperty parent)
			=> property.depth > parent.depth;

		/// <summary>
		/// Returns <paramref name="property"/> is a child of <paramref name="parent"/> or not.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static bool IsChildOf(this SerializedProperty property, SerializedProperty parent)
			=> property.depth == parent.depth + 1;


		/// <summary>
		/// Calculates count of descendant of <paramref name="property"/>.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static int CountDescendantRecursively(this SerializedProperty property)
		{
			// single property
			if (!property.HasMultiPropertiesWithNoGeneric())
				return 1;

			// fold out
			if (!property.isExpanded)
				return 1;

			// make copied
			var child = property.Copy();
			var count = 1;

			// count only children
			while (child.NextVisible(true))
			{
				if (!child.IsDescendantOf(property))
					break;
				if (!child.IsChildOf(property))
					continue;

				// count recursively
				count += child.CountDescendantRecursively();
			}

			return count;
		}
	}
}
