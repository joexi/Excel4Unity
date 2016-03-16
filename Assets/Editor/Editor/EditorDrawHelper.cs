using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class EditorDrawHelper {
    
    public static void DrawTableTab(Excel xls, ref int selectIndex)
    {
        GUILayout.BeginHorizontal();
        {
            for (int i = 0; i < xls.Tables.Count; i++)
            {
                if (GUILayout.Toggle(selectIndex == i, xls.Tables[i].TableName, EditorStyles.toolbarButton))
                {
                    selectIndex = i;
                }
            }
        }
        GUILayout.EndHorizontal();
    }

    public static void DrawTable(ExcelTable table)
    {
        if (table != null)
        {
            table.Position = EditorGUILayout.BeginScrollView(table.Position);
            for (int i = 1; i <= table.NumberOfRows; i++)
            {
                bool continued = false;

                if (continued)
                {
                    continue;
                }
                EditorGUILayout.BeginHorizontal();

                for (int j = 1; j <= table.NumberOfColumns; j++)
                {
                    DrawCell(table, i, j);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }
    }

    public static void DrawCell(ExcelTable table, int row, int column)
    {
        ExcelTableCell cell = table.GetCell(row, column);
        if (cell != null)
        {
            switch (cell.Type)
            {
                case ExcelTableCellType.TextField:
                    {
                        string s = EditorGUILayout.TextField(cell.Value.ToString(), GUILayout.MaxWidth(cell.width));
                        cell.Value = s;
                        break;
                    }
                case ExcelTableCellType.Label:
                    {
                        EditorGUILayout.LabelField(cell.Value.ToString(), GUILayout.MaxWidth(cell.width));
                        break;
                    }
                case ExcelTableCellType.Popup:
                    {
                        int selectIndex = cell.ValueSelected.IndexOf(cell.Value);
                        if (selectIndex < 0)
                        {
                            selectIndex = 0;
                        }
                        selectIndex = EditorGUILayout.Popup(selectIndex, cell.ValueSelected.ToArray(), GUILayout.MaxWidth(cell.width));
                        cell.Value = cell.ValueSelected[selectIndex];
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        else
        {
            string s = EditorGUILayout.TextField(table.GetValue(row, column).ToString());
            table.SetValue(row, column, s);
        }
       
    }

    public static void DrawButton(string title, Action onClick)
    {
        DrawButtonHorizontal(title, onClick);
    }

    public static void DrawButtonHorizontal(string title, Action onClick, bool horizontal = true)
    {
        if (horizontal)
        {
            EditorGUILayout.BeginHorizontal();
        }
        if (GUILayout.Button(title))
        {
            if (onClick != null)
            {
                onClick();
            }
        }
        if (horizontal)
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    public static void DoHorizontal(Action cb)
    {
        EditorGUILayout.BeginHorizontal();
        if (cb != null)
        {
            cb();
        }
        EditorGUILayout.EndHorizontal();
    }
}
