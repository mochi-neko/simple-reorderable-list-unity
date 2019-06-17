using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.SimpleReorderableList.Samples
{
	public class DropDownSample : MonoBehaviour
	{
		[SerializeField]
		private List<Human> humans = new List<Human> { };
		public List<Human> Humans => humans;

		public static readonly string[] bloodTypeCanditates = new string[]
		{
			"A",
			"B",
			"AB",
			"O"
		};

		[System.Serializable]
		public class Human
		{
			[SerializeField]
			private string name;
			[SerializeField]
			[ReadOnlyField]
			private string bloodType;

			public Human(string bloodType)
			{
				this.bloodType = bloodType;
			}
		}

	}
}
