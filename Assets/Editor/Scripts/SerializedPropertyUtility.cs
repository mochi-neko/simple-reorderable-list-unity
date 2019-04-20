using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Mochineko.ReorderableList
{
	// Todo : Refactor and add summaries

	internal static class SerializedPropertyUtility
	{

		public static bool IsNotChildOf(SerializedProperty property, int parentDepth)
			=> property.depth <= parentDepth;

		public static bool IsNotDirectChildOf(SerializedProperty property, int parentDepth)
			=> property.depth > parentDepth + 1;

		public static int CountElementIn(SerializedProperty property)
		{
			var parentDepth = property.depth;
			var copy = property.Copy();
			var count = 0;

			while (copy.NextVisible(true))
			{
				if (IsNotChildOf(copy, parentDepth))
					break;
				if (IsNotDirectChildOf(copy, parentDepth))
					continue;

				count++;
			}

			return count;
		}

		public static int CountActiveElementIn(SerializedProperty property)
		{
			var parentDepth = property.depth;
			var copy = property.Copy();
			var count = 1;

			if (IsSingleProperty(copy))
				return count;
			if (!copy.isExpanded)
				return count;

			// count direct children
			while (copy.NextVisible(true))
			{
				if (IsNotChildOf(copy, parentDepth))
					break;
				if (IsNotDirectChildOf(copy, parentDepth))
					continue;

				count += CountActiveElementIn(copy);
			}

			return count;
		}

		public static bool IsSingleProperty(SerializedProperty property)
			=> property.propertyType != SerializedPropertyType.Generic;
	}
}
