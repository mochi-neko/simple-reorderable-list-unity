using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	[CustomEditor(typeof(DropDownSample))]
	public class DropDownSampleEditor : UnityEditor.Editor
	{
		private ReorderableList layouter;
		private DropDownSample component;

		private void OnEnable()
		{
			if (component == null)
				component = target as DropDownSample;

			layouter = new ReorderableList(
				serializedObject.FindProperty("humans"),
				new NativeFunctionOptions(false)
			);

			layouter.AddDrawDropDownCallback(
				DropDownSample.bloodTypeCanditates, 
				(selected) => component.Humans.Add(new DropDownSample.Human(selected))
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
