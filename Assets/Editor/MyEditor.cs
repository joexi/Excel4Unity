using UnityEngine;
using UnityEditor;
using System.Collections;
using OfficeOpenXml;
using System.IO;

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
}
