using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using OfficeOpenXml;

public class ExcelTable
{
    private Dictionary <int, Dictionary<int, object>> cells = new Dictionary<int, Dictionary<int, object>>();

    public string TableName;
    public int NumberOfRows;
    public int NumberOfColumns;


    public ExcelTable()
    {

    }

    public ExcelTable(ExcelWorksheet sheet)
    {
        TableName = sheet.Name;
        NumberOfRows = sheet.Dimension.Rows;
        NumberOfColumns = sheet.Dimension.Columns;
        for (int row = 1; row <= NumberOfRows; row++)
        {
            for (int column = 1; column <= NumberOfColumns; column++)
            {
                object value = sheet.Cells[row, column].Value;
                SetValue(row, column, value);
            }
        }
    }

    public void SetValue(int row, int column, object value)
    {
        if (!cells.ContainsKey(row))
        {
            cells[row] = new Dictionary<int, object>();
        }
        cells[row][column] = value;
    }

    public object GetValue(int row, int column)
    {
        if (cells.ContainsKey(row))
        {
            Dictionary<int, object> rowDic = cells[row];
            if (rowDic.ContainsKey(column))
            {
                return rowDic[column];
            }
        }
        return null;
    }

    public void ShowLog() {
        string msg = "";
        for (int row = 1; row <= NumberOfRows; row++)
        {
            for (int column = 1; column <= NumberOfColumns; column++)
            {
                msg += string.Format("{0} ", GetValue(row, column));
            }
            msg += "\n";
        }
        Debug.Log(msg);
    }
}
