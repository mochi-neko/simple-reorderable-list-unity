using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	[CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
	public class ReadOnlyFieldDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginDisabledGroup(true);
			{
				EditorGUI.PropertyField(position, property, label);
			}
			EditorGUI.EndDisabledGroup();
		}
	}
}
