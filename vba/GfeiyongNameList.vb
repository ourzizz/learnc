Function fill_table(sheetx As Worksheet)
    '将数据库中的select name from kdry where dwname=dwname,写入当前sheet
    Dim money%, qurey$, i%, row%, clum%, j%
    Dim namelist(500) As String
    Dim dwname$
    dwname = sheetx.Name
    i = 1
    row = 4
    clum = 0
    'money=calc_money
    Set mycon = CreateObject("ADODB.Connection")
    Set dwrs = CreateObject("ADODB.recordset")
    mycon.Open "Driver={MySQL ODBC 5.3 UNICODE Driver};Server=192.168.1.107;Database=kddb;User=root;Password=123123;Option=3;"
    qurey = "select ryname from kdry where dwname=" & "'" & dwname & "'"
    dwrs.Open qurey, mycon
    While dwrs.EOF <> True:
        namelist(i) = dwrs.fields(0)
        i = i + 1
        dwrs.movenext
    Wend

    For j = 1 To i - 1
        If j = (i \ 2) + 1 Then
            row = 4
            clum = 5
        End If
        sheetx.Cells(row, 1 + clum) = j
        sheetx.Cells(row, 2 + clum) = namelist(j)
        sheetx.Cells(row, 3 + clum) = 400
        row = row + 1
    Next
    fill_table = (i \ 2) + 4
End Function


Sub fill_dwarray(DwArray() As String) 'select dwname from danwei
    Dim i%
    i = 0
    Set mycon = CreateObject("ADODB.Connection")
    Set dwrs = CreateObject("ADODB.recordset")
    mycon.Open "Driver={MySQL ODBC 5.3 UNICODE Driver};Server=192.168.1.107;Database=kddb;User=root;Password=123123;Option=3;"
    dwrs.Open "select * from danwei", mycon
    While dwrs.EOF <> True:
        DwArray(i) = dwrs.fields(0)
        i = i + 1
        dwrs.movenext
    Wend
End Sub

Sub format_table(sheetx As Excel.Worksheet, row As Integer)
    '将已经填写好名单的sheet格式化'
    sheetx.Cells(1, 1) = "2017年毕节市招考公务员笔试考务费发放名册" & "(" & sheetx.Name & ")"
    sheetx.Cells(2, 1) = "填报单位市人社局"
    sheetx.Cells(2, 4) = "填报日期" & Format(Now, "yyyy/m/d")
    sheetx.Cells(3, 1) = "序号"
    sheetx.Cells(3, 2) = "姓名"
    sheetx.Cells(3, 3) = "金额"
    sheetx.Cells(3, 4) = "签名"
    sheetx.Cells(3, 6) = "序号"
    sheetx.Cells(3, 7) = "姓名"
    sheetx.Cells(3, 8) = "金额"
    sheetx.Cells(3, 9) = "签名"
    sheetx.Cells(row, 1) = "合计:"
    sheetx.Range("A1:I1").Merge
    sheetx.Rows(1).RowHeight = 25
    sheetx.Range("A2:B2").Merge
    sheetx.Range("E3:E" & (row - 1)).Merge
    'sheetx.range("A" & row & ":I" & row).merge
    sheetx.Range("A1:I" & (row)).Borders.LineStyle = xlContinuous
    sheetx.Range("A1:I" & (row)).HorizontalAlignment = Excel.xlCenter
End Sub
Sub create_menu()
    Sheets("首页").Select
    Cells(1, 2) = "人数"
    Dim j%
    For i = 1 To Sheets.Count
        Cells(i, 1) = Sheets(i).Name
        j = 5
        While Sheets(i).Cells(j, 6) <> ""
            j = j + 1
        Wend
        Cells(i, 2) = Sheets(i).Cells(j - 1, 6)
    Next
    For i = 1 To Sheets.Count
        t = Cells(i, 1)
        Cells(i, 1).Select
        ActiveSheet.Hyperlinks.Add Anchor:=Selection, Address:="", SubAddress:=t & "!A1", ScreenTip:="进入", TextToDisplay:=t
    Next
End Sub

Sub delete_sht()
    Application.DisplayAlerts = False
    While Sheets.Count <> 1
        Sheets(2).Delete
    Wend
End Sub

Sub main()
    Dim DwArray(200) As String
    Dim row%
    Call fill_dwarray(DwArray)
    Call delete_sht
    i = 1
    For Each dwname In DwArray
        If dwname <> "" Then
            Sheets.Add(after:=Sheets(Sheets.Count)).Name = dwname
            row = fill_table(Sheets(dwname))
            Call format_table(Sheets(dwname), row)
        End If
        i = i + 1
    Next
    Call create_menu
End Sub
