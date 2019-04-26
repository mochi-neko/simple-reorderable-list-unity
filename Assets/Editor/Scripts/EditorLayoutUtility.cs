using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies editor layout utilities.
	/// </summary>
	internal static class EditorLayoutUtility
	{
		/// <summary>
		/// A height margin of a single line.  
		/// </summary>
		public const float singleLineHeightMargin = 2f;

		/// <summary>
		/// A height margin of multi properties.
		/// </summary>
		private const float multiPropertiesHeightMargin = 8f;

		/// <summary>
		/// The width of grip marker in the left of reorderable list element. 
		/// </summary>
		public const float gripWidth = 12f;

		/// <summary>
		/// A single line height with margin.
		/// </summary>
		public static float SingleLineHeight
			=> EditorGUIUtility.singleLineHeight
				+ singleLineHeightMargin * 2f; // top and bottom

		/// <summary>
		/// A multi properties height with margin. 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public static float MultiPropertiesHeight(int count)
			=> EditorGUIUtility.singleLineHeight * count
				+ singleLineHeightMargin * (count + 1); // Element Margin * 2 + Property Margin * (count - 1)

		/// <summary>
		/// A multi properties height in the property with margin. 
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static float MultiPropertiesHeight(this SerializedProperty property)
			=> MultiPropertiesHeight(property.CountActiveElements());
	}
}
