using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HogeComponent))]
public class HogeComponentEditor : Editor
{
	private Mochineko.ReorderableList.ReorderableListLayouter reorderableList;

	private void OnEnable()
	{
		reorderableList
			= new Mochineko.ReorderableList.ReorderableListLayouter(
				serializedObject.FindProperty("texts")
				);
	}

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();

		EditorGUI.BeginChangeCheck();
		{
			if (reorderableList != null)
				reorderableList.Layout();
		}
		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
	}
}
