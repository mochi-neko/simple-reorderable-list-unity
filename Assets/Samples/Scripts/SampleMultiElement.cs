using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.ReorderableList.Samples
{
	[System.Serializable]
	public class SampleMultiElement
	{
		[SerializeField]
		private string name = null;

		[SerializeField]
		private Rect rect;

		[SerializeField]
		private Bounds bounds;

		[SerializeField]
		private string text;

		[SerializeField]
		private List<SampleMultiElement> samples = new List<SampleMultiElement> { };
	}
}
