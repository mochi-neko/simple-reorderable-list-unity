using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	[CustomEditor(typeof(MultiPropertySample))]
	public class MultiPropertySampleEditor : UnityEditor.Editor
	{
		private ReorderableList reorderableList;

		private void OnEnable()
		{
			reorderableList = new ReorderableList(
				serializedObject.FindProperty("list")
			);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			{
				EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

				if (reorderableList != null)
					reorderableList.Layout();
			}
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
