using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Supplies an easily usable reorderable list on editor.
	/// </summary>
	public class ReorderableListLayouter
	{
		/// <summary>
		/// Native reorderable list.
		/// </summary>
		public UnityEditorInternal.ReorderableList Native { get; protected set; }

		/// <summary>
		/// Uses fold out or not.
		/// </summary>
		protected bool UseFoldOut { get; set; } = true;

		#region Serialized Property

		/// <summary>
		/// <see cref="SerializedProperty"/> of the source list.
		/// </summary>
		protected SerializedProperty SourceProperty { get; set; }

		/// <summary>
		/// Gets <see cref="SerializedProperty"/> of the element at the index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		protected SerializedProperty ElementPropertyAt(int index)
			=> SourceProperty.GetArrayElementAtIndex(index);

		/// <summary>
		/// Gets display name of the source list.
		/// </summary>
		protected string DisplayName
			=> SourceProperty.displayName;

		/// <summary>
		/// The source property is folded out or not.
		/// </summary>
		protected bool IsFoldedOut
		{
			get
			{
				return SourceProperty.isExpanded;
			}
			set
			{
				SourceProperty.isExpanded = value;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for customizable developper.
		/// </summary>
		/// <param name="sourceProperty">The source list property</param>
		/// <param name="nativeOptions">Native options of reorderable list about functions.</param>
		/// <param name="drawerOptions">Drawer options which are ready made.</param>
		public ReorderableListLayouter(
			SerializedProperty sourceProperty,
			NativeFunctionOptions nativeOptions,
			ReadyMadeDrawerOptions drawerOptions,
			bool useFoldOut = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldOut = useFoldOut;

			InitializeNativeFunctions(sourceProperty, nativeOptions);

			InitializeReadyMadeDrawers(drawerOptions);
		}
		public ReorderableListLayouter(
			SerializedProperty sourceProperty,
			bool useFoldOut = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldOut = useFoldOut;

			InitializeNativeFunctions(sourceProperty);

			InitializeReadyMadeDrawers();
		}
		public ReorderableListLayouter(
			SerializedProperty sourceProperty,
			NativeFunctionOptions nativeOptions,
			bool useFoldOut = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldOut = useFoldOut;

			InitializeNativeFunctions(sourceProperty, nativeOptions);

			InitializeReadyMadeDrawers();
		}
		public ReorderableListLayouter(
			SerializedProperty sourceProperty,
			ReadyMadeDrawerOptions drawerOptions,
			bool useFoldOut = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldOut = useFoldOut;

			InitializeNativeFunctions(sourceProperty);

			InitializeReadyMadeDrawers(drawerOptions);
		}

		protected virtual void InitializeNativeFunctions(SerializedProperty listProperty)
		{
			InitializeNativeFunctions(listProperty, NativeFunctionOptions.Default);
		}
		protected virtual void InitializeNativeFunctions(SerializedProperty listProperty, NativeFunctionOptions options)
		{
			Native = new UnityEditorInternal.ReorderableList(
				listProperty.serializedObject,
				listProperty,
				options.Draggable,
				options.DisplayHeader,
				options.DisplayAddButton,
				options.DisplayRemoveButton
			);
		}

		protected virtual void InitializeReadyMadeDrawers()
			=> InitializeReadyMadeDrawers(ReadyMadeDrawerOptions.Default);
		protected virtual void InitializeReadyMadeDrawers(ReadyMadeDrawerOptions options)
		{
			if (options.UseDefaultHeader)
				AddDrawHeaderCallback();

			if (options.UseDefaultElement)
				AddDrawElementPropertyCallback();

			if (options.UseDefaultBackground)
				AddDrawElementBackgroundCallback();
		}

		#endregion

		#region Layout

		/// <summary>
		/// Layouts field of reorderable list.
		/// Please call this on inspector GUI updated.
		/// </summary>
		public virtual void Layout()
		{
			if (Native == null)
				return;

			if (!UseFoldOut)
				Native.DoLayoutList();
			else
				LayoutWithFoldOut();
		}

		/// <summary>
		/// Layouts field of reorderable list with foldout.
		/// </summary>
		protected virtual void LayoutWithFoldOut()
		{
			IsFoldedOut = EditorGUILayout.Foldout(IsFoldedOut, DisplayName);

			if (!IsFoldedOut)
				return;

			Native.DoLayoutList();
		}

		#endregion

		#region Header

		/// <summary>
		/// Adds drwa header callback by default label. 
		/// </summary>
		public virtual void AddDrawHeaderCallback()
		{
			if (Native == null)
				return;

			Native.drawHeaderCallback += DrawHeader;
		}
		/// <summary>
		/// Adds drwa header callback by specified label.
		/// </summary>
		/// <param name="label"></param>
		public virtual void AddDrawHeaderCallback(string label)
		{
			if (Native == null)
				return;

			Native.drawHeaderCallback += (rect) => DrawHeader(rect, label);
		}

		/// <summary>
		/// Drwas header by default label. 
		/// </summary>
		/// <param name="rect"></param>
		protected virtual void DrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, DisplayName);
		}
		/// <summary>
		/// Drwas header by specified label.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="label"></param>
		protected virtual void DrawHeader(Rect rect, string label)
		{
			EditorGUI.LabelField(rect, label);
		}

		#endregion

		#region Property

		/// <summary>
		/// Adds draw element property callback.
		/// </summary>
		public void AddDrawElementPropertyCallback()
		{
			if (Native == null)
				return;

			Native.drawElementCallback += DrawProperty;
			Native.elementHeightCallback += ElementHeight;
		}

		/// <summary>
		/// Draws a property at an index.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="index"></param>
		/// <param name="isActive"></param>
		/// <param name="isFocused"></param>
		protected virtual void DrawProperty(Rect rect, int index, bool isActive, bool isFocused)
			=> DrawProperty(rect, ElementPropertyAt(index));
		/// <summary>
		/// Draws the property in reorderable list by default <see cref="EditorGUI.PropertyField"/>. 
		/// </summary>
		/// <param name="property"></param>
		/// <param name="rect"></param>
		protected virtual void DrawProperty(Rect rect, SerializedProperty property)
		{
			if (property == null)
				return;

			if (property.IsMultiProperty())
			{
				// avoid grip marker
				rect.x += EditorLayoutUtility.gripMarkerWidth;
				rect.width -= EditorLayoutUtility.gripMarkerWidth;
			}

			// adjust center position
			rect.y += EditorLayoutUtility.singleHeightMargin * 2f;

			EditorGUI.PropertyField(rect, property, true);
		}

		/// <summary>
		/// Calculates height of an element at an index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		protected virtual float ElementHeight(int index)
			=> ElementPropertyAt(index).Height();

		#endregion

		#region Background

		/// <summary>
		/// Adds alternative draw element background callback.
		/// </summary>
		public void AddDrawElementBackgroundCallback()
		{
			if (Native == null)
				return;

			Native.drawElementBackgroundCallback +=
				(Rect rect, int index, bool isActive, bool isFocused)
					=> DrawElementBackgroundAlternatively(rect, index, isActive, isFocused);
		}

		/// <summary>
		/// Draws element background alternatively for readability. 
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="index"></param>
		/// <param name="isActive"></param>
		/// <param name="isFocused"></param>
		protected virtual void DrawElementBackgroundAlternatively(Rect rect, int index, bool isActive, bool isFocused)
		{
			// selected element
			if (isFocused)
			{
				DrawActiveColor(rect);
				return;
			}

			// odd element
			if (index % 2 != 0)
			{
				// draw default background color
				return;
			}

			DrawDifferentBackgroundColor(rect);
		}

		protected virtual void DrawActiveColor(Rect rect)
		{
			EditorColorUtility.DrawElementColor(rect, EditorColorUtility.ActiveColor);
		}

		protected virtual void DrawDifferentBackgroundColor(Rect rect)
		{
			EditorColorUtility.DrawElementColor(rect, EditorColorUtility.DifferentBackgroundColor);
		}

		#endregion

		#region Drop Down

		/// <summary>
		/// Adds drop down callback with canditate names and selected callback.
		/// </summary>
		/// <param name="canditateNames"></param>
		/// <param name="OnSelected"></param>
		public virtual void AddDrawDropDownCallback(string[] canditateNames, System.Action<string> OnSelected)
		{
			if (Native == null)
				return;

			Native.onAddDropdownCallback += (rect, list)
				=> DrawDropDown(rect, canditateNames, OnSelected);
		}

		/// <summary>
		/// Draw drop down menu.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="canditateNames"></param>
		/// <param name="OnSelected"></param>
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
