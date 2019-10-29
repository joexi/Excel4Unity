using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
//using LitJson;
using System.Collections.Generic;
using System.Text;
using System.Xml;

public class ExcelDeserializer {
    public int FieldNameLine;
    public int FieldTypeLine;
    public int FieldValueLine;

    public string IgnoreSymbol = string.Empty;
    public string ModelPath = Application.dataPath + "/Editor/Excel4Unity/DataItem.txt";
    public bool GenerateCS(ExcelTable table)
	{
        string moudle = File.ReadAllText(ModelPath);

		string properties = "";
		string parse = "";
		int tableColumn = 0;
		try
		{
			for (int j = 1; j <= table.NumberOfColumns; j++)
			{
				tableColumn = j;
                string propName = table.GetValue(FieldNameLine, j).ToString();
                string propType = table.GetValue(FieldTypeLine, j).ToString().ToLower();
                if (!string.IsNullOrEmpty(IgnoreSymbol) && propName.StartsWith(IgnoreSymbol))
				{
					continue;
				}
				if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(propType))
				{
					continue;
				}
				if (properties.Length == 0)
				{
					properties += string.Format("\tpublic {0} {1};\n", propType, propName);
					if (propType.Equals("string"))
					{
						properties += "\tpublic override string StringIdentity(){ return " + propName + "; }\n";
					}
					else
					{
						properties += "\tpublic override int Identity(){ return " + propName + "; }\n";
					}
				}
				else
				{
					properties += string.Format("\tpublic {0} {1};\n", propType, propName);
				}

				if (propType == "string")
				{
					parse += string.Format("\t\t{0} = data[\"{1}\"].ToString();\n", propName, propName);
				}
				else if (propType == "bool")
				{
					parse += string.Format("\t\t{0} = data[\"{1}\"].ToString() != \"0\";\n", propName, propName);
				}
				else if (propType == "int" || propType == "float" || propType == "double")
				{
					parse += string.Format("\t\t{0} = {1}.Parse(data[\"{2}\"].ToString());\n", propName, propType, propName);
				}
				else if (propType == "string[]")
				{
					string subType = propType.Replace("[]", "");
					parse += string.Format("\t\tstring {0}_str = data[\"{1}\"].ToString();\n", propName, propName);
					parse += "\t\tif(" + propName + "_str.Length > 0) { \n";
					parse += string.Format("\t\t {0} = data[\"{1}\"].ToString().Split (';');\n", propName, propName);
					string elseStr = string.Format("{0} = new {1}[0];", propName, subType);
					parse += "\t\t} else {" + elseStr + "}\n";
				}
				else if (propType == "int[]" || propType == "float[]" || propType == "double[]")
				{
					string subType = propType.Replace("[]", "");
					parse += string.Format("\t\tstring {0}_str = data[\"{1}\"].ToString();\n", propName, propName);
					parse += "\t\tif(" + propName + "_str.Length > 0) { \n";
					parse += string.Format("\t\tstring[] {0}_data = data[\"{1}\"].ToString().Split (';');\n", propName, propName);
					parse += string.Format("\t\t{0} = new {1}[{2}_data.Length];\n", propName, subType, propName);
					parse += "\t\tfor (int i = 0; i < " + propName + "_data.Length; i++) { " + propName + "[i] = " + subType + ".Parse (" + propName + "_data [i]);}\n";
					string elseStr = string.Format("{0} = new {1}[0];", propName, subType);
					parse += "\t\t} else {" + elseStr + "}\n";
				}
				else
				{
                    Debug.LogError("generate .cs failed! " + propType + " not a valid type" + " " + "table:" + table.TableName);
					return false;
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError(e);
            Debug.LogError("generate .cs failed: " + table.TableName + "!" + " " + "table:" + table.TableName);
			return false;
		}
		moudle = moudle.Replace("{0}", table.TableName + "Item");
		moudle = moudle.Replace("{1}", properties);
		moudle = moudle.Replace("{2}", table.TableName + "Item");
		moudle = moudle.Replace("{3}", parse);
		string path = Application.dataPath + "/Scripts/Model/" + table.TableName + "Item.cs";
		string str = string.Empty;
		if (File.Exists(path))
		{
			str = File.ReadAllText(path);
		}
        string directory = Path.GetDirectoryName(path);
        Debug.LogError(directory);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
		if (str != moudle)
		{
			Debug.LogError("change " + table.TableName + ".cs");
			File.WriteAllText(path, moudle);
		}
		else
		{
			//			Debug.LogError ("no change " + table.TableName + ".cs");
		}
        AssetDatabase.Refresh();
		return true;
	}
}
