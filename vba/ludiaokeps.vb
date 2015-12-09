sub ps0Main()
    sheet4.cells(11,3)=sheet1.cells(3,2)'填写单位名
    sheet4.cells(11,4)=sheet1.cells(3,3)'组织机构代码
    sheet4.cells(11,5)=sheet1.cells(3,4)'机构类型
    sheet4.cells(11,7)=sheet1.cells(3,5)'单位层次
    sheet4.cells(11,17)=sheet1.cells(3,6)'单位技术领域
end sub

Function GetAge(StrAge As String)
    '计算形如1985/7/8格式的年龄
    StrAge = Mid(StrAge, 1, 4)
    GetAge = 2016 - Val(StrAge)
End Function

Function InitPsArray(ps02() As Integer)
    '初始化数组，避免未赋值情况
    For i = 0 To UBound(ps02)
        ps02(i) = 0
    Next
End Function

Function fillExcel(ps02_row as Integer,ps02() As Integer)
    '将计算得到的ps02数组中的数据对应填入相应行
    for i in UBound(ps02)
        sheet6.cells(ps02_row,i+3) = ps02(i)
    next
End Function

function GetIndexRow()

end function

Function FillPsArray(indexRows() as integer, ps02() As Integer)
    '本函数负责从indexRows中取出行号，以行号为循环条件，将ps02全部赋值
    InitPsArray ps02
    for each sheet2_row in indexRows
        ps02(0) = ps02(0) + 1
        If sheet2.Cells(sheet2_row, 4) = "女" Then
            ps02(1) = ps02(1) + 1
        End If

        If sheet2.Cells(sheet2_row, 5) <> "汉族" Then
            ps02(2) = ps02(2) + 1
        End If

        If sheet2.Cells(sheet2_row, 10) <> "" Then
            ps02(3) = ps02(3) + 1
        End If

        if sheet2.cells(sheet2_row,7) = "博士" then 
            ps02(4) = ps02(4) + 1
        End If

        If sheet2.Cells(sheet2_row,7) = "硕士" Then
            ps02(5) = ps02(5) + 1
            ps02(7) = ps02(7) + 1
        End If

        ps02(6) = 0 '港澳台

        If sheet2.Cells(sheet2_row,7) = "本科" Then
            ps02(8) = ps02(8) + 1
        End If

        If sheet2.Cells(sheet2_row,7) = "专科" Then
            ps02(9) = ps02(9) + 1
        End If

        If sheet2.Cells(sheet2_row,7) = "中专及以下" Then
            ps02(10) = ps02(10) + 1
        End If

        ps02(11) = 0

        '    -------------------------计算年龄-------------------------
        age = GetAge(sheet2.Cells(sheet2_row, 6))
        if age <= 35 then 
            ps02(12) = ps02(12) + 1

        ElseIf age >= 36 And age <= 40 Then
            ps02(13) = ps02(13) + 1

        ElseIf age >= 41 And age <= 45 Then
            ps02(14) = ps02(14) + 1

        ElseIf age >= 46 And age <= 50 Then
            ps02(15) = ps02(15) + 1

        ElseIf age >= 51 And age <= 54 Then
            ps02(16) = ps02(16) + 1

        ElseIf age >= 55 And age <= 59 Then
            ps02(17) = ps02(17) + 1

        ElseIf age >= 60 Then
            ps02(18) = ps02(18) + 1
        End If
    next
End Function


function fill(indexRows() as integer,ps02() as integer)
end Function

Sub ps02Main()
    Dim ps02(32) As Integer
    dim indexRows() As integer
    dim x as integer
    x = 0

    sheet2.Range("$A$1:$C$19").AutoFilter Field:=1 '全选不需要参数总计
    x = sheet2.Range("A2", [a65536].End(3)).SpecialCells(xlCellTypeVisible).Count
    ReDim indexRows(x - 1)
    For Each c In sheet2.Range("A2", [a65536].End(3)).SpecialCells(xlCellTypeVisible)
        indexRows(a) = c
        a = a + 1
    Next
    FillPsArray(indexRows,ps02)
End Sub

Sub Macro3()
    Dim a, x As Integer
    Dim marray() As Integer
    a = 0
    x = 0

    'ActiveSheet.Range("$A$1:$C$19").AutoFilter Field:=1, Criteria1:="(全选)"
    sheet1.Range("$A$1:$C$19").AutoFilter Field:=1 '全选不需要参数
    x = sheet1.Range("A2", [a65536].End(3)).SpecialCells(xlCellTypeVisible).Count
    ReDim marray(x - 1)
    For Each c In sheet1.Range("A2", [a65536].End(3)).SpecialCells(xlCellTypeVisible)
        marray(a) = c
        a = a + 1

    Next
    ReDim marray(4)

End Sub
