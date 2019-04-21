using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.ReorderableList.Samples
{
	public class AttributeSampleComponent : MonoBehaviour
	{
		[SerializeField]
		[ReorderableList]
		private List<SampleElement> sampleList = new List<SampleElement> { };
	}
}
