using System.Collections;
using System.Collections.Generic;

namespace Mochineko.ReorderableList
{
	/// <summary>
	/// Holds options which judge to use ready made drawers. 
	/// </summary>
	public struct ReadyMadeDrawerOptions
	{
		/// <summary>
		/// Uses the ready made header drawer or not.
		/// </summary>
		public bool UseReadyMadeHeader { get; private set; }
		/// <summary>
		/// Uses the ready made element drawer or not.
		/// </summary>
		public bool UseReadyMadeElement { get; private set; }
		/// <summary>
		/// Uses the ready made element background drawer or not.
		/// </summary>
		public bool UseReadyMadeBackground { get; private set; }

		public ReadyMadeDrawerOptions(
			bool useReadyMadeHeader,
			bool useReadyMadeElement = true,
			bool useReadyMadeBackground = true)
		{
			UseReadyMadeHeader = useReadyMadeHeader;
			UseReadyMadeElement = useReadyMadeElement;
			UseReadyMadeBackground = useReadyMadeBackground;
		}

		/// <summary>
		/// Supplies default options.
		/// </summary>
		public static ReadyMadeDrawerOptions Default
			=> new ReadyMadeDrawerOptions
			{
				UseReadyMadeHeader = true,
				UseReadyMadeElement = true,
				UseReadyMadeBackground = true
			};
	}
}
