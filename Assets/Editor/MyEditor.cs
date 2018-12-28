using UnityEngine;
using UnityEditor;
using System.Collections;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
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

}
