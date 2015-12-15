Dim industry As String
Dim system As String
Dim level As String

Sub globaltest()
    globalx = 3
End Sub
Function InitPsArray(ps02() As Integer)
    For i = 0 To UBound(ps02)
        ps02(i) = 0
    Next
End Function

Function fillExcel(sheetIndex As Integer, ObjectRow As Integer, ObjectCol As Integer, ps02() As Integer)
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

FUNCTION ps01 ()

END FUNCTION

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

    Select Case system
        Case "1.党的系统"
            ps02(19) = ps02(0)
        Case "2.行政系统"
            ps02(20) = ps02(0)
        Case "3.人大"
            ps02(21) = ps02(0)
        Case "4.政协"
            ps02(22) = ps02(0)
        Case "5.法院"
            ps02(23) = ps02(0)
        Case "6.检察院"
            ps02(24) = ps02(0)
        Case "7.群众团体"
            ps02(25) = ps02(0)
        Case "8.民主党派"
            ps02(26) = ps02(0)
        Case "9.军队"
            ps02(27) = ps02(0)
        Case Else
    End Select


    If level = "3市州级单位" Then
        ps02(30) = ps02(0)
    ElseIf level = "4县级单位" Then
        ps02(31) = ps02(0)
    Else
        ps02(32) = ps02(0)
    End If

End Function



Function condition(ps02() As Integer, sheetIndex As Integer, ObjectRow As Integer, condtionColA As Integer, conditionStrA As String, condtionColB As Integer, conditionStrB As String)
    '为了if第一个条件也能默认成立，
    Dim sheet2_row As Integer
    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, condtionColA) = conditionStrA And Sheet2.Cells(sheet2_row, condtionColB) <> conditionStrB Then  'conditionStrB默认设置为2也就是名字列，conditionStrB默认设置为空,那么无论如何第二个条件默认都成立
            Call FillPsArray(sheet2_row, ps02)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(sheetIndex, ObjectRow, 5, ps02)
End Function
'------------------------------------------MAIN----------------------------------
Sub ps0Main()
    Sheet4.Cells(11, 3) = Sheet1.Cells(3, 2) '填写单位名
    Sheet4.Cells(11, 4) = Sheet1.Cells(3, 3) '组织机构代码
    Sheet4.Cells(11, 5) = Sheet1.Cells(3, 4) '机构类型
    Sheet4.Cells(11, 7) = Sheet1.Cells(3, 5) '单位层次
    Sheet4.Cells(11, 17) = Sheet1.Cells(3, 6) '单位技术领域
