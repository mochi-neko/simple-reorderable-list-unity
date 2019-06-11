using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using System.Text;

namespace Mochineko.ReorderableList.ScriptCreator
{
	public class ScriptCreator : EndNameEditAction
	{
		public string SourceName { private get; set; } = "";
		public string[] FieldNames { private get; set; } = new string[0];

		private const string scriptNameTemplate = "#SCRIPTNAME#";
		private const string sourceNameTemplate = "#SOURCE_NAME#";
		private const string fieldsTemplate = "#FIELDS#";
		private const string onEnableTemplate = "#ON_ENABLE#";
		private const string onInspectorGUITemplate = "#ON_INSPECTOR_GUI#";

		private const string newLine = "\n";
		private const string inClassAlignement = "\t\t";
		private const string inMethodAlignment = "\t\t\t";
		private const string inMethodNestedAlignment = "\t\t\t\t";

		public override void Action(int instanceId, string pathName, string resourceFile)
		{
			var source = File.ReadAllText(resourceFile);
			var fileName = Path.GetFileNameWithoutExtension(pathName);

			// remove space
			fileName = fileName.Replace(" ", "");


			source = source.Replace(scriptNameTemplate, fileName);

			source = source.Replace(sourceNameTemplate, SourceName);

			source = source.Replace(fieldsTemplate, CreateFields(FieldNames));

			source = source.Replace(onEnableTemplate, CreateInitializations(FieldNames));

			source = source.Replace(onInspectorGUITemplate, CreateLayouts(FieldNames));

			var newPath = pathName.Replace(fileName, fileName);

			File.WriteAllText(newPath, source, new UTF8Encoding(true, false));

			AssetDatabase.ImportAsset(newPath);

			ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath<MonoScript>(newPath));
		}

		private string CreateField(string fieldName)
			=> $"private Mochineko.ReorderableList.ReorderableListLayouter {fieldName}Layouter;";

		private string CreateInitialization(string fieldName)
			=> $"{fieldName}Layouter = new Mochineko.ReorderableList.ReorderableListLayouter(serializedObject.FindProperty(\"{fieldName}\"));";

		private string CreateLayout(string fieldName)
			=> $"if ({fieldName}Layouter != null)\n\t\t\t\t\t{fieldName}Layouter.Layout();";


		private string CreateFields(string[] fieldNames)
		{
			var builder = new StringBuilder();

			foreach (var fieldName in fieldNames)
			{
				builder.Append(CreateField(fieldName));
				builder.Append(newLine);
				builder.Append(inClassAlignement);
			}

			return builder.ToString();
		}

		private string CreateInitializations(string[] fieldNames)
		{
			var builder = new StringBuilder();

			foreach (var fieldName in fieldNames)
			{
				builder.Append(CreateInitialization(fieldName));
				builder.Append(newLine);
				builder.Append(inMethodAlignment);
			}

			return builder.ToString();
		}

		private string CreateLayouts(string[] fieldNames)
		{
			var builder = new StringBuilder();

			foreach (var fieldName in fieldNames)
			{
				builder.Append(CreateLayout(fieldName));
				builder.Append(newLine);
				builder.Append(inMethodNestedAlignment);
			}

			return builder.ToString();
		}
	}
}