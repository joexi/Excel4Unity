using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ExcelEditor : EditorWindow {

    private Excel mExcel;
    private ExcelTable mTable;
    private int selectIndex;

    [MenuItem("MyEditor/ShowXlsEditor")]
    static void ShowWindow()
    {
        ExcelEditor window = EditorWindow.GetWindowWithRect<ExcelEditor>(new Rect(0, 0, 800, 400));
        window.Show();

        string path = Application.dataPath + "/Test/Test3.xlsx";
        Excel xls =  ExcelHelper.LoadExcel(path);
        xls.ShowLog();

        window.Show(xls);
    }


    public void Show(Excel xls)
    {
        mExcel = xls;
        for (int i = 0; i < mExcel.Tables.Count; i++)
        {
            mExcel.Tables[i].SetCellTypeColumn(1, ExcelTableCellType.Label);
            mExcel.Tables[i].SetCellTypeColumn(3, ExcelTableCellType.Popup, new List<string>(){"1","2"});
            mExcel.Tables[i].SetCellTypeRow(1, ExcelTableCellType.Label);
            mExcel.Tables[i].SetCellTypeRow(2, ExcelTableCellType.Label);
        }
    }

    void OnGUI()
    {
        if (mExcel != null)
        {
            EditorDrawHelper.DrawTableTab(mExcel, ref selectIndex);
            mTable = mExcel.Tables[selectIndex];
            EditorDrawHelper.DrawTable(mTable);
            DrawButton();
        }
    }

    public void DrawButton()
    {
        EditorGUILayout.BeginHorizontal();
        EditorDrawHelper.DrawButton("Add", delegate()
        {
            mTable.NumberOfRows++;
            Show(mExcel);
        });

        EditorDrawHelper.DrawButton("Save", delegate()
        {
            string path = Application.dataPath + "/Test/Test3.xlsx";
            ExcelHelper.SaveExcel(mExcel, path);
            EditorUtility.DisplayDialog("Save Success", path, "ok");
        });
        EditorGUILayout.EndHorizontal();
    }
}
