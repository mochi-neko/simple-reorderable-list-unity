﻿using System.Collections;
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
		/// Returns the property has multi children or not.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static bool IsMultiProperty(this SerializedProperty property)
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
		/// A number of element in a single property.
		/// Returns 1 for ordinary single properties, -1 for multi properties, and other value for irregular single properties.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static int ElementCountInSingleProperty(this SerializedProperty property)
		{
			switch (property.propertyType)
			{
				case SerializedPropertyType.Rect:
					return 2;
				case SerializedPropertyType.RectInt:
					return 2;

				case SerializedPropertyType.Bounds:
					return 3;
				case SerializedPropertyType.BoundsInt:
					return 3;

				case SerializedPropertyType.Generic:
					return -1;

				default:
					return 1;
			}
		}

		/// <summary>
		/// Returns the input is child of the parent or not.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static bool IsChildOf(this SerializedProperty property, SerializedProperty parent)
			=> property.depth > parent.depth;

		/// <summary>
		/// Returns the input is direct child of the parent or not.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static bool IsDirectChildOf(this SerializedProperty property, SerializedProperty parent)
			=> property.depth == parent.depth + 1;

	}
}