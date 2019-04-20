using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEditorInternal;

namespace Mochineko.ReorderableList
{
	// Todo : Refactor and add summaries

	public class ReorderableListLayouter
	{
		private UnityEditorInternal.ReorderableList reorderable;
		private SerializedProperty listProperty;
		private SerializedProperty GetListElement(int index)
			=> listProperty.GetArrayElementAtIndex(index);
		private string DisplayName
			=> listProperty.displayName;
		private bool IsExpanded
		{
			get
			{
				return listProperty.isExpanded;
			}
			set
			{
				listProperty.isExpanded = value;
			}
		}

		#region Element Height

		public float PropertyHeigthMargin { private get; set; } = 1f;
		public float ElementHeightMargin { private get; set; } = 1f;

		private float SinglePropertyHeight
			=> EditorGUIUtility.singleLineHeight + PropertyHeigthMargin * 2f;

		private float SinglePropertyHeightWithElementMargin
			=> SinglePropertyHeight + ElementHeightMargin * 2f;

		private float MultiPropertyHeight(SerializedProperty property)
			=> SinglePropertyHeight * SerializedPropertyUtility.CountActiveElementIn(property) + ElementHeightMargin * 2f;

		private float AutoElementHeight(int index)
		{
			var element = GetListElement(index);

			if (SerializedPropertyUtility.IsSingleProperty(element))
				return SinglePropertyHeightWithElementMargin;
			else if (!element.isExpanded)
				return SinglePropertyHeightWithElementMargin;
			else
				return MultiPropertyHeight(element);
		}

		#endregion

		public ReorderableListLayouter(
			SerializedProperty listProperty, bool custom = false,
			bool draggable = true, bool displayHeader = true, bool displayAddButton = true, bool displayRemoveButton = true)
		{
			reorderable = new UnityEditorInternal.ReorderableList(
				listProperty.serializedObject, listProperty,
				draggable, displayHeader, displayAddButton, displayRemoveButton);

			this.listProperty = listProperty;

			if (custom)
				return;

			AddDrawHeader();
			AddDrawElementProperty();
			AddDrawElementBackground();
		}

		public void Layout(bool useFoldout = true)
		{
			if (reorderable == null)
				return;

			if (!useFoldout)
			{
				reorderable.DoLayoutList();
				return;
			}

			IsExpanded = EditorGUILayout.Foldout(IsExpanded, DisplayName);
			if (IsExpanded)
				reorderable.DoLayoutList();
		}

		#region Header

		public void AddDrawHeader()
		{
			if (reorderable == null)
				return;

			reorderable.drawHeaderCallback += DrawListName;
		}
		private void DrawListName(Rect rect)
		{
			EditorGUI.LabelField(rect, DisplayName);
		}

		public void AddDrawHeader(string label)
		{
			if (reorderable == null)
				return;

			reorderable.drawHeaderCallback += DrawListName;
		}
		private void DrawListName(Rect rect, string label)
		{
			EditorGUI.LabelField(rect, label);
		}

		#endregion

		#region Property

		public void AddDrawElementProperty()
		{
			if (reorderable == null)
				return;

			reorderable.drawElementCallback += LayoutPropertiesAuto;
			reorderable.elementHeightCallback += AutoElementHeight;
		}

		private void LayoutPropertiesAuto(Rect rect, int index, bool isActive, bool isFocused)
			=> LayoutProperty(GetListElement(index), index, rect);

		private void LayoutProperty(SerializedProperty property, int elementIndex, Rect rect)
		{
			var propertyRect = new Rect(rect)
			{
				height = EditorGUIUtility.singleLineHeight,
				y = rect.y + SinglePropertyHeight * elementIndex + PropertyHeigthMargin
			};

			EditorGUI.PropertyField(rect, property, true);
		}

		#endregion

		#region Background

		public Color ActiveColor { private get; set; } = new Color(0.2f, 0.7f, 0.95f, 0.95f);
		public Color ActivteColorPro { private get; set; } = new Color(0.43f, 0.59f, 0.73f, 0.95f);
		public Color CurrentActiveColor
		{
			get
			{
				if (!EditorGUIUtility.isProSkin)
					return ActiveColor;

				return ActivteColorPro;
			}
		}

		public Color BackgroundColor { private get; set; } = new Color(0.92f, 0.92f, 0.92f, 1f);
		public Color BackgroundColorPro { private get; set; } = new Color(0.54f, 0.54f, 0.54f, 1f);
		public Color CurrentBackgroundColor
		{
			get
			{
				if (!EditorGUIUtility.isProSkin)
					return BackgroundColor;

				return BackgroundColorPro;
			}
		}

