using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList.Samples.Editor
{
	[CustomEditor(typeof(SampleComponent))]
	public class SampleComponentEditor : UnityEditor.Editor
	{
		private ReorderableListLayouter layouter;

		private void OnEnable()
		{
			var property = serializedObject.FindProperty("sampleList");

			layouter = new ReorderableListLayouter(property);
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