End Sub
Sub ps02Main()
    Dim ps02(32) As Integer
    Dim sheet2_row As Integer

    Call condition(ps02, 6, 11, 34, "", 2, "") '总计
    Call condition(ps02, 6, 12, 34, "", 12, "") '管理人员小计
    Call condition(ps02, 6, 13, 4, "女", 2, "")
    Call condition(ps02, 6, 14, 5, "少数民族", 12, "")
    Call condition(ps02, 6, 15, 12, "一级职员岗位（部级正职）", 2, "")
    Call condition(ps02, 6, 16, 12, "二级职员岗位（部级副职）", 2, "")
    Call condition(ps02, 6, 17, 12, "三级职员岗位（厅级正职）", 2, "")
    Call condition(ps02, 6, 18, 12, "四级职员岗位（厅级副职）", 2, "")
    Call condition(ps02, 6, 19, 12, "五级职员岗位（处级正职）", 2, "")
    Call condition(ps02, 6, 20, 12, "六级职员岗位（处级副职）", 2, "")
    Call condition(ps02, 6, 21, 12, "七级职员岗位（科级正职）", 2, "")
    Call condition(ps02, 6, 22, 12, "八级职员岗位（科级副职）", 2, "")
    Call condition(ps02, 6, 23, 12, "九级职员岗位（科员）", 2, "")
    Call condition(ps02, 6, 24, 12, "十级职员岗位（办事员）", 2, "")
    Call condition(ps02, 6, 25, 12, "其他等级人员", 2, "")

    Call condition(ps02, 6, 26, 34, "", 13, "") '专技小计
    '"-----{
    双肩挑的无法用函数解决
    sheet2_row = 3
    InitPsArray ps02
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, 12) <> "" And Sheet2.Cells(sheet2_row, 13) <> "" Then
            Call FillPsArray(sheet2_row, ps02)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(6, 27, 5, ps02)
    ''}}}}--------------------

    Call condition(ps02, 6, 28, 34, "", 15, "") '具有职业资格的
    Call condition(ps02, 6, 29, 4, "女", 13, "") '专业技术女
    Call condition(ps02, 6, 30, 5, "少数民族", 13, "") '专业技术少数民族
    Call condition(ps02, 6, 31, 13, "高级岗位一级", 2, "")
    Call condition(ps02, 6, 32, 13, "高级岗位二级", 2, "")
    '    "-----ps02.1
    Call condition(ps02, 7, 11, 13, "高级岗位三级", 2, "")
    Call condition(ps02, 7, 12, 13, "高级岗位四级", 2, "")
    Call condition(ps02, 7, 13, 13, "高级岗位五级", 2, "")
    Call condition(ps02, 7, 14, 13, "高级岗位六级", 2, "")
    Call condition(ps02, 7, 15, 13, "高级岗位七级", 2, "")
    Call condition(ps02, 7, 16, 13, "中级岗位八级", 2, "")
    Call condition(ps02, 7, 17, 13, "中级岗位九级", 2, "")
    Call condition(ps02, 7, 18, 13, "中级岗位十级", 2, "")
    Call condition(ps02, 7, 19, 13, "初级岗位十一级", 2, "")
    Call condition(ps02, 7, 20, 13, "初级岗位十二级", 2, "")
    Call condition(ps02, 7, 21, 13, "初级岗位十三级", 2, "")
    Call condition(ps02, 7, 22, 13, "其他", 2, "")
    '    统计工勤类别
    Call condition(ps02, 7, 23, 34, "", 16, "") '工勤小计就是工勤总数
    Call condition(ps02, 7, 24, 4, "女", 16, "") '工勤女
    Call condition(ps02, 7, 25, 5, "少数民族", 16, "") '工勤中的少数民族
    Call condition(ps02, 7, 26, 16, "一级岗位（高级技师）", 2, "")
    Call condition(ps02, 7, 27, 16, "二级岗位（技师）", 2, "")
    Call condition(ps02, 7, 28, 16, "三级岗位（高级工）", 2, "")
    Call condition(ps02, 7, 29, 16, "四级岗位（中级工）", 2, "")
    Call condition(ps02, 7, 30, 16, "五级岗位（初级工）", 2, "")
    Call condition(ps02, 7, 31, 16, "普通工", 2, "")
    Call condition(ps02, 7, 32, 16, "其他等级人员", 2, "")
    Call condition(ps02, 7, 33, 34, "", 17, "") '其他从业人员
End Sub
'--------------------------------------------03-------------------------------
Function FillPs03Array(sheet2_row, ps03() As Integer)
    ps03(0) = ps03(0) + 1
    If Sheet2.Cells(sheet2_row, 19) = "公开招考（招聘）应届大中专毕业生" Then
        ps03(1) = ps03(1) + 1
        ps03(3) = ps03(3) + 1
    End If
    If Sheet2.Cells(sheet2_row, 19) = "公开招考（招聘）社会在职人员" Then
        ps03(2) = ps03(2) + 1
        ps03(3) = ps03(3) + 1
    End If
    If Sheet2.Cells(sheet2_row, 19) = "调入" Then '调入就是考察过了才调入
        ps03(4) = ps03(4) + 1
    End If

    ps03(5) = 0 '表中不可能出现任命这种情况所以直接赋值0

    If Sheet2.Cells(sheet2_row, 19) = "政策性安置" Then
        ps03(6) = ps03(6) + 1
    End If

    ps03(7) = 0 '表中不可能出现交流这种情况所以直接赋值0

    If Sheet2.Cells(sheet2_row, 19) = "其他" Then
        ps03(8) = ps03(8) + 1
    End If
    '-----计算减员
    If Sheet2.Cells(sheet2_row, 21) = "退休" Then
        ps03(9) = ps03(9) + 1
    End If
    If Sheet2.Cells(sheet2_row, 21) = "解除合同" Then
        ps03(10) = ps03(10) + 1
    End If
    If Sheet2.Cells(sheet2_row, 21) = "开除" Then
        ps03(11) = ps03(11) + 1
    End If
    If Sheet2.Cells(sheet2_row, 21) = "终止合同" Then
        ps03(12) = ps03(12) + 1
    End If
    If Sheet2.Cells(sheet2_row, 21) = "辞职辞退" Then
        ps03(13) = ps03(13) + 1
    End If
    If Sheet2.Cells(sheet2_row, 21) = "交流" Then
        ps03(14) = ps03(14) + 1
    End If
    If Sheet2.Cells(sheet2_row, 21) = "其他" Then
        ps03(15) = ps03(15) + 1
    End If
    ps03(16) = ps03(9) + ps03(10) + ps03(11) + ps03(12) + ps03(13) + ps03(14) + ps03(15)
    ps03(17) = ps03(9)
