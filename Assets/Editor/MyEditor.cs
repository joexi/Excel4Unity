using UnityEngine;
using UnityEditor;
using System.Collections;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using LitJson;
using System.Text;

public class Excel4Unity : Editor
{

    [MenuItem("Excel4Unity/Test/ReadWrite")] 
    static void ReadWrite()
    {
        Excel xls = new Excel();
        ExcelTable table = new ExcelTable();
        table.TableName = "test";
        string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        xls.Tables.Add(table);
        xls.Tables[0].SetValue(1, 1, "1");
        xls.Tables[0].SetValue(1, 2, "2");
        xls.Tables[0].SetValue(2, 1, "3");
        xls.Tables[0].SetValue(2, 2, "4");
        xls.ShowLog();
        ExcelHelper.SaveExcel(xls, outputPath);
    }

    [MenuItem("Excel4Unity/Test/Read")] 
    static void Read()
    {
        string path = Application.dataPath + "/Test/Test3.xlsx";
        Excel xls =  ExcelHelper.LoadExcel(path);
        xls.ShowLog();
    }

    [MenuItem("Excel4Unity/Test/Write")] 
    static void Write()
    {
        Excel xls = new Excel();
        ExcelTable table = new ExcelTable();
        table.TableName = "test";
        string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        xls.Tables.Add(table);
        xls.Tables[0].SetValue(1, 1, Random.Range(1000,100000).ToString());
        xls.ShowLog();
        ExcelHelper.SaveExcel(xls, outputPath);
    }

    [MenuItem("Excel4Unity/Test/GenerateModel")] 
    static void GenerateModel()
    {
        string path = Application.dataPath + "/Test/Test4.xlsx";
        Excel xls =  ExcelHelper.LoadExcel(path);
        ExcelDeserializer ed = new ExcelDeserializer();
        ed.FieldNameLine = 1;
        ed.FieldTypeLine = 2;
        ed.FieldValueLine = 3;
        ed.IgnoreSymbol = "#";
        ed.ModelPath = Application.dataPath + "/Editor/Excel4Unity/DataItem.txt";
        ed.GenerateCS(xls.Tables[0]);
    }

    [MenuItem(@"Excel4Unity/Test/Excel2JSON")]
    static void Excel2JSON()
    {
        Object[] objs = Selection.objects;
        for (int i = 0; i < objs.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(objs[i]);
            if (path.EndsWith(".xlsx"))
            {
                ParseFile(path);
            }
            else
            {
                EditorUtility.DisplayDialog("提示", "暂不支持的文件格式" + path, "ok");
                return;
            }
        }
        AssetDatabase.Refresh();
    }

    public static string ParseFile(string path, bool createCS = true, bool isMac = false)
    {
        //		UnityEngine.Debug.LogError ("path " + path);
        if (!path.EndsWith("xlsx"))
        {
            return null;
        }

        string tableName = "";
        string currentPropName = "";
        int tableRow = 0;
        int tableColumn = 0;
        string v = "";
        Excel excel = null;
        excel = ExcelHelper.LoadExcel(path);
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();
            foreach (ExcelTable table in excel.Tables)
            {
                tableName = table.TableName;
                bool language = tableName.ToLower().Contains("language");
                if (table.TableName.StartsWith("#"))
                {
                    continue;
                }
                if (createCS)
                {
                    ExcelDeserializer ed = new ExcelDeserializer();
                    ed.FieldNameLine = 1;
                    ed.FieldTypeLine = 2;
                    ed.FieldValueLine = 3;
                    ed.IgnoreSymbol = "#";
                    ed.ModelPath = Application.dataPath + "/Editor/Excel4Unity/DataItem.txt";
                    ed.GenerateCS(table);
                }
                writer.WritePropertyName(table.TableName);
                writer.WriteArrayStart();
                for (int i = 4; i <= table.NumberOfRows; i++)
                {
                    tableRow = i;
                    string idStr = table.GetValue(i, 1).ToString();
                    if (idStr.Length <= 0)
                    {
                        //						UnityEngine.Debug.LogError ("ID error:" + tableName + "  (第" + i + "行)");
                        break;
                    }
                    writer.WriteObjectStart();

                    for (int j = 1; j <= table.NumberOfColumns; j++)
                    {
                        tableColumn = j;
                        string propName = table.GetValue(1, j).ToString();
                        string propType = table.GetValue(3, j).ToString();
                        propName = propName.Replace("*", "");
                        currentPropName = propName;

                        if (propName.StartsWith("#"))
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(propType))
                        {
                            continue;
                        }
                        writer.WritePropertyName(propName);
                        v = table.GetValue(i, j).ToString();
                        if (propType.Equals("int"))
                        {
                            int value = v.Length > 0 ? int.Parse(v) : 0;
                            writer.Write(value);
                        }
                        else if (propType.Equals("bool"))
                        {
                            int value = v.Length > 0 ? int.Parse(v) : 0;
                            writer.Write(value);
                        }
                        else if (propType.Equals("float"))
                        {
                            float value = v.Length > 0 ? float.Parse(v) : 0;
                            writer.Write(value);
                        }
                        else
                        {
                            string ss = table.GetValue(i, j).ToString();
                            if (language && ss.Contains(" "))
                            {
                                ss = ss.Replace(" ", "\u00A0");
                            }
                            writer.Write(ss);
                        }
                    }
                    writer.WriteObjectEnd();
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
            string outputDir = Application.dataPath + "/Resources/DataFiles/";
            string outputPath = outputDir + Path.GetFileNameWithoutExtension(path) + ".txt";
            if (!Directory.Exists(outputDir)) {
                Directory.CreateDirectory(outputDir);
            }
            string str = string.Empty;
            if (File.Exists(path))
            {
                byte[] bytes = File.ReadAllBytes(path);
                UTF8Encoding encoding = new UTF8Encoding();
                str = encoding.GetString(bytes);
            }
            string content = sb.ToString();
            if (str != content)
            {
                File.WriteAllText(outputPath, content);
            }
            Debug.Log("convert success! path = " + path);

            return sb.ToString();
        }
        catch (System.Exception e)
        {
            if (excel == null)
            {
                //                EditorUtility.DisplayDialog("ERROR!", "open excel failed!","ok"); 
                UnityEngine.Debug.LogError("open excel failed!");
            }
            else
            {
                string msg = "解析错误！ \n表:" + tableName + " \n字段:" + currentPropName + "  \n第" + tableRow + "行,第" + tableColumn + "列 \nvalue = " + v;
                EditorUtility.DisplayDialog("error!", msg, "ok");
                UnityEngine.Debug.LogError(e);
                UnityEngine.Debug.LogError(e.StackTrace);
                UnityEngine.Debug.LogError(msg);
            }
            UnityEngine.Debug.LogError(e.StackTrace.ToString());
            return null;
        }
    }
}
