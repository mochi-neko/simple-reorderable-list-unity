using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	[CustomEditor(typeof(FixedElementCountSample))]
	public class FixedElementCountSampleEditor : UnityEditor.Editor
	{
		private ReorderableList reorderableList;

		private void OnEnable()
		{
			reorderableList = new ReorderableList(
				serializedObject.FindProperty("texts"),
				new NativeFunctionOptions(
					draggable: true,
					displayHeader: true,
					displayAddButton: false,
					displayRemoveButton: false)
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