End Function

Sub ps03Main()
    Dim ps03(17) As Integer
    Dim sheet2_row As Integer
    Dim CengCiRow As Integer
    Dim cengci As String
    CengCiRow = 0
    '-----------------------本段为了确定层次 市 县 乡
    cengci = Mid(Sheet1.Cells(3, 5), 1, 1)

    If cengci = "3" Then
        CengCiRow = 21
    ElseIf cengci = "4" Then
        CengCiRow = 22
    ElseIf cengci = "5" Then
        CengCiRow = 23
    End If

    '-----------------------本段为了确定层次 市 县 乡

    sheet2_row = 3
    InitPsArray ps03
    While Sheet2.Cells(sheet2_row, 2) <> ""  '总计
        If Sheet2.Cells(sheet2_row, 19) <> "" Or Sheet2.Cells(sheet2_row, 21) <> "" Then
            Call FillPs03Array(sheet2_row, ps03)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(8, 13, 4, ps03)
    Call fillExcel(8, CengCiRow, 4, ps03)

    sheet2_row = 3
    InitPsArray ps03
    While Sheet2.Cells(sheet2_row, 2) <> "" '管理人员
        If Sheet2.Cells(sheet2_row, 19) <> "" Or Sheet2.Cells(sheet2_row, 21) <> "" Then
            If Sheet2.Cells(sheet2_row, 12) <> "" Then
                Call FillPs03Array(sheet2_row, ps03)
            End If
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(8, 14, 4, ps03)

    InitPsArray ps03
    sheet2_row = 3
    While Sheet2.Cells(sheet2_row, 2) <> "" '专技人员
        If Sheet2.Cells(sheet2_row, 19) <> "" Or Sheet2.Cells(sheet2_row, 21) <> "" Then
            If Sheet2.Cells(sheet2_row, 13) <> "" Then
                Call FillPs03Array(sheet2_row, ps03)
            End If
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(8, 15, 4, ps03)

    sheet2_row = 3
    InitPsArray ps03
    While Sheet2.Cells(sheet2_row, 2) <> ""  '双肩挑
        If Sheet2.Cells(sheet2_row, 19) <> "" Or Sheet2.Cells(sheet2_row, 21) <> "" Then
            If Sheet2.Cells(sheet2_row, 12) <> "" And Sheet2.Cells(sheet2_row, 13) <> "" Then
                Call FillPs03Array(sheet2_row, ps03)
            End If
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(8, 16, 4, ps03)

    sheet2_row = 3 '工勤
    InitPsArray ps03
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, 19) <> "" Or Sheet2.Cells(sheet2_row, 21) <> "" Then
            If Sheet2.Cells(sheet2_row, 16) <> "" Then
                Call FillPs03Array(sheet2_row, ps03)
            End If
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(8, 17, 4, ps03)

    sheet2_row = 3
    InitPsArray ps03
    While Sheet2.Cells(sheet2_row, 2) <> "" '其他
        If Sheet2.Cells(sheet2_row, 19) <> "" Or Sheet2.Cells(sheet2_row, 21) <> "" Then
            If Sheet2.Cells(sheet2_row, 17) <> "" Then
                Call FillPs03Array(sheet2_row, ps03)
            End If
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(8, 18, 4, ps03)


