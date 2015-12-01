using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
        string excelPath = Application.dataPath + "/Test/Test.xlsx";
        string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        Excel xls = ExcelHelper.LoadExcel(excelPath);
        xls.ShowLog();

        xls.Tables[0].SetValue(1, 1, "???");
        ExcelHelper.SaveExcel(xls, outputPath);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