		public void AddDrawElementBackground()
		{
			reorderable.drawElementBackgroundCallback +=
				(Rect rect, int index, bool isActive, bool isFocused)
					=> DrawElementBackgroundAlternatively(rect, index, isActive, isFocused);
		}

		private void DrawElementBackgroundAlternatively(Rect rect, int index, bool isActive, bool isFocused)
		{
			// isActive = true => last selected item
			// isfocused = true => current selected item

			if (isFocused)
			{
				DrawColor(CurrentActiveColor, rect);
				return;
			}

			if (index % 2 == 0)
			{
				//DrawColor(dark, rect);
				return;
			}

			DrawColor(CurrentBackgroundColor, rect);
		}

		private void DrawColor(Color color, Rect rect)
		{
			Texture2D tex = new Texture2D(1, 1);
			tex.SetPixel(0, 0, color);
			tex.Apply();

			GUI.DrawTexture(rect, tex as Texture);
		}

		#endregion

		#region Drop Down

		public void AddDrawDropDown(string[] canditateNames, System.Action<string> OnSelected)
		{
			reorderable.onAddDropdownCallback +=
				(rect, list)
					=> DrawDropDown(rect, canditateNames, OnSelected);
		}

		private void DrawDropDown(Rect rect, string[] canditateNames, System.Action<string> OnSelected)
		{
			var menu = new GenericMenu();

			foreach (var name in canditateNames)
				menu.AddItem(new GUIContent(name), false, () => OnSelected?.Invoke(name));

			menu.DropDown(rect);
		}

		#endregion


		#region Legacy

		//#region Single Property

		//public void AddDrawElementSingleProperty()
		//{
		//	if (reorderableList == null)
		//		return;

		//	reorderableList.elementHeight = SinglePropertyHeight;

		//	reorderableList.drawElementCallback +=
		//		(Rect rect, int index, bool isActive, bool isFocused)
		//			=> LayoutSingleProperty(index, rect);
		//}

		//private void LayoutSingleProperty(int index, Rect rect)
		//{
		//	var property = ListProperty.GetArrayElementAtIndex(index);

		//	var propertyRect = new Rect(rect)
		//	{
		//		height = EditorGUIUtility.singleLineHeight,
		//	};

		//	EditorGUI.PropertyField(propertyRect, property);
		//}

		//#endregion

		//#region Multi Property

		//public void AddDrawElementMultiProperty(string[] propertyNames)
		//{
		//	if (reorderableList == null)
		//		return;

		//	reorderableList.elementHeight =
		//		SinglePropertyHeight * propertyNames.Length + ElementHeightMargin * 2f;

		//	reorderableList.drawElementCallback +=
		//		(Rect rect, int index, bool isActive, bool isFocused)
		//			=> LayoutMultiPropertiesByName(propertyNames, rect, index, isActive, isFocused);
		//}

		//private void LayoutMultiPropertiesByName(string[] propertyNames, Rect rect, int index, bool isActive, bool isFocused)
		//{
		//	if (reorderableList == null)
		//		return;

		//	var element = ListProperty.GetArrayElementAtIndex(index);
		//	if (element == null)
		//		return;

		//	// add margin
		//	rect.y += ElementHeightMargin;

		//	for (int i = 0; i < propertyNames.Length; i++)
		//		LayoutChildPropertyByName(element, propertyNames[i], i, rect);
		//}

		//private void LayoutChildPropertyByName(SerializedProperty element, string propertyName, int propertyIndex, Rect rect)
		//{
		//	var property = element.FindPropertyRelative(propertyName);
		//	if (property == null)
		//		return;

		//	var propertyRect = new Rect(rect)
		//	{
		//		height = EditorGUIUtility.singleLineHeight,
		//		y = rect.y + SinglePropertyHeight * propertyIndex + PropertyHeigthMargin
		//	};

		//	EditorGUI.PropertyField(propertyRect, property, true);
		//}

		//#endregion

		//#region Foldout

		//public void AddDrawElementMultiPropertyFoldout(string[] propertyNames)
		//{
		//	if (reorderableList == null)
		//		return;

		//	reorderableList.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused)
		//		=> LayoutPropertiesFoldout(propertyNames, rect, index, isActive, isFocused);

		//	propertyCount = propertyNames.Length;
		//	reorderableList.elementHeightCallback += AutoElementHeight;
		//}

		//public void AddDrawElementMultiPropertyFoldout(string[] propertyNames, string labelPropertyName)
		//{
		//	if (reorderableList == null)
		//		return;

		//	reorderableList.drawElementCallback +=
		//		(Rect rect, int index, bool isActive, bool isFocused)
		//			=> LayoutPropertiesFoldout(propertyNames, labelPropertyName, rect, index, isActive, isFocused);

		//	propertyCount = propertyNames.Length;
		//	reorderableList.elementHeightCallback += AutoElementHeight;
		//}