End Sub


'ps04-------------------------------------------------
'初步估计人员情况表，数据不全面，如ps04中要求的特岗，无法从人员信息表中得出，故而该列不填
'所谓的在岗人数也无法体现，只能岗位和人数直接对等
Sub ps04shuangjiantiao()
    Dim sheet2_row As Integer
    Dim ps04(14) As Integer
    Dim level As String
    sheet2_row = 3
    InitPsArray ps04
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, 12) <> "" And Sheet2.Cells(sheet2_row, 13) <> "" Then
            level = Mid(Sheet2.Cells(sheet2_row, 13), 5, 6)
            ps04(0) = ps04(0) + 1
            If level = "一级" Then
                ps04(1) = ps04(1) + 1
            ElseIf level = "二级" Then
                ps04(2) = ps04(2) + 1
            ElseIf level = "三级" Then
                ps04(3) = ps04(4) + 1
            ElseIf level = "四级" Then
                ps04(4) = ps04(4) + 1
            ElseIf level = "五级" Then
                ps04(5) = ps04(5) + 1
            ElseIf level = "六级" Then
                ps04(6) = ps04(6) + 1
            ElseIf level = "七级" Then
                ps04(7) = ps04(7) + 1
            ElseIf level = "八级" Then
                ps04(8) = ps04(8) + 1
            ElseIf level = "九级" Then
                ps04(9) = ps04(9) + 1
            ElseIf level = "十级" Then
                ps04(10) = ps04(10) + 1
            ElseIf level = "十一级" Then
                ps04(11) = ps04(11) + 1
            ElseIf level = "十二级" Then
                ps04(12) = ps04(13) + 1
            ElseIf level = "十三级" Then
                ps04(13) = ps04(13) + 1
            End If
        End If
        sheet2_row = sheet2_row + 1
    Wend
    For i = 13 To 26
        Sheet9.Cells(i, 8) = ps04(i - 13)
    Next
