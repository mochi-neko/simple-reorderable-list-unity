using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList.Samples.Editor
{
	[CustomEditor(typeof(ExpandedPropertySample))]
	public class ExpandedPropertySampleEditor : UnityEditor.Editor
	{
		private ReorderableListLayouter layouter;

		private void OnEnable()
		{
			layouter = new ReorderableListLayouter(
				serializedObject.FindProperty("list"),
				false
			);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			{
				EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

				if (layouter != null)
					layouter.Layout();
			}
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
