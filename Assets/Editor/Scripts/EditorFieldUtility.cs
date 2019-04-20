using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	// Todo : Add summaries

	public static class EditorFieldUtility
	{
		private const string editorLabel = "Editor";
		private const string scriptLabel = "Script";
		private const string scriptableInstanceLabel = "Instance";


		public static void ReadOnlyEditorField(ScriptableObject editor)
		{
			EditorGUI.BeginDisabledGroup(true);
			{
				ScriptField(editor, editorLabel);
			}
			EditorGUI.EndDisabledGroup();
		}

		public static void ReadOnlyComponentField(MonoBehaviour script, ScriptableObject editor)
		{
			EditorGUI.BeginDisabledGroup(true);
			{
				ScriptField(script, scriptLabel);
				ScriptField(editor, editorLabel);
			}
			EditorGUI.EndDisabledGroup();
		}

		public static void ReadOnlyScriptableObjectField(ScriptableObject scriptable, ScriptableObject editor)
		{
			EditorGUI.BeginDisabledGroup(true);
			{
				ScriptField(scriptable, scriptLabel);
				ScriptField(editor, editorLabel);
				ScriptableObjectField(scriptable, scriptableInstanceLabel);
			}
			EditorGUI.EndDisabledGroup();
		}


		private static void ScriptField(ScriptableObject scriptable, string label)
		{
			if (scriptable == null)
				return;

			EditorGUILayout.ObjectField(
				label,
				MonoScript.FromScriptableObject(scriptable),
				typeof(MonoScript),
				false
			);
		}

		private static void ScriptField(MonoBehaviour script, string label)
		{
			if (script == null)
				return;

			EditorGUILayout.ObjectField(
				label,
				MonoScript.FromMonoBehaviour(script),
				typeof(MonoScript),
				false
			);
		}

		private static void ScriptableObjectField(ScriptableObject scriptable, string label)
		{
			EditorGUILayout.ObjectField(
				label, 
				scriptable, 
				typeof(ScriptableObject), 
				false
			);
		}
	}
}
