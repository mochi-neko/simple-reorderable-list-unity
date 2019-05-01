using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.ReorderableList.Samples
{
	public class SampleComponent : MonoBehaviour
	{
		[SerializeField]
		private List<string> singleSampleList = new List<string> { };

		[SerializeField]
		private List<SampleElement> sampleList = new List<SampleElement> { };

		[SerializeField]
		private SampleMultiElement[] multiSampleList = new SampleMultiElement[5];
	}
}
