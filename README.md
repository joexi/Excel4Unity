# Excel4Unity
Excel for Unity

## Provides
* Excel(.xls/.xlsx) Read/Write
* Format Excel sheet into custom object (Excel,ExcelTable...)
## How to use
``` c#
        string excelPath = Application.dataPath + "/Test/Test.xlsx";
        string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        Excel xls = ExcelHelper.LoadExcel(excelPath);
        xls.ShowLog();

        xls.Tables[0].SetValue(1, 1, "???");
        ExcelHelper.SaveExcel(xls, outputPath);
```

## Dependency
* EEPlus