End Sub
Sub ps04Main()
    ps04shuangjiantiao
    Sheet9.Cells(13, 4) = Sheet6.Cells(12, 5) '合计
    Sheet9.Cells(14, 4) = Sheet6.Cells(15, 5) '管理一级
    Sheet9.Cells(15, 4) = Sheet6.Cells(16, 5) '
    Sheet9.Cells(16, 4) = Sheet6.Cells(17, 5) '
    Sheet9.Cells(17, 4) = Sheet6.Cells(18, 5) '
    Sheet9.Cells(18, 4) = Sheet6.Cells(19, 5) '
    Sheet9.Cells(19, 4) = Sheet6.Cells(20, 5) '
    Sheet9.Cells(20, 4) = Sheet6.Cells(21, 5) '
    Sheet9.Cells(21, 4) = Sheet6.Cells(22, 5) '
    Sheet9.Cells(22, 4) = Sheet6.Cells(23, 5) '
    Sheet9.Cells(23, 4) = Sheet6.Cells(24, 5) '
    Sheet9.Cells(27, 4) = Sheet6.Cells(25, 5) '其他

    Sheet9.Cells(13, 5) = Sheet6.Cells(12, 5) '合计
    Sheet9.Cells(14, 5) = Sheet6.Cells(15, 5) '在岗人数
    Sheet9.Cells(15, 5) = Sheet6.Cells(16, 5) '
    Sheet9.Cells(16, 5) = Sheet6.Cells(17, 5) '
    Sheet9.Cells(17, 5) = Sheet6.Cells(18, 5) '
    Sheet9.Cells(18, 5) = Sheet6.Cells(19, 5) '
    Sheet9.Cells(19, 5) = Sheet6.Cells(20, 5) '
    Sheet9.Cells(20, 5) = Sheet6.Cells(21, 5) '
    Sheet9.Cells(21, 5) = Sheet6.Cells(22, 5) '
    Sheet9.Cells(22, 5) = Sheet6.Cells(23, 5) '
    Sheet9.Cells(23, 5) = Sheet6.Cells(24, 5) '
    Sheet9.Cells(27, 5) = Sheet6.Cells(25, 5) '其他


    Sheet9.Cells(13, 6) = Sheet6.Cells(26, 5) '专技 小计
    Sheet9.Cells(14, 6) = Sheet6.Cells(31, 5) '1
    Sheet9.Cells(15, 6) = Sheet6.Cells(32, 5) '2
    Sheet9.Cells(16, 6) = Sheet7.Cells(11, 5) '3
    Sheet9.Cells(17, 6) = Sheet7.Cells(12, 5) '4
    Sheet9.Cells(18, 6) = Sheet7.Cells(13, 5) '5
    Sheet9.Cells(19, 6) = Sheet7.Cells(14, 5) '6
    Sheet9.Cells(20, 6) = Sheet7.Cells(15, 5) '7
    Sheet9.Cells(21, 6) = Sheet7.Cells(16, 5) '8
    Sheet9.Cells(22, 6) = Sheet7.Cells(17, 5) '9
    Sheet9.Cells(23, 6) = Sheet7.Cells(18, 5) '10
    Sheet9.Cells(24, 6) = Sheet7.Cells(19, 5) '11
    Sheet9.Cells(25, 6) = Sheet7.Cells(20, 5) '12
    Sheet9.Cells(26, 6) = Sheet7.Cells(21, 5) '13
    Sheet9.Cells(27, 6) = Sheet7.Cells(22, 5) '其他

    Sheet9.Cells(13, 7) = Sheet6.Cells(26, 5) '专技 小计
    Sheet9.Cells(14, 7) = Sheet6.Cells(31, 5) '1
    Sheet9.Cells(15, 7) = Sheet6.Cells(32, 5) '2
    Sheet9.Cells(16, 7) = Sheet7.Cells(11, 5) '3
    Sheet9.Cells(17, 7) = Sheet7.Cells(12, 5) '4
    Sheet9.Cells(18, 7) = Sheet7.Cells(13, 5) '5
    Sheet9.Cells(19, 7) = Sheet7.Cells(14, 5) '6
    Sheet9.Cells(20, 7) = Sheet7.Cells(15, 5) '7
    Sheet9.Cells(21, 7) = Sheet7.Cells(16, 5) '8
    Sheet9.Cells(22, 7) = Sheet7.Cells(17, 5) '9
    Sheet9.Cells(23, 7) = Sheet7.Cells(18, 5) '10
    Sheet9.Cells(24, 7) = Sheet7.Cells(19, 5) '11
    Sheet9.Cells(25, 7) = Sheet7.Cells(20, 5) '12
    Sheet9.Cells(26, 7) = Sheet7.Cells(21, 5) '13
    Sheet9.Cells(27, 7) = Sheet7.Cells(22, 5) '其他

    Sheet9.Cells(13, 9) = Sheet7.Cells(23, 5) '工勤小计
    Sheet9.Cells(14, 9) = Sheet7.Cells(26, 5) '1
    Sheet9.Cells(15, 9) = Sheet7.Cells(27, 5) '2
    Sheet9.Cells(16, 9) = Sheet7.Cells(28, 5) '3
    Sheet9.Cells(17, 9) = Sheet7.Cells(29, 5) '4
    Sheet9.Cells(18, 9) = Sheet7.Cells(30, 5) '5
    Sheet9.Cells(19, 9) = Sheet7.Cells(31, 5) '6 普通工
    Sheet9.Cells(27, 9) = Sheet7.Cells(32, 5) '其他

    Sheet9.Cells(13, 10) = Sheet7.Cells(23, 5) '工勤小计
    Sheet9.Cells(14, 10) = Sheet7.Cells(26, 5) '1
    Sheet9.Cells(15, 10) = Sheet7.Cells(27, 5) '2
    Sheet9.Cells(16, 10) = Sheet7.Cells(28, 5) '3
    Sheet9.Cells(17, 10) = Sheet7.Cells(29, 5) '4
    Sheet9.Cells(18, 10) = Sheet7.Cells(30, 5) '5
    Sheet9.Cells(19, 10) = Sheet7.Cells(31, 5) '6 普通工
    Sheet9.Cells(27, 10) = Sheet7.Cells(32, 5) '其他

