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
		private const float singleLineHeightMargin = 4f;

		/// <summary>
		/// A height margin of multi properties.
		/// </summary>
		private const float multiPropertyHeightMargin = 6f;

		/// <summary>
		/// A single line height with margin.
		/// </summary>
		public static float SingleLineHeight
			=> EditorGUIUtility.singleLineHeight 
				+ singleLineHeightMargin;

		/// <summary>
		/// A multi properties height with margin. 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public static float MultiPropertyHeight(int count)
			=> SingleLineHeight * count	
				+ multiPropertyHeightMargin;

		/// <summary>
		/// A multi properties height in the property with margin. 
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static float MultiPropertyHeight(this SerializedProperty property)
			=> MultiPropertyHeight(property.CountActiveElements());
	}
}
