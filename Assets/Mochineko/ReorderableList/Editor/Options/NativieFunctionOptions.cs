using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Holds options which judge to use native functions.
	/// </summary>
	public struct NativeFunctionOptions
	{
		/// <summary>
		/// Element can be dragged or not.
		/// </summary>
		public bool Draggable { get; private set; }
		/// <summary>
		/// Displays text in header or not.
		/// </summary>
		public bool DisplayHeader { get; private set; }
		/// <summary>
		/// Displays add (+) button or not.
		/// </summary>
		public bool DisplayAddButton { get; private set; }
		/// <summary>
		/// Displays remove (-) button or not.
		/// </summary>
		public bool DisplayRemoveButton { get; private set; }

		public NativeFunctionOptions(
			bool draggable,
			bool displayHeader = true,
			bool displayAddButton = true,
			bool displayRemoveButton = true)
		{
			Draggable = draggable;
			DisplayHeader = displayHeader;
			DisplayAddButton = displayAddButton;
			DisplayRemoveButton = displayRemoveButton;
		}

		/// <summary>
		/// Supplies defalut options.
		/// </summary>
		public static NativeFunctionOptions Default
			=> new NativeFunctionOptions
			{
				Draggable = true,
				DisplayHeader = true,
				DisplayAddButton = true,
				DisplayRemoveButton = true
			};
	}
}

