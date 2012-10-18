
'find reader file
'dir = Evoware.GetStringVariable("Dir")

dir = "."

Set fso = CreateObject("Scripting.FileSystemObject")
  Set folder = fso.GetFolder(dir)
  Set files = folder.Files


dMostRecent = 0
sMostRecent = ""

For Each oFile In fso.GetFolder(dir).Files
	dFileDate = oFile.DateLastModified
	ext = LCase(Right(oFile.Path,3)) 
	'AND ext="xlsx"
	'msgbox(ext )
	If dFileDate > dMostRecent AND ext="xml" Then
		dMostRecent = dFileDate
		FullName= oFile.Path
'		msgbox(FullName)
	End If
Next


 'msgbox(FullName)

Set xmlDoc = CreateObject("Microsoft.XMLDOM")
xmlDoc.Async = "False"

'read XML
'ReaderFile = "Read2012-09-20 23-16-49.xml"
ReaderFile = FullName
xmlDoc.Load(ReaderFile)

strQuery = "/MeasurementResultData/Section/Data"
Set Datas = xmlDoc.selectNodes(strQuery)

Dim wellsOD() 
ReDim wellsOD(Datas.item(0).getElementsByTagName("Well").length)
For Each Data in Datas 
	'MsgBox Data.nodeName & ": " & Data.text
	Set Wells = Data.getElementsByTagName("Well")
	For w = 1 To Wells.length
            OD = CDbl(Wells.item(w-1).getElementsByTagName("Single").item(0).FirstChild.NodeValue)
            wellsOD(w) = OD +  wellsOD(w) 
    		'MsgBox  wellsOD(w)
	Next
Next

for w=1 to UBound(wellsOD)
    wellsOD(w) = wellsOD(w)/Datas.length
    'MsgBox  wellsOD(w)
Next

'calc NetOD
'TaraWells = Evoware.GetStringVariable("TaraWells")
TaraWells = "6,7,8"
TaraWellsStrArr =  Split(TaraWells, ",")

Dim TaraWellsArr()
ReDim TaraWellsArr(UBound(TaraWellsStrArr))
for i=0 to UBound(TaraWellsStrArr)
    TaraWellsArr(i) = CInt(TaraWellsStrArr(i))
    TotTaraWellsOD = TotTaraWellsOD+wellsOD(TaraWellsArr(i))
Next

MeanTaraWellsOD = TotTaraWellsOD/(UBound(TaraWellsStrArr)+1)


'DilutWells= Evoware.GetStringVariable("DilutWells")
DilutWells = "1,2,3"
DilutWellsStrArr =  Split(DilutWells, ",")

ReDim DilutWellsArr(UBound(DilutWellsStrArr))
ReDim NeDilutWellsOD(UBound(DilutWellsStrArr))
for i=0 to UBound(DilutWellsArr)
    DilutWellsArr(i) = CInt(DilutWellsStrArr(i))
    NeDilutWellsOD(i) = wellsOD(DilutWellsArr(i)) - MeanTaraWellsOD
    'MsgBox NeDilutWellsOD(i)
Next

'Calc vol to dilut 
wellvol = 200.0
dilution = 100.0

ReDim DilutVolume(UBound(NeDilutWellsOD))
for i=0 to UBound(DilutVolume)
      DilutVolume(i) = wellvol*0.4395/dilution/NeDilutWellsOD(i)
Next

'write to file
Set fso = CreateObject("Scripting.FileSystemObject")
Set DilutionsList = fso.CreateTextFile("DilutionsList.gwl", True)

LBRes=";LBRes;;Trough 100ml;"
Plate96 = ";Plate96;;96WellMicroplate;"

'fill LB
for i=0 to UBound(DilutVolume)
      LineString  = "A" + LBRes + CStr(1) + ";;" + Cstr(wellvol- DilutVolume(i)) + ";;;"
      DilutionsList.WriteLine LineString
      LineString  = "D" + Plate96 + CStr(DilutWellsArr(i)+8) + ";;" + Cstr(wellvol- DilutVolume(i)) + ";;;"
      DilutionsList.WriteLine LineString
Next
LineString = "W;"
DilutionsList.WriteLine LineString

'Dilut LB
for i=0 to UBound(DilutVolume)
      LineString  = "A" + Plate96 + CStr(DilutWellsArr(i)) + ";;" + Cstr(DilutVolume(i)) + ";;;"
      DilutionsList.WriteLine LineString
      LineString  = "D" + Plate96 + CStr(DilutWellsArr(i)+8) + ";;" + Cstr(DilutVolume(i)) + ";;;"
      DilutionsList.WriteLine LineString
      LineString = "W;"
        DilutionsList.WriteLine LineString
Next
