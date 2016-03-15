using UnityEngine;
using UnityEditor;
using System.Collections;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
public class MyEditor : Editor
{

    [MenuItem("MyEditor/test")] 
    static void test()
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

    [MenuItem("MyEditor/LoadXls")] 
    static void LoadXls()
    {
        string path = Application.dataPath + "/Test/Test3.xlsx";
        Excel xls =  ExcelHelper.LoadExcel(path);
        xls.ShowLog();
    }

    [MenuItem("MyEditor/WriteXls")] 
    static void WriteXls()
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


}
