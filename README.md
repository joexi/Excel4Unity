# Excel4Unity
Excel for Unity


## Provides

* Excel(.xls/.xlsx) Read/Write
* Format Excel sheet into custom object (Excel,ExcelTable...)
* Edit .xls/.xlsx file in Unity EditorWindow
* Generate .cs model according to .xls

## Todo
* .xls support in platform Windows
* xls to json convertion


## How to use
#### read & write
``` c#
string excelPath = Application.dataPath + "/Test/Test.xlsx";
string outputPath = Application.dataPath + "/Test/Test2.xlsx";
Excel xls = ExcelHelper.LoadExcel(excelPath);
xls.ShowLog();

xls.Tables[0].SetValue(1, 1, "???");
ExcelHelper.SaveExcel(xls, outputPath);
```
#### generate .cs file
``` c#
string path = Application.dataPath + "/Test/Test4.xlsx";
Excel xls =  ExcelHelper.LoadExcel(path);
ExcelDeserializer ed = new ExcelDeserializer();
ed.FieldNameLine = 1;
ed.FieldTypeLine = 2;
ed.FieldValueLine = 3;
ed.IgnoreSymbol = "#";
ed.ModelPath = Application.dataPath + "/Editor/Excel4Unity/DataItem.txt";
ed.GenerateCS(xls.Tables[0]);
```
## Dependency

* EEPlus
* .Net 2.0 instead of .Net 2.0 Subset if you want to do excel job runtime

## Version

Unity 4.x or higher

