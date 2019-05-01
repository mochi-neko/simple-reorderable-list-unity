using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	// ToDo : Add summaries
	// WillDo : Support big height properties, e.g. Texture, AnimationCurve, etc...
	public class ReorderableListLayouter
	{
		protected UnityEditorInternal.ReorderableList native;

		#region Serialized Property

		protected SerializedProperty listProperty;

		protected SerializedProperty GetElementPropertyAt(int index)
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

		#region Constructor

		/// <summary>
		/// Constructor for easy setting up.
		/// </summary>
		/// <param name="listProperty"></param>
		public ReorderableListLayouter(SerializedProperty listProperty)
		{
			if (listProperty == null)
				return;

			this.listProperty = listProperty;

			native = new UnityEditorInternal.ReorderableList(
				listProperty.serializedObject, 
				listProperty,
				draggable: true, 
				displayHeader: true,
				displayAddButton: true,
				displayRemoveButton: true
			);

			// default layouts
			AddDrawHeader();
			AddDrawElementProperty();
			AddDrawElementBackground();
		}

		/// <summary>
		/// Constructor for customizable developper.
		/// </summary>
		/// <param name="listProperty"></param>
		/// <param name="draggable"></param>
		/// <param name="displayHeader"></param>
		/// <param name="displayAddButton"></param>
		/// <param name="displayRemoveButton"></param>
		public ReorderableListLayouter(
			SerializedProperty listProperty,
			bool draggable = true, bool displayHeader = true, bool displayAddButton = true, bool displayRemoveButton = true)
		{
			if (listProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.listProperty = listProperty;

			native = new UnityEditorInternal.ReorderableList(
				listProperty.serializedObject,
				listProperty,
				draggable,
				displayHeader,
				displayAddButton, 
				displayRemoveButton
			);
		}

		#endregion

		#region Layout

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

		#endregion

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

			native.drawElementCallback += DrawProperty;
			native.elementHeightCallback += ElementHeight;
		}

		protected virtual void DrawProperty(Rect rect, int index, bool isActive, bool isFocused)
			=> DrawProperty(GetElementPropertyAt(index), index, rect);

		protected virtual void DrawProperty(SerializedProperty property, int elementIndex, Rect rect)
		{
			if (property == null)
				return;

			if (property.IsMultiProperty())
			{
				// avoid grip marker
				rect.x += EditorLayoutUtility.gripWidth;
				rect.width -= EditorLayoutUtility.gripWidth;
			}

			// adjust center position
			rect.y += EditorLayoutUtility.singleHeightMargin * 2f;

			EditorGUI.PropertyField(rect, property, true);
		}

		protected virtual float ElementHeight(int index)
			=> GetElementPropertyAt(index).Height();

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

		protected virtual void DrawElementBackgroundAlternatively(Rect rect, int index, bool isActive, bool isFocused)
		{
			// current selected
			if (isFocused)
			{
				rect.DrawElementColor(EditorColorUtility.EffectiveActiveColor);
				return;
			}

			// odd element
			if (index % 2 != 0)
			{
				return;
			}

			rect.DrawElementColor(EditorColorUtility.EffectiveBackgroundColor);
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

		protected virtual void DrawDropDown(Rect rect, string[] canditateNames, System.Action<string> OnSelected)
		{
			var menu = new GenericMenu();

			foreach (var name in canditateNames)
				menu.AddItem(new GUIContent(name), false, () => OnSelected?.Invoke(name));

			menu.DropDown(rect);
		}

		#endregion

		
	}
}
