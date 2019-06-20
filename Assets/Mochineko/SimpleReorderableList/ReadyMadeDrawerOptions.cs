using System;

namespace Mochineko.SimpleReorderableList
{
	/// <summary>
	/// Holds options which judge to use ready made drawers. 
	/// </summary>
	public struct ReadyMadeDrawerOptions : IEquatable<ReadyMadeDrawerOptions>
	{
		/// <summary>
		/// Uses the ready made header drawer or not.
		/// </summary>
		public bool UseReadyMadeHeader { get; }
		/// <summary>
		/// Uses the ready made element drawer or not.
		/// </summary>
		public bool UseReadyMadeElement { get; }
		/// <summary>
		/// Uses the ready made element background drawer or not.
		/// </summary>
		public bool UseReadyMadeBackground { get; }

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
			=> new ReadyMadeDrawerOptions(
				useReadyMadeHeader: true,
				useReadyMadeElement: true,
				useReadyMadeBackground: true
			);


		bool IEquatable<ReadyMadeDrawerOptions>.Equals(ReadyMadeDrawerOptions other)
		{
			if (this.UseReadyMadeHeader != other.UseReadyMadeHeader)
				return false;
			if (this.UseReadyMadeElement != other.UseReadyMadeElement)
				return false;
			if (this.UseReadyMadeBackground != other.UseReadyMadeBackground)
				return false;

			return true;
		}

		public override int GetHashCode()
		{
			var hashCode = -360803941;

			hashCode = hashCode * -1521134295 + UseReadyMadeHeader.GetHashCode();
			hashCode = hashCode * -1521134295 + UseReadyMadeElement.GetHashCode();
			hashCode = hashCode * -1521134295 + UseReadyMadeBackground.GetHashCode();

			return hashCode;
		}
	}
}
