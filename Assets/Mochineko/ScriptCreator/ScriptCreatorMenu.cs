using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Mochineko.ReorderableList.ScriptCreator
{
	public class ScriptCreatorMenu
	{
		#region Templates

		protected const string templateFolderRelativePath = "Mochineko/ScriptCreator/Templates/";
		protected const string defaultMenuPath = "Assets/Create/ReorderableList/";
		protected const string csIconName = "cs Script Icon";

		protected static string TemplateFolderPath
			=> Path.Combine(
				Application.dataPath,
				templateFolderRelativePath
				);

		protected static Texture2D CsIcon
			=> EditorGUIUtility.IconContent(csIconName).image as Texture2D;

		protected static ScriptCreator GetCreator
			=> ScriptableObject.CreateInstance<ScriptCreator>();

		#endregion

		private const string templateFileName = "84-C# Script-NewEditorScript.cs.txt";
		private const string defaultScriptName = "NewEditorScript.cs";
		private const string displayMenuName = "Reorderable List Editor Script";

		//[MenuItem(defaultMenuPath + displayMenuName)]
		public static void CreateCsScript(string sourceName, string[] fieldNames)
		{
			var creator = GetCreator;
			creator.SourceName = sourceName;
			creator.FieldNames = fieldNames;

			ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
				0,
				creator,
				defaultScriptName,
				CsIcon,
				TemplateFolderPath + templateFileName
			);
		}
	}
}
