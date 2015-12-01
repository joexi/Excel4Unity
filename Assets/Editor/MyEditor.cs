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
        table.TableName = "1";
        xls.Tables.Add(table);

        ExcelHelper.SaveExcel(xls, "/Users/junfeixi/Desktop/test111.xlsx");
    }
}
