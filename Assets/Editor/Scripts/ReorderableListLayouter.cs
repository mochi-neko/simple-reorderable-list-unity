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
		protected UnityEditorInternal.ReorderableList native;

		#region Serialized Property

		protected SerializedProperty listProperty;

		protected SerializedProperty GetListElement(int index)
			=> listProperty.GetArrayElementAtIndex(index);

		protected string DisplayName
			=> listProperty.displayName;

		protected bool IsExpanded
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

		#endregion

		public ReorderableListLayouter(
			SerializedProperty listProperty, bool custom = false,
			bool draggable = true, bool displayHeader = true, bool displayAddButton = true, bool displayRemoveButton = true)
		{
			native = new UnityEditorInternal.ReorderableList(
				listProperty.serializedObject, listProperty,
				draggable, displayHeader, displayAddButton, displayRemoveButton);

			this.listProperty = listProperty;

			if (custom)
				return;

			AddDrawHeader();
			AddDrawElementProperty();
			AddDrawElementBackground();
		}

		public virtual void Layout(bool useFoldout = true)
		{
			if (native == null)
				return;

			if (!useFoldout)
			{
				native.DoLayoutList();
				return;
			}

			IsExpanded = EditorGUILayout.Foldout(IsExpanded, DisplayName);
			if (IsExpanded)
				native.DoLayoutList();
		}

		#region Header

		public void AddDrawHeader()
		{
			if (native == null)
				return;

			native.drawHeaderCallback += DrawListName;
		}
		public void AddDrawHeader(string label)
		{
			if (native == null)
				return;

			native.drawHeaderCallback += DrawListName;
		}

		protected virtual void DrawListName(Rect rect)
		{
			EditorGUI.LabelField(rect, DisplayName);
		}
		protected virtual void DrawListName(Rect rect, string label)
		{
			EditorGUI.LabelField(rect, label);
		}

		#endregion

		#region Property

		public void AddDrawElementProperty()
		{
			if (native == null)
				return;

			native.drawElementCallback += LayoutProperty;
			native.elementHeightCallback += ElementHeight;
		}

		private void LayoutProperty(Rect rect, int index, bool isActive, bool isFocused)
			=> LayoutProperty(GetListElement(index), index, rect);

		private void LayoutProperty(SerializedProperty property, int elementIndex, Rect rect)
		{
			if (property == null)
				return;

			EditorGUI.PropertyField(rect, property, true);
		}

		private float ElementHeight(int index)
		{
			var element = GetListElement(index);

			if (element.IsSingleProperty())
				return EditorHeightUtility.SinglePropertyHeight;
			else if (!element.isExpanded)
				return EditorHeightUtility.SinglePropertyHeight;
			else
				return element.MultiPropertyHeight();
		}

		#endregion

		#region Background

		public void AddDrawElementBackground()
		{
			if (native == null)
				return;

			native.drawElementBackgroundCallback +=
				(Rect rect, int index, bool isActive, bool isFocused)
					=> DrawElementBackgroundAlternatively(rect, index, isActive, isFocused);
		}

		private void DrawElementBackgroundAlternatively(Rect rect, int index, bool isActive, bool isFocused)
		{
			// isActive = true => last selected item
			// isfocused = true => current selected item

			if (isFocused)
			{
				rect.DrawColor(EditorColorUtility.EffectiveActiveColor);
				return;
			}

			if (index % 2 == 0)
			{
				return;
			}

			rect.DrawColor(EditorColorUtility.EffectiveBackgroundColor);
		}

		

		#endregion

		#region Drop Down

		public void AddDrawDropDown(string[] canditateNames, System.Action<string> OnSelected)
		{
			if (native == null)
				return;

			native.onAddDropdownCallback += (rect, list)
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

		
	}
}
