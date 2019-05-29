using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Mochineko.ReorderableList;

[CustomEditor(typeof(SandBox))]
public class SandBoxEditor : UnityEditor.Editor
{
	private ReorderableListLayouter layouter;

	private void OnEnable()
	{
		layouter = new ReorderableListLayouter(
			serializedObject.FindProperty("list"),
			new ReadyMadeDrawerOptions(false, false, false)
		);

		layouter.Native.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused)
			=> EditorGUI.PropertyField(rect, layouter.Native.serializedProperty.GetArrayElementAtIndex(index), true);

		layouter.Native.elementHeightCallback += (int index)
			=> EditorGUIUtility.singleLineHeight;

		layouter.Native.drawElementBackgroundCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
		{
			// selected element
			if (isFocused)
			{
				DrawBackgroundColor(rect, ActiveColor);
				return;
			}

			// odd element
			if (index % 2 != 0)
			{
				// draw default background color
				return;
			}

			// even element
			DrawBackgroundColor(rect, DifferentBackgroundColor);
		};
	}

	private void DrawBackgroundColor(Rect rect, Color color)
	{
		var texture = new Texture2D(1, 1);
		texture.SetPixel(0, 0, Color.red);
		texture.Apply();

		rect.x += 1f;
		rect.width -= 1f + 2f;

		GUI.DrawTexture(rect, texture as Texture, ScaleMode.StretchToFill);
	}

	private Color activeColorPersonal = new Color(0.3f, 0.6f, 0.95f, 0.95f);
	private  Color activteColorProfessional = new Color(0.2f, 0.4f, 0.7f, 0.95f);
	public Color ActiveColor
	{
		get
		{
			if (EditorGUIUtility.isProSkin)
				return activteColorProfessional;

			return activeColorPersonal;
		}
	}

	private Color backgroundColorPersonal = new Color(0.85f, 0.85f, 0.85f, 1f);
	private Color backgroundColorProfessional = new Color(0.25f, 0.25f, 0.25f, 1f);
	public Color DifferentBackgroundColor
	{
		get
		{
			if (EditorGUIUtility.isProSkin)
				return backgroundColorProfessional;

			return backgroundColorPersonal;
		}
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUI.BeginChangeCheck();
		{
			EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

			if (layouter != null)
				layouter.Layout();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("texts"), true);
		}
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.ApplyModifiedProperties();
		}
	}
}
