sub ps0()
    sheet4.cells(11,3)=sheet1.cells(3,2)'填写单位名
    sheet4.cells(11,4)=sheet1.cells(3,3)'组织机构代码
    sheet4.cells(11,5)=sheet1.cells(3,4)'机构类型
    sheet4.cells(11,7)=sheet1.cells(3,5)'单位层次
    sheet4.cells(11,17)=sheet1.cells(3,6)'单位技术领域
end sub

Function GetAge(StrAge As String)
    StrAge = Mid(StrAge, 1, 4)
    GetAge = 2016 - Val(StrAge)
End Function

Function FillPsArray(sheet2_row, ps02() As Integer)
    ps02(0) = ps02(0) + 1

    If Sheet2.Cells(sheet2_row, 4) = "女" Then
        ps02(1) = ps02(1) + 1
    End If

    If Sheet2.Cells(sheet2_row, 5) <> "汉族" Then
        ps02(2) = ps02(2) + 1
    End If

    If Sheet2.Cells(sheet2_row, 10) <> "" Then
        ps02(3) = ps02(3) + 1
    End If

    If Sheet2.Cells(sheet2_row, 7) = "博士" Then
        ps02(4) = ps02(4) + 1
    End If

    If Sheet2.Cells(sheet2_row, 7) = "硕士" Then
        ps02(5) = ps02(5) + 1
        ps02(7) = ps02(7) + 1
    End If

    ps02(6) = 0 '港澳台

    If Sheet2.Cells(sheet2_row, 7) = "本科" Then
        ps02(8) = ps02(8) + 1
    End If

    If Sheet2.Cells(sheet2_row, 7) = "专科" Then
        ps02(9) = ps02(9) + 1
    End If

    If Sheet2.Cells(sheet2_row, 7) = "中专及以下" Then
        ps02(10) = ps02(10) + 1
    End If

    ps02(11) = 0

    '    -------------------------计算年龄-------------------------
    age = GetAge(Sheet2.Cells(sheet2_row, 6))
    If age <= 35 Then
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
End Function

Function InitPsArray(ps02() As Integer)
    For i = 0 To UBound(ps02)
        ps02(i) = 0
    Next
End Function

Function prtArray(ps02() As Integer)
    For i = 0 To UBound(ps02)
        MsgBox ps02(i)
    Next
End Function

Function fillExcel(ps02_row As Integer, ps02() As Integer)
    For i = 0 To UBound(ps02)
        Sheet6.Cells(ps02_row, i + 5) = ps02(i)
    Next
End Function

'-------------------------------------------------------------------------------------------------------------------
Function condition(ps02() as Integer,ObjectRow as integer,sheet2_col as Integer,ifcondition as string)
    dim sheet2_row as Integer
    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        if sheet2.cells(sheet2_row, sheet2_col) = ifcondition  then
            Call FillPsArray(sheet2_row, ps02)
        end if
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(ObjectRow, ps02)
end Function
'-------------------------------------------------------------------------------------------------------------------

Sub ps02Main()
    Dim ps02(32) As Integer
    Dim sheet2_row As Integer

    '-------------------------------------------------------------------------------------------------------------------
    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        Call FillPsArray(sheet2_row, ps02)
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(11, ps02)
    '-------------------------------------------------------------------------------------------------------------------

    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, 12) <> "" Then
            Call FillPsArray(sheet2_row, ps02)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(12, ps02)
    InitPsArray ps02
    '-------------------------------------------------------------------------------------------------------------------

    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        if sheet2.cells(sheet2_row, 12) <> "" and  sheet2.cells(sheet2_row, 4) <> "男" then
            Call FillPsArray(sheet2_row, ps02)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(13, ps02)
    '------------------------------------管理岗位的少数民族统计-------------------------------------------
    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        if sheet2.cells(sheet2_row, 12) <> "" and  sheet2.cells(sheet2_row, 5)  <> "汉族" then
            Call FillPsArray(sheet2_row, ps02)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(13, ps02)
    '-------------------------------------------------------------------------------------------------------------------
    call condition(ps02,15,12,"一级职员岗位（部级正职）")

End Sub



