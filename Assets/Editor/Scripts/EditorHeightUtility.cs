using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies editor line height utilities.
	/// </summary>
	internal static class EditorHeightUtility
	{
		/// <summary>
		/// A height margin of a single line.  
		/// </summary>
		private const float singleLineHeightMargin = 1f;

		/// <summary>
		/// A single line height with margin.
		/// </summary>
		public static float SingleLineHeightWithMargin
			=> EditorGUIUtility.singleLineHeight 
				+ singleLineHeightMargin * 2f;

		/// <summary>
		/// A single property height with margin.
		/// </summary>
		public static float SinglePropertyHeight
			=> SingleLineHeightWithMargin 
				+ singleLineHeightMargin * 2f;

		/// <summary>
		/// A multi properties height with margin. 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public static float MultiPropertyHeight(int count)
			=> SingleLineHeightWithMargin * count
				+ singleLineHeightMargin * 2f;
		/// <summary>
		/// A multi properties height in the property with margin. 
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static float MultiPropertyHeight(this SerializedProperty property)
			=> MultiPropertyHeight(property.CountActiveElements());
	}
}
