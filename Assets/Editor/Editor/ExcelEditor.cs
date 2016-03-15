using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExcelEditor : EditorWindow {

    private Excel mExcel;
    private ExcelTable mTable;

    [MenuItem("MyEditor/ShowXlsEditor")]
    static void ShowWindow()
    {
        ExcelEditor window = EditorWindow.GetWindowWithRect<ExcelEditor>(new Rect(0, 0, 600, 400));
        window.Show();

        string path = Application.dataPath + "/Test/Test3.xlsx";
        Excel xls =  ExcelHelper.LoadExcel(path);
        xls.ShowLog();

        window.Show(xls);
    }


    public void Show(Excel xls)
    {
        mExcel = xls;
        mTable = mExcel.Tables[0];
        mTable.SetCellTypeRow(1, ExcelTableCellType.Label);
        mTable.SetCellTypeRow(2, ExcelTableCellType.Label);
        mTable.SetCellTypeColumn(2, ExcelTableCellType.Label);
    }

    void OnGUI()
    {
        if (mExcel != null && mTable != null)
        {
            EditorDrawHelper.DrawTable(mTable);
        }
    }

    public void DrawButton()
    {
        EditorGUILayout.BeginHorizontal();
        EditorDrawHelper.DrawButton("Add", delegate()
        {
            mTable.NumberOfRows++;
        });

        EditorDrawHelper.DrawButton("Save", delegate()
        {
            string path = Application.dataPath + "/Test/Test3.xlsx";
            ExcelHelper.SaveExcel(mExcel, path);
        });
        
        EditorGUILayout.EndHorizontal();
    }


}
