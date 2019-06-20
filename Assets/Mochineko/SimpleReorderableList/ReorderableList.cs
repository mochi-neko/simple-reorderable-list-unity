using UnityEngine;
using UnityEditor;

namespace Mochineko.SimpleReorderableList
{
	/// <summary>
	/// Supplies an easily usable reorderable list on editor.
	/// </summary>
	public class ReorderableList
	{

		#region Settings

		/// <summary>
		/// Native reorderable list.
		/// </summary>
		public UnityEditorInternal.ReorderableList Native { get; protected set; }

		/// <summary>
		/// Uses foldout or not.
		/// </summary>
		protected bool UseFoldout { get; set; } = true;

		#endregion

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

		#region Set Up

		/// <summary>
		/// Easy constructor. 
		/// </summary>
		/// <param name="sourceProperty"></param>
		/// <param name="useFoldout"></param>
		public ReorderableList(
			SerializedProperty sourceProperty,
			bool useFoldout = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldout = useFoldout;

			InitializeNative(sourceProperty);

			InitializeReadyMadeDrawers();
		}

		/// <summary>
		/// Constructor for customizable developpers.
		/// </summary>
		/// <param name="sourceProperty">The source list property</param>
		/// <param name="nativeOptions">Native options of reorderable list about functions.</param>
		/// <param name="drawerOptions">Drawer options which are ready made.</param>
		public ReorderableList(
			SerializedProperty sourceProperty,
			NativeFunctionOptions nativeOptions,
			ReadyMadeDrawerOptions drawerOptions,
			bool useFoldout = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldout = useFoldout;

			InitializeNative(sourceProperty, nativeOptions);

			InitializeReadyMadeDrawers(drawerOptions);
		}
		
		/// <summary>
		///  Constructor for customizable developpers.
		/// </summary>
		/// <param name="sourceProperty"></param>
		/// <param name="nativeOptions"></param>
		/// <param name="useFoldout"></param>
		public ReorderableList(
			SerializedProperty sourceProperty,
			NativeFunctionOptions nativeOptions,
			bool useFoldout = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldout = useFoldout;

			InitializeNative(sourceProperty, nativeOptions);

			InitializeReadyMadeDrawers();
		}
		/// <summary>
		///  Constructor for customizable developpers.
		/// </summary>
		/// <param name="sourceProperty"></param>
		/// <param name="drawerOptions"></param>
		/// <param name="useFoldout"></param>
		public ReorderableList(
			SerializedProperty sourceProperty,
			ReadyMadeDrawerOptions drawerOptions,
			bool useFoldout = true)
		{
			if (sourceProperty == null)
				throw new System.ArgumentNullException("listProperty");

			this.SourceProperty = sourceProperty;
			this.UseFoldout = useFoldout;

			InitializeNative(sourceProperty);

			InitializeReadyMadeDrawers(drawerOptions);
		}

		/// <summary>
		/// Initialize native reorderable list.
		/// </summary>
		/// <param name="listProperty"></param>
		protected virtual void InitializeNative(SerializedProperty listProperty)
		{
			InitializeNative(listProperty, NativeFunctionOptions.Default);
		}
		/// <summary>
		/// Initialize native reorderable list by <paramref name="options"/>.
		/// </summary>
		/// <param name="listProperty"></param>
		/// <param name="options"></param>
		protected virtual void InitializeNative(SerializedProperty listProperty, NativeFunctionOptions options)
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

		/// <summary>
		/// Initialize ready made drawers.
		/// </summary>
		public virtual void InitializeReadyMadeDrawers()
			=> InitializeReadyMadeDrawers(ReadyMadeDrawerOptions.Default);
		/// <summary>
		/// Initialize ready made drawers by <paramref name="options"/>.
		/// </summary>
		/// <param name="options"></param>
		public virtual void InitializeReadyMadeDrawers(ReadyMadeDrawerOptions options)
		{
			if (options.UseReadyMadeHeader)
				AddDrawHeaderCallback();

			if (options.UseReadyMadeElement)
				AddDrawElementPropertyCallback();

			if (options.UseReadyMadeBackground)
				AddDrawElementBackgroundCallback();
		}

		#endregion

		#region Layout

		/// <summary>
		/// Layouts field of reorderable list.
		/// Please call this at <see cref="Editor.OnInspectorGUI"/>.
		/// </summary>
		public virtual void Layout()
		{
			if (Native == null)
				return;

			if (!UseFoldout)
				Native.DoLayoutList();
			else
				LayoutWithFoldOut();
		}

		/// <summary>
		/// Layouts field of reorderable list with foldout.
		/// Please call this at <see cref="Editor.OnInspectorGUI"/>.
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
		/// Adds drwa header callback by <paramref name="label"/>.
		/// </summary>
		/// <param name="label"></param>
		public virtual void AddDrawHeaderCallback(string label)
		{
			if (Native == null)
				return;

			Native.drawHeaderCallback += (rect) => DrawHeader(rect, label);
		}

		/// <summary>
		/// Drwas header at <paramref name="rect"/> by default label. 
		/// </summary>
		/// <param name="rect"></param>
		protected virtual void DrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, DisplayName);
		}
		/// <summary>
		/// Drwas header at <paramref name="rect"/> by <paramref name="label"/>.
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
		/// Draws a property of <paramref name="index"/> at <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="index"></param>
		/// <param name="isActive"></param>
		/// <param name="isFocused"></param>
		protected virtual void DrawProperty(Rect rect, int index, bool isActive, bool isFocused)
			=> DrawProperty(rect, ElementPropertyAt(index));
		/// <summary>
		/// Draws <paramref name="property"/> at <paramref name="rect"/>. 
		/// </summary>
		/// <param name="property"></param>
		/// <param name="rect"></param>
		protected virtual void DrawProperty(Rect rect, SerializedProperty property)
		{
			if (property == null)
				return;

			EditorGUI.PropertyField(
				LayoutUtility.AdjustedRect(rect, property),
				property,
				true
			);
		}

		/// <summary>
		/// Calculates height of an element at <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		protected virtual float ElementHeight(int index)
			=> LayoutUtility.ElementHeight(ElementPropertyAt(index));

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
		/// Draws element background alternatively for readability at <paramref name="rect"/>. 
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

			// draw dirrerent background color
			DrawDifferentBackgroundColor(rect);
		}

		/// <summary>
		/// Draws active color background at <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		protected virtual void DrawActiveColor(Rect rect)
		{
			BackgroundUtility.DrawElementBackgroundColorActive(rect);
		}

		/// <summary>
		/// Draws active color background at <paramref name="rect"/>.
		/// </summary>
		/// <param name="rect"></param>
		protected virtual void DrawDifferentBackgroundColor(Rect rect)
		{
			BackgroundUtility.DrawElementBackgroundColorDifferent(rect);
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
		/// Draw drop down menu at <paramref name="rect"/> with <paramref name="canditateNames"/> and <paramref name="OnSelected"/> reaction.
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
