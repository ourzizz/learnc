Sub OpenCloseArray()
    Dim MyFile As String
    Dim MySaveAs as string
    Dim Arr(100) As String
    Dim count As Integer
    MyFile = Dir("C:\Users\Administrator\Desktop\k考试中心\2018公务员考试\学校资料\原始数据jc\" & "*.xls")
    count = count + 1
    Arr(count) = MyFile
    Do While MyFile <> ""
        MyFile = Dir
        If MyFile = "" Then
            Exit Do
        End If
        count = count + 1
        Arr(count) = MyFile         '将文件的名字存在数组中
    Loop
    For i = 1 To count
        Workbooks.Open Filename:="C:\Users\Administrator\Desktop\k考试中心\2018公务员考试\学校资料\原始数据jc\" & Arr(i)  '循环打开Excel文件
        xx = ActiveWorkbook.Sheets(1).Range("a2:g300")
        ActiveWorkbook.Close savechanges = True     '关闭打开的文件
        
        'Workbooks.Open Filename:="C:\Users\Administrator\Desktop\k考试中心\2018公务员考试\学校资料\模板.xls"
        ActiveWorkbook.Sheets(1).Range("a2:g300") = xx
        ActiveWorkbook.SaveAs Filename:="C:\Users\Administrator\Desktop\k考试中心\2018公务员考试\学校资料\obj\" & Arr(i)
        ActiveWorkbook.Sheets(1).Range("a2:g300") = ""
    Next
End Sub

