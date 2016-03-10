# Excel4Unity
Excel for Unity

### Update 2016-3-10
* move the scripts and dlls into "Editor" folder to make sure the game can be running in ".Net 2.0 Subset"
* fix the row number & column number counting bug
* add example script in MyEditor/test

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
* .Net 2.0 instead of .Net 2.0 Subset if you want to do excel job runtime
