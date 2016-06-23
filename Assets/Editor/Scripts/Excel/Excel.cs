using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml;

public class Excel
{
    public List <ExcelTable> Tables = new List<ExcelTable>();

    public Excel()
    {
		
    }

    public Excel(ExcelWorkbook wb)
    {
        for (int i = 1; i <= wb.Worksheets.Count; i++)
        {
            ExcelWorksheet sheet = wb.Worksheets[i];
            ExcelTable table = new ExcelTable(sheet);
            Tables.Add(table);
        }
    }

    public void ShowLog() {
        for (int i = 0; i < Tables.Count; i++)
        {
            Tables[i].ShowLog();
        }
    }

    public void AddTable(string name) {
        ExcelTable table = new ExcelTable();
        table.TableName = name;
        Tables.Add(table);
    }

}
