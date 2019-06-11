using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

namespace Mochineko.ReorderableList.ScriptCreator
{
	public class ScriptCreatorWindow : EditorWindow
	{
		[SerializeField]
		private MonoScript source;

		[SerializeField]
		[ReadOnlyField]
		private List<string> fieldNames;

		private SerializedObject serializedObject;
		private ReorderableListLayouter layouter;
		private string[] fieldNameCanditates;

		private void OnEnable()
		{
			serializedObject = new SerializedObject(this);

			layouter = new ReorderableListLayouter(serializedObject.FindProperty("fieldNames"), false);

			FindCanditates();

			layouter.AddDrawDropDownCallback(fieldNameCanditates, OnSelected);
		}

		private void FindCanditates()
		{
			if (source == null)
				return;

			fieldNameCanditates =
				source
				.GetClass()
				.GetRuntimeFields()
				.Where((info) => !info.IsNotSerialized)
				.Select((info) => info.Name)
				.ToArray();
		}

		private void OnSelected(string selected)
		{
			if (string.IsNullOrEmpty(selected))
				return;
			if (fieldNames.Contains(selected))
				return;

			fieldNames.Add(selected);
		}

		[MenuItem("Reorderable List/Editor Script Creator")]
		public static void Open()
		{
			GetWindow<ScriptCreatorWindow>("Script Creator");
		}

		private void OnGUI()
		{
			if (serializedObject == null)
				return;

			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("source"));

				if (layouter != null)
					layouter.Layout();

				if (GUILayout.Button("Create Script"))
					ScriptCreatorMenu.CreateCsScript(source.name, fieldNames.ToArray());
			}
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
		}
	}
}
