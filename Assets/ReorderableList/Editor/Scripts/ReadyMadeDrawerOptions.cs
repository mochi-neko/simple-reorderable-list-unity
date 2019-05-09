using System.Collections;
using System.Collections.Generic;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Holds options which judge to use ready made drawers. 
	/// </summary>
	public struct ReadyMadeDrawerOptions
	{
		public bool UseDefaultHeader { get; private set; }
		public bool UseDefaultElement { get; private set; }
		public bool UseDefaultBackground { get; private set; }

		public ReadyMadeDrawerOptions(
			bool useDefaultHeader,
			bool useDefaultElement,
			bool useDefaultBackground)
		{
			UseDefaultHeader = useDefaultHeader;
			UseDefaultElement = useDefaultElement;
			UseDefaultBackground = useDefaultBackground;
		}

		/// <summary>
		/// Supplies default options.
		/// </summary>
		public static ReadyMadeDrawerOptions Default
			=> new ReadyMadeDrawerOptions
			{
				UseDefaultHeader = true,
				UseDefaultElement = true,
				UseDefaultBackground = true
			};
	}
}
