using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.ReorderableList.Samples
{
	public class NestedPropertySample : MonoBehaviour
	{
		[SerializeField]
		private List<SampleElement> list = new List<SampleElement> { };

		[System.Serializable]
		private class SampleElement
		{
			[SerializeField]
			private Rect rect = new Rect();
			[SerializeField]
			private Quaternion rotation = Quaternion.identity;
			[SerializeField]
			private string[] labels = new string[0];
		}
	}

}
