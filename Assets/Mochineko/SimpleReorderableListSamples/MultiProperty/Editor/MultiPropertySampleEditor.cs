using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	[CustomEditor(typeof(MultiPropertySample))]
	public class MultiPropertySampleEditor : UnityEditor.Editor
	{
		private ReorderableListLayouter layouter;

		private void OnEnable()
		{
			layouter = new ReorderableListLayouter(
				serializedObject.FindProperty("list")
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
