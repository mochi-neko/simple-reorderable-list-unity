using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	public static class EditorColorUtility 
	{
		private static readonly Color activeColor = new Color(0.2f, 0.7f, 0.95f, 0.95f);
		private static readonly Color activteColorPro = new Color(0.43f, 0.59f, 0.73f, 0.95f);
		public static Color EffectiveActiveColor
		{
			get
			{
				if (!EditorGUIUtility.isProSkin)
					return activeColor;

				return activteColorPro;
			}
		}

		private static Color backgroundColor = new Color(0.92f, 0.92f, 0.92f, 1f);
		private static Color backgroundColorPro = new Color(0.54f, 0.54f, 0.54f, 1f);
		public static Color EffectiveBackgroundColor
		{
			get
			{
				if (!EditorGUIUtility.isProSkin)
					return backgroundColor;

				return backgroundColorPro;
			}
		}

		public static void DrawColor(this Rect rect, Color color)
		{
			var tex = new Texture2D(1, 1);
			tex.SetPixel(0, 0, color);
			tex.Apply();

			GUI.DrawTexture(rect, tex as Texture);
		}

	}
}
