using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.SimpleReorderableList.Samples
{
	public class MultiPropertySample : MonoBehaviour
	{
		[SerializeField]
		private List<SampleElement> list = new List<SampleElement> { };

		[System.Serializable]
		private class SampleElement
		{
			[SerializeField]
			private string label;
			[SerializeField]
			private bool activity;
			[SerializeField]
			private Vector3 position;
		}
	}

}
