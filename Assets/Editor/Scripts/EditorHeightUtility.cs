using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	public static class EditorHeightUtility
	{
		private const float PropertyHeigthMargin = 1f;
		private const float ElementHeightMargin = 1f;

		public static float SinglePropertyHeightWithMargin
			=> EditorGUIUtility.singleLineHeight 
				+ PropertyHeigthMargin * 2f;

		public static float SinglePropertyHeightWithElementMargin
			=> SinglePropertyHeightWithMargin 
				+ ElementHeightMargin * 2f;

		public static float MultiPropertyHeightWithMargin(int index)
			=> SinglePropertyHeightWithMargin * index
				+ PropertyHeigthMargin;

		public static float MultiPropertyHeightWithMargin(this SerializedProperty property)
			=> SinglePropertyHeightWithMargin * SerializedPropertyUtility.CountActiveElements(property) 
				+ ElementHeightMargin * 2f;
	}
}
