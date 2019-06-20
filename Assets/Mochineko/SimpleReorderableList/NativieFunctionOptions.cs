using System;

namespace Mochineko.SimpleReorderableList
{
	/// <summary>
	/// Holds options which judge to use native functions.
	/// </summary>
	public struct NativeFunctionOptions : IEquatable<NativeFunctionOptions>
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


		bool IEquatable<NativeFunctionOptions>.Equals(NativeFunctionOptions other)
		{
			if (this.Draggable != other.Draggable)
				return false;
			if (this.DisplayHeader != other.DisplayHeader)
				return false;
			if (this.DisplayAddButton != other.DisplayAddButton)
				return false;
			if (this.DisplayRemoveButton != other.DisplayRemoveButton)
				return false;

			return true;
		}

		public override int GetHashCode()
		{
			var hashCode = 1643622589;

			hashCode = hashCode * -1521134295 + Draggable.GetHashCode();
			hashCode = hashCode * -1521134295 + DisplayHeader.GetHashCode();
			hashCode = hashCode * -1521134295 + DisplayAddButton.GetHashCode();
			hashCode = hashCode * -1521134295 + DisplayRemoveButton.GetHashCode();

			return hashCode;
		}
	}
}

