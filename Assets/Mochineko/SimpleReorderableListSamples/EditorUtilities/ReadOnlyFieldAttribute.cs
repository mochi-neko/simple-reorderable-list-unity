using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.SimpleReorderableList.Samples
{
	/// <summary>
	/// An attribute to disallow edit on the Editor.
	/// Its drawer is <see cref="Editor.ReadOnlyFieldDrawer"/>.
	/// </summary>
	public class ReadOnlyFieldAttribute : PropertyAttribute
	{

	}
}