		//private void LayoutPropertiesFoldout(string[] propertyNames, Rect rect, int index, bool isActive, bool isFocused)
		//{
		//	if (reorderableList == null)
		//		return;

		//	var element = ListProperty.GetArrayElementAtIndex(index);
		//	if (element == null)
		//		return;

		//	var foldoutRect = new Rect(rect)
		//	{
		//		x = rect.x + FoldoutMargin,
		//		y = rect.y + ElementHeightMargin,
		//		height = EditorGUIUtility.singleLineHeight,
		//	};

		//	element.isExpanded = EditorGUI.Foldout(foldoutRect, element.isExpanded, $"{element.type} {index}", true);

		//	if (element.isExpanded)
		//	{
		//		for (int i = 0; i < propertyNames.Length; i++)
		//			LayoutChildPropertyByName(element, propertyNames[i], i + 1, rect);
		//	}
		//}

		//private void LayoutPropertiesFoldout(string[] propertyNames, string[] disablePropertyNames, Rect rect, int index, bool isActive, bool isFocused)
		//{
		//	if (reorderableList == null)
		//		return;

		//	var element = ListProperty.GetArrayElementAtIndex(index);
		//	if (element == null)
		//		return;

		//	var foldoutRect = new Rect(rect)
		//	{
		//		x = rect.x + FoldoutMargin,
		//		y = rect.y + ElementHeightMargin,
		//		height = EditorGUIUtility.singleLineHeight,
		//	};

		//	element.isExpanded = EditorGUI.Foldout(foldoutRect, element.isExpanded, $"{element.type} {index}", true);

		//	if (element.isExpanded)
		//	{
		//		for (int i = 0; i < propertyNames.Length; i++)
		//			LayoutChildPropertyByName(element, propertyNames[i], i + 1, rect);

		//		EditorGUI.BeginDisabledGroup(true);
		//		{
		//			for (int i = 0; i < disablePropertyNames.Length; i++)
		//				LayoutChildPropertyByName(element, disablePropertyNames[i], i + 1 + propertyNames.Length, rect);
		//		}
		//		EditorGUI.EndDisabledGroup();
		//	}
		//}

		//private void LayoutPropertiesFoldout(string[] propertyNames, string labelPropertyName, Rect rect, int index, bool isActive, bool isFocused)
		//{
		//	if (reorderableList == null)
		//		return;

		//	var element = ListProperty.GetArrayElementAtIndex(index);
		//	if (element == null)
		//		return;

		//	var foldoutRect = new Rect(rect)
		//	{
		//		x = rect.x + FoldoutMargin,
		//		y = rect.y + ElementHeightMargin,
		//		height = EditorGUIUtility.singleLineHeight,
		//	};

		//	var label = element.FindPropertyRelative(labelPropertyName);
		//	if (label == null)
		//	{
		//		Debug.Log($"Not found string label : {labelPropertyName}");
		//		return;
		//	}

		//	element.isExpanded = EditorGUI.Foldout(foldoutRect, element.isExpanded, label.stringValue, true);

		//	if (element.isExpanded)
		//	{
		//		for (int i = 0; i < propertyNames.Length; i++)
		//			LayoutChildPropertyByName(element, propertyNames[i], i + 1, rect);
		//	}
		//}

		//private void LayoutPropertiesFoldout(string[] propertyNames, string[] disablePropertyNames, string labelPropertyName, Rect rect, int index, bool isActive, bool isFocused)
		//{
		//	if (reorderableList == null)
		//		return;

		//	var element = ListProperty.GetArrayElementAtIndex(index);
		//	if (element == null)
		//		return;

		//	var foldoutRect = new Rect(rect)
		//	{
		//		x = rect.x + FoldoutMargin,
		//		y = rect.y + ElementHeightMargin,
		//		height = EditorGUIUtility.singleLineHeight,
		//	};

		//	var label = element.FindPropertyRelative(labelPropertyName);
		//	if (label == null)
		//	{
		//		Debug.Log($"Not found string label : {labelPropertyName}");
		//		return;
		//	}

		//	element.isExpanded = EditorGUI.Foldout(foldoutRect, element.isExpanded, label.stringValue, true);

		//	if (element.isExpanded)
		//	{
		//		for (int i = 0; i < propertyNames.Length; i++)
		//			LayoutChildPropertyByName(element, propertyNames[i], i + 1, rect);

		//		EditorGUI.BeginDisabledGroup(true);
		//		{
		//			for (int i = 0; i < disablePropertyNames.Length; i++)
		//				LayoutChildPropertyByName(element, disablePropertyNames[i], i + 1 + propertyNames.Length, rect);
		//		}
		//		EditorGUI.EndDisabledGroup();
		//	}
		//}

		//#endregion

		#endregion

	}
}
