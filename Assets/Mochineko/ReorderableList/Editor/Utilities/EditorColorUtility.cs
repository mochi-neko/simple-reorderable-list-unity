using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies editor color utilities.
	/// </summary>
	public static class EditorColorUtility
	{
		/// <summary>
		/// Active color on personal skin.
		/// </summary>
		private static readonly Color activeColorPersonal = new Color(0.3f, 0.6f, 0.95f, 0.95f);
		/// <summary>
		/// Active color on professional skin.
		/// </summary>
		private static readonly Color activteColorProfessional = new Color(0.2f, 0.4f, 0.7f, 0.95f);
		/// <summary>
		/// Suppies active color on user skin.
		/// </summary>
		public static Color ActiveColor
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return activteColorProfessional;
				else
					return activeColorPersonal;
			}
		}

		/// <summary>
		/// Background color on personal skin.
		/// </summary>
		private static Color backgroundColorPersonal = new Color(0.85f, 0.85f, 0.85f, 1f);
		/// <summary>
		/// Background color on professional skin.
		/// </summary>
		private static Color backgroundColorProfessional = new Color(0.25f, 0.25f, 0.25f, 1f);
		/// <summary>
		/// Supplies background color for user skin.
		/// </summary>
		public static Color DifferentBackgroundColor
		{
			get
			{
				if (EditorGUIUtility.isProSkin)
					return backgroundColorProfessional;
				else
					return backgroundColorPersonal;
			}
		}

	
	}
}
