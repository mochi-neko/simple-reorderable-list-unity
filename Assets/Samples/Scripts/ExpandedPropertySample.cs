using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.ReorderableList.Samples
{
	public class ExpandedPropertySample : MonoBehaviour
	{
		[SerializeField]
		private List<SampleElement> list = new List<SampleElement> { };

		[System.Serializable]
		private class SampleElement
		{
			[SerializeField]
			private string label;
			[SerializeField]
			private Rect rect;
			[SerializeField]
			private Bounds bounds;
		}
	}

}
