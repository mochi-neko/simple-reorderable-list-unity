using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.SimpleReorderableList.Samples
{
	public class NestedMultiPropertySample : MonoBehaviour
	{
		[SerializeField]
		private List<SampleElement> list = new List<SampleElement> { };

		[SerializeField]
		private string text;

		[System.Serializable]
		private class SampleElement
		{
			[SerializeField]
			private Rect rect = new Rect();
			[SerializeField]
			private Bounds bounds = new Bounds();
			[SerializeField]
			private Quaternion rotation = Quaternion.identity;
			[SerializeField]
			private Vector4 vector = new Vector4();
			[SerializeField]
			private string[] labels = new string[0];
		}
	}

}
