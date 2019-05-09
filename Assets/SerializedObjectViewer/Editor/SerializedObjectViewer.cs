using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko
{
	public static class SerializedObjectViewer
	{
		public static void LayoutAll(SerializedObject serializedObject)
		{
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				var iterator = serializedObject.GetIterator();

				while (iterator.Next(true))
					PropertyInfoField(iterator.Copy());
			}
			EditorGUILayout.EndVertical();
		}

		public static void LayoutVisible(SerializedObject serializedObject)
		{
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				var iterator = serializedObject.GetIterator();

				while (iterator.NextVisible(true))
					PropertyInfoField(iterator.Copy());
			}
			EditorGUILayout.EndVertical();
		}

		public static void LayoutAll(SerializedProperty property)
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

		public static void LayoutVisible(SerializedProperty property)
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

		private static void PropertyInfoField(SerializedProperty property)
		{
			EditorGUILayout.LabelField($"{Nest(property.depth)}{property.displayName} : <{property.propertyType}>");
		}

		private static string Nest(int count)
		{
			var builder = new System.Text.StringBuilder();

			for (var i = 0; i < count; i++)
				builder.Append(" | ");

			if (count > 0)
				builder.Append("- ");

			return builder.ToString();
		}
	}
}