End Sub
'--------------------------------------------------------ps05相关函数----------------------------------------------------------------
Function FillPs05Array(sheet2_row, ps05() As Integer)
    '表中数据必须按照下拉菜单中的数据来选择，如果自行填入，程序将不能得出正确结果
    '表中数据必须按照下拉菜单中的数据来选择，如果自行填入，程序将不能得出正确结果
    ps05(0) = ps05(0) + 1
    Select Case Sheet2.Cells(sheet2_row, 24)
        Case "上年末已签订短期合同"
            ps05(2) = ps05(2) + 1
        Case "上年末已签订中期合同"
            ps05(3) = ps05(3) + 1
        Case "上年末已签订长期合同"
            ps05(4) = ps05(4) + 1
        Case "上年末已签订项目合同"
            ps05(5) = ps05(5) + 1
        Case "新签订短期合同"
            ps05(6) = ps05(6) + 1
        Case "新签订中期合同"
            ps05(7) = ps05(7) + 1
        Case "新签订长期合同"
            ps05(8) = ps05(8) + 1
        Case "新签订项目合同"
            ps05(9) = ps05(9) + 1
        Case "解除聘用合同"
            ps05(10) = ps05(10) + 1
        Case "终止聘用合同"
            ps05(11) = ps05(11) + 1
        Case "未签订聘用合同(原固定用人方式)"
            ps05(13) = ps05(13) + 1
        Case "未签订聘用合同(签订劳动合同)"
            ps05(14) = ps05(14) + 1
        Case "未签订聘用合同(其他)"
            ps05(15) = ps05(15) + 1
        Case Else
    End Select
    ps05(1) = ps05(2) + ps05(3) + ps05(4) + ps05(5) + ps05(6) + ps05(7) + ps05(8) + ps05(9)
    ps05(12) = ps05(13) + ps05(14) + ps05(15) '未签订合同的可以直接从数组中求得就不再遍历excel
End Function

Sub ps05Main()
    Dim ps05(15) As Integer
    Dim sheet2_row As Integer
    Dim x As Integer
    x = 0

    sheet2_row = 3
    InitPsArray ps05
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, 24) <> "" And Sheet2.Cells(sheet2_row, 12) <> "" Then   '管理
            Call FillPs05Array(sheet2_row, ps05)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(10, 12, 3, ps05)

    sheet2_row = 3
    InitPsArray ps05
    While Sheet2.Cells(sheet2_row, 2) <> "" '专技人员
        If Sheet2.Cells(sheet2_row, 13) <> "" And Sheet2.Cells(sheet2_row, 24) <> "" Then  '双肩挑
            Call FillPs05Array(sheet2_row, ps05)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(10, 13, 3, ps05)

    sheet2_row = 3
    InitPsArray ps05
    While Sheet2.Cells(sheet2_row, 2) <> ""
        If Sheet2.Cells(sheet2_row, 12) <> "" And Sheet2.Cells(sheet2_row, 13) <> "" And Sheet2.Cells(sheet2_row, 24) <> "" Then '双肩挑
            Call FillPs05Array(sheet2_row, ps05)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(10, 14, 3, ps05)

    sheet2_row = 3
    InitPsArray ps05
    While Sheet2.Cells(sheet2_row, 2) <> "" '工勤
        If Sheet2.Cells(sheet2_row, 16) <> "" And Sheet2.Cells(sheet2_row, 24) <> "" Then
            Call FillPs05Array(sheet2_row, ps05)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(10, 15, 3, ps05)

    sheet2_row = 3
    InitPsArray ps05
    While Sheet2.Cells(sheet2_row, 2) <> "" '其他
        If Sheet2.Cells(sheet2_row, 17) <> "" And Sheet2.Cells(sheet2_row, 24) <> "" Then
            Call FillPs05Array(sheet2_row, ps05)
        End If
        sheet2_row = sheet2_row + 1
    Wend
    Call fillExcel(10, 16, 3, ps05)
End Sub

Sub Mian()
    system = Sheet1.Cells(3, 1)
    industry = Sheet1.Cells(3, 6)
    level = Sheet1.Cells(3, 5)

    ps0Main
    ps02Main
    ps03Main
    ps04Main
    ps05Main
    MsgBox "已成功生成"
End Sub
