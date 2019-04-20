using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.ReorderableList.Samples
{
	[System.Serializable]
	public class SampleElement
	{
		[SerializeField]
		private string name = null;

		[SerializeField]
		private int index = 0;

		[SerializeField]
		private Vector3 vector = new Vector3();

		[SerializeField]
		private string[] strings = new string[0];
	}
}
