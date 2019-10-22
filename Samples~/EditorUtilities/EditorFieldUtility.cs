using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	/// <summary>
	/// Supplies editor field utilities.
	/// </summary>
	public static class EditorFieldUtility
	{
		/// <summary>
		/// The label of editor script.
		/// </summary>
		private const string editorLabel = "Editor";
		/// <summary>
		/// The label of script.
		/// </summary>
		private const string scriptLabel = "Script";
		/// <summary>
		/// The label of instance of a scriptable object.
		/// </summary>
		private const string scriptableInstanceLabel = "Instance";

		/// <summary>
		/// Draw a read only filed of an editor script.
		/// </summary>
		/// <param name="editor"></param>
		public static void ReadOnlyEditorField(ScriptableObject editor)
		{
			EditorGUI.BeginDisabledGroup(true);
			{
				ScriptField(editor, editorLabel);
			}
			EditorGUI.EndDisabledGroup();
		}

		/// <summary>
		/// Draw a read only field of a component script with editor.
		/// </summary>
		/// <param name="script"></param>
		/// <param name="editor"></param>
		public static void ReadOnlyComponentField(MonoBehaviour script, ScriptableObject editor)
		{
			EditorGUI.BeginDisabledGroup(true);
			{
				ScriptField(script, scriptLabel);
				ScriptField(editor, editorLabel);
			}
			EditorGUI.EndDisabledGroup();
		}

		/// <summary>
		/// Draw a read only field of a scriptable object with editor and instnce.
		/// </summary>
		/// <param name="scriptable"></param>
		/// <param name="editor"></param>
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

		/// <summary>
		/// Draw a mono script from a scriptable object with label.
		/// </summary>
		/// <param name="scriptable"></param>
		/// <param name="label"></param>
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
		/// <summary>
		/// Draw a mono script from a MonoBehaviour with label.
		/// </summary>
		/// <param name="script"></param>
		/// <param name="label"></param>
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

		/// <summary>
		/// Draw an instance of a scriptable object with label.
		/// </summary>
		/// <param name="scriptable"></param>
		/// <param name="label"></param>
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
