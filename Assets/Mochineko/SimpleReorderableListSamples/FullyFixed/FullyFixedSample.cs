using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.SimpleReorderableList.Samples
{
	public class FullyFixedSample : MonoBehaviour
	{
		[SerializeField]
		[ReadOnlyField]
		private string[] texts = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E"
		};
	}
}
