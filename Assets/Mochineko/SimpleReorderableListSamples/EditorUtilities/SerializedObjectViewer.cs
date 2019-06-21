using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList.Samples.Editor
{
	/// <summary>
	/// Supplies a debbugging viewer of <see cref="SerializedObject"/> and <see cref="SerializedProperty"/>.
	/// </summary>
	public static class SerializedObjectViewer
	{
		/// <summary>
		/// Draws all properties in <paramref name="serializedObject"/> including invibible these.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawAll(SerializedObject serializedObject)
		{
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				var iterator = serializedObject.GetIterator();

				while (iterator.Next(true))
					PropertyInfoField(iterator.Copy());
			}
			EditorGUILayout.EndVertical();
		}

		/// <summary>
		/// Draws properties in <paramref name="serializedObject"/> only visible these.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawVisible(SerializedObject serializedObject)
		{
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				var iterator = serializedObject.GetIterator();

				while (iterator.NextVisible(true))
					PropertyInfoField(iterator.Copy());
			}
			EditorGUILayout.EndVertical();
		}

		/// <summary>
		/// Draws all properties in <paramref name="property"/> including invibible these.
		/// </summary>
		/// <param name="property"></param>
		public static void DrawAll(SerializedProperty property)
		{
			var depth = property.depth;
			var copied = property.Copy();

			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				PropertyInfoField(property);

				while (copied.Next(true))
				{
					if (copied.depth <= depth)
						break;

					PropertyInfoField(copied.Copy());
				}
			}
			EditorGUILayout.EndVertical();
		}

		/// <summary>
		/// Draws properties in <paramref name="property"/> only visible these.
		/// </summary>
		/// <param name="property"></param>
		public static void DrawVisible(SerializedProperty property)
		{
			var depth = property.depth;
			var copied = property.Copy();

			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				PropertyInfoField(property);

				while (copied.NextVisible(true))
				{
					if (copied.depth <= depth)
						break;

					PropertyInfoField(copied.Copy());
				}
			}
			EditorGUILayout.EndVertical();
		}


		/// <summary>
		/// Draws informations of <paramref name="property"/> by text with margin by depth.
		/// </summary>
		/// <param name="property"></param>
		private static void PropertyInfoField(SerializedProperty property)
		{
			EditorGUILayout.LabelField($"{Nest(property.depth)}{property.displayName} : <{property.propertyType}>");
		}

		/// <summary>
		/// Nest text by <paramref name="depthCount"/>.
		/// </summary>
		/// <param name="depthCount"></param>
		/// <returns></returns>
		private static string Nest(int depthCount)
		{
			var builder = new System.Text.StringBuilder();

			for (var i = 0; i < depthCount; i++)
				builder.Append(" | ");

			if (depthCount > 0)
				builder.Append("- ");

			return builder.ToString();
		}
	}
}
