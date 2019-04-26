using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList.Samples.Editor
{
	[CustomEditor(typeof(SampleComponent))]
	public class SampleComponentEditor : UnityEditor.Editor
	{
		private ReorderableListLayouter singleLayouter;
		private ReorderableListLayouter sampleLayouter;
		private ReorderableListLayouter multiLayouter;

		private void OnEnable()
		{
			singleLayouter = new ReorderableListLayouter(
				serializedObject.FindProperty("singleSampleList")
			);

			sampleLayouter = new ReorderableListLayouter(
				serializedObject.FindProperty("sampleList")
			);

			multiLayouter = new ReorderableListLayouter(
				serializedObject.FindProperty("multiSampleList")
			);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			{
				EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

				if (singleLayouter != null)
					singleLayouter.Layout();

				if (sampleLayouter != null)
					sampleLayouter.Layout();

				if (multiLayouter != null)
					multiLayouter.Layout();
			}
			if (EditorGUI.EndChangeCheck())
			{
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
