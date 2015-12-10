sub ps0()
    sheet4.cells(11,3)=sheet1.cells(3,2)'填写单位名
    sheet4.cells(11,4)=sheet1.cells(3,3)'组织机构代码
    sheet4.cells(11,5)=sheet1.cells(3,4)'机构类型
    sheet4.cells(11,7)=sheet1.cells(3,5)'单位层次
    sheet4.cells(11,17)=sheet1.cells(3,6)'单位技术领域
end sub

Function InitPsArray(ps02() As Integer)
    For i = 0 To UBound(ps02)
        ps02(i) = 0
    Next
End Function

Function fillExcel(sheetIndex As Integer,ObjectRow As Integer,ObjectCol As Integer, ps02() As Integer)
    '表号、行号、列号、数组
    Dim sht
    Set sht = ThisWorkbook.Sheets(sheetIndex)
    For i = 0 To UBound(ps02)
        sht.Cells(ObjectRow, ObjectCol + i) = ps02(i)
    Next
End Function

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



Function condition(ps02() As Integer,sheetIndex As Integer, ObjectRow As Integer, condtionColA As Integer, conditionStrA As String, condtionColB As Integer, conditionStrB As String)
    '为了if第一个条件也能默认成立，在sheet2的AH一列只要有数据的全置为1，好些默认条件condtionColA=34 string=就为空
    Dim sheet2_row As Integer
    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, condtionColA) = conditionStrA And Sheet2.Cells(sheet2_row, condtionColB) <> conditionStrB Then  'conditionStrB默认设置为2也就是名字列，conditionStrB默认设置为空,那么无论如何第二个条件默认都成立
            Call FillPsArray(sheet2_row, ps02)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(sheetIndex,ObjectRow,5,ps02)
End Function

Sub ps02Main()
    Dim ps02(32) As Integer
    Dim sheet2_row As Integer

    CALL condition(ps02, 6,11, 34, "", 2, "") '总计
    CALL condition(ps02, 6,12, 34, "", 12, "") '管理人员小计
    CALL condition(ps02, 6,13, 4, "女", 2, "")
    CALL condition(ps02, 6,14, 5, "少数民族", 12, "")
    CALL condition(ps02, 6,15, 12, "一级职员岗位（部级正职）", 2, "")
    CALL condition(ps02, 6,16, 12, "二级职员岗位（部级副职）", 2, "")
    CALL condition(ps02, 6,17, 12, "三级职员岗位（厅级正职）", 2, "")
    CALL condition(ps02, 6,18, 12, "四级职员岗位（厅级副职）", 2, "")
    CALL condition(ps02, 6,19, 12, "五级职员岗位（处级正职）", 2, "")
    CALL condition(ps02, 6,20, 12, "六级职员岗位（处级副职）", 2, "")
    CALL condition(ps02, 6,21, 12, "七级职员岗位（科级正职）", 2, "")
    CALL condition(ps02, 6,22, 12, "八级职员岗位（科级副职）", 2, "")
    CALL condition(ps02, 6,23, 12, "九级职员岗位（科员）", 2, "")
    CALL condition(ps02, 6,24, 12, "十级职员岗位（办事员）", 2, "")
    CALL condition(ps02, 6,25, 12, "其他等级人员", 2, "")

    CALL condition(ps02, 6,26, 34, "", 13, "") '专技小计
    '"-----{
    { 双肩挑的无法用函数解决
    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, 12) <> "" And Sheet2.Cells(sheet2_row, 13) <> "" Then
            CALL FillPsArray(sheet2_row, ps02)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    CALL fillExcel(6,27,5,ps02)
    ''}}}}--------------------

    CALL condition(ps02, 6,28, 34, "", 15, "") '具有职业资格的
    CALL condition(ps02, 6,29, 4, "女", 13, "") '专业技术女
    CALL condition(ps02, 6,30, 5, "少数民族", 13, "") '专业技术少数民族
    CALL condition(ps02, 6,31, 13, "高级岗位一级", 2, "")
    CALL condition(ps02, 6,32, 13, "高级岗位二级", 2, "")
    '    "-----ps02.1
    CALL condition(ps02,7,11, 13, "高级岗位三级", 2, "")
    CALL condition(ps02,7,12, 13, "高级岗位四级", 2, "")
    CALL condition(ps02,7,13, 13, "高级岗位五级", 2, "")
    CALL condition(ps02,7,14, 13, "高级岗位六级", 2, "")
    CALL condition(ps02,7,15, 13, "高级岗位七级", 2, "")
    CALL condition(ps02,7,16, 13, "中级岗位八级", 2, "")
    CALL condition(ps02,7,17, 13, "中级岗位九级", 2, "")
    CALL condition(ps02,7,18, 13, "中级岗位十级", 2, "")
    CALL condition(ps02,7,19, 13, "初级岗位十一级", 2, "")
    CALL condition(ps02,7,20, 13, "初级岗位十二级", 2, "")
    CALL condition(ps02,7,21, 13, "初级岗位十三级", 2, "")
    CALL condition(ps02,7,22, 13, "其他", 2, "")
    '    统计工勤类别
    CALL condition(ps02,7,23, 34, "", 16,"") '工勤小计就是工勤总数
    CALL condition(ps02,7,24, 4, "女",16,"") '工勤女
    CALL condition(ps02,7,25, 5, "少数民族", 16, "") '工勤中的少数民族
    CALL condition(ps02,7,26, 16, "一级岗位（高级技师）", 2, "")
    CALL condition(ps02,7,27, 16, "二级岗位（技师）", 2, "")
    CALL condition(ps02,7,28, 16, "三级岗位（高级工）", 2, "")
    CALL condition(ps02,7,29, 16, "四级岗位（中级工）", 2, "")
    CALL condition(ps02,7,30, 16, "五级岗位（初级工）", 2, "")
    CALL condition(ps02,7,31, 16, "普通工", 2, "")
    CALL condition(ps02,7,32, 16, "其他等级人员", 2, "")
    CALL condition(ps02,7,33, 34, "",17,"")  '其他从业人员
End Sub
