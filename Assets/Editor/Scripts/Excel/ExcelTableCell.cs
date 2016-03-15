using UnityEngine;
using System.Collections;

public enum ExcelTableCellType
{
    None = 0,
    TextField = 1,
    Label = 2,
}

public class ExcelTableCell {
    public int RowIndex;
    public int ColumnIndex;
    public object Value;

    public float width = 50f;

    public ExcelTableCellType Type = ExcelTableCellType.TextField;

    public ExcelTableCell(int row, int column, object value)
    {
        RowIndex = row;
        ColumnIndex = column;
        Value = value;
    }
}
