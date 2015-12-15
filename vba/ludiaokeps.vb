Dim industry As String
Dim system As String
Dim CityLevel As String
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
Function FillPs01Array(sheet2_row, ps01() As Integer)
    ps01(0) = 1
    ps01(1) = 1

    Select Case Sheet1.Cells(3, 8)
        Case "1.公益一类"
            ps01(2) = 1
        Case "2.公益二类"
            ps01(3) = 1
        Case "3.其他"
            ps01(4) = 1
        Case Else
    End Select

    Select Case Sheet1.Cells(3, 9)
        Case "1.已完成岗位设置方案备案的单位"
            ps01(5) = 1
        Case "2.已完成岗位设置的单位"
            ps01(6) = 1
        Case "3.实行聘用管理制度的单位"
            ps01(7) = 1
        Case "4.实行公开招聘制度的单位"
            ps01(8) = 1
        Case Else
    End Select

    If Sheet1.Cells(3, 6) Like "*教育*" Then
        ps01(9) = 1
    ElseIf Sheet1.Cells(3, 6) Like "*科学研究*" Then
        ps01(10) = 1
    ElseIf Sheet1.Cells(3, 6) Like "*文化*" Then
        ps01(11) = 1
    ElseIf Sheet1.Cells(3, 6) Like "*卫生*" Then
        ps01(12) = 1
    ElseIf Sheet1.Cells(3, 6) Like "*体育*" Then
        ps01(13) = 1
    ElseIf Sheet1.Cells(3, 6) = "农林牧渔业" Then
        ps01(14) = 1
    Else
        ps01(15) = 1
    End If

    If CityLevel = "3市州级单位" Then
        ps01(18) = 1
    ElseIf CityLevel = "4县级单位" Then
        ps01(19) = 1
    Else
        ps01(20) = 1
    End If
    
    ps01(21) = Sheet1.Cells(3, 10)
    ps01(22) = Sheet1.Cells(3, 11)
    ps01(23) = Sheet1.Cells(3, 12)
    ps01(24) = Sheet1.Cells(3, 13)
    ps01(25) = Sheet1.Cells(3, 14)
End Function

Sub ps01Main()
    Dim ps01(25) As Integer
    Dim sheet2_row As Integer

    Call InitPsArray(ps01)
    Call FillPs01Array(sheet2_row, ps01)
    Call fillExcel(5, 11, 4, ps01)

    If industry Like "*教育*" Then
        Call fillExcel(5, 16, 4, ps01)
    ElseIf industry Like "*科学*" Then
        Call fillExcel(5, 17, 4, ps01)
    ElseIf industry Like "*文化*" Then
        Call fillExcel(5, 18, 4, ps01)
    ElseIf industry Like "*卫生*" Then
        Call fillExcel(5, 19, 4, ps01)
    ElseIf industry Like "*体育*" Then
        Call fillExcel(5, 20, 4, ps01)
    ElseIf industry Like "*渔*" Then
        Call fillExcel(5, 21, 4, ps01)
    Else
        Call fillExcel(5, 22, 4, ps01)
    End If
    
    If system Like "*党的*" Then
        Call fillExcel(5, 23, 4, ps01)
    ElseIf system Like "*行政*" Then
        Call fillExcel(5, 24, 4, ps01)
    ElseIf system Like "*人大*" Then
        Call fillExcel(5, 25, 4, ps01)
    ElseIf system Like "*政协*" Then
        Call fillExcel(5, 26, 4, ps01)
    ElseIf system Like "*法院*" Then
        Call fillExcel(5, 27, 4, ps01)
    ElseIf system Like "*检擦院*" Then
        Call fillExcel(5, 28, 4, ps01)
    ElseIf system Like "*群众*" Then
        Call fillExcel(5, 29, 4, ps01)
    ElseIf system Like "*民主*" Then
        Call fillExcel(5, 30, 4, ps01)
    ElseIf system Like "*军队*" Then
        Call fillExcel(5, 31, 4, ps01)
    End If


    If CityLevel = "3市州级单位" Then
        Call fillExcel(5, 34, 4, ps01)
    ElseIf CityLevel = "4县级单位" Then
        Call fillExcel(5, 35, 4, ps01)
    Else
        Call fillExcel(5, 36, 4, ps01)
    End If
    
sheet2_row = 0
End Sub

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


    If CityLevel = "3市州级单位" Then
        ps02(30) = ps02(0)
    ElseIf CityLevel = "4县级单位" Then
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
    '"-----{{ 双肩挑的无法用函数解决
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
Function GetPs07Line(ps07line As Integer, ps07sheetIndex As Integer)
    Select Case industry
        Case "1.农林牧渔业"
            ps07line = 14
            ps07sheetIndex = 0
        Case "2.采矿业"
            ps07line = 19
            ps07sheetIndex = 0
        Case "3.制造业"
            ps07line = 20
            ps07sheetIndex = 0
        Case "4.电力、煤气及水的生产和供应业"
            ps07line = 21
            ps07sheetIndex = 0
        Case "5.建筑业"
            ps07line = 22
            ps07sheetIndex = 0
        Case "6.批发和零售业"
            ps07line = 23
            ps07sheetIndex = 0
        Case "7.交通运输、仓储和邮政业"
            ps07line = 24
            ps07sheetIndex = 0
        Case "8.住宿和餐饮业"
            ps07line = 25
            ps07sheetIndex = 0
        Case "9.信息传输、计算机服务和软件业"
            ps07line = 26
            ps07sheetIndex = 0
        Case "10.金融业"
            ps07line = 27
            ps07sheetIndex = 0
        Case "11.房地产业"
            ps07line = 28
            ps07sheetIndex = 0
        Case "12.租赁和商务服务业"
            ps07line = 29
            ps07sheetIndex = 0
        Case "13.科学研究、技术服务和地质勘查"
            ps07line = 30
            ps07sheetIndex = 0
        Case "14.水利、环境和公共设施管理业"
            ps07line = 31
            ps07sheetIndex = 0

        Case "15.居民服务和其他服务业"
            ps07line = 13
            ps07sheetIndex = 1
        Case "16.教育"
            ps07line = 14
            ps07sheetIndex = 1
        Case "17.卫生、社会保障和社会福利业"
            ps07line = 21
            ps07sheetIndex = 1
        Case "18.文化体育和娱乐业"
            ps07line = 26
            ps07sheetIndex = 1
        Case "19.公共管理和社会组织"
            ps07line = 29
            ews07sheetIndex = 1
        Case "20.国际组织"
            ps07line = 30
            ps07sheetIndex = 1
    End Select

End Function



Function FillPs071_074Array(ps0714() As Integer)
    Dim qitacongyerenyuan As Integer
    qitacongyerenyuan = Sheet7.Cells(33, 5)

    '-----管理岗位
    ps0714(0) = Sheet6.Cells(11, 5)
    For i = 15 To 25
        ps0714(i - 14) = Sheet6.Cells(i, 5)
    Next
    ps0714(12) = qitacongyerenyuan

    '----层级
    ps0714(13) = 0
    ps0714(14) = 0
    If CityLevel = "3市州级单位" Then
        ps0714(15) = ps0714(0)
    ElseIf CityLevel = "4县级单位" Then
        ps0714(16) = ps0714(0)
    Else
        ps0714(17) = ps0714(0)
    End If
    '----专业技术
    ps0714(18) = Sheet6.Cells(26, 5)
    ps0714(19) = Sheet6.Cells(31, 5)
    ps0714(20) = Sheet6.Cells(32, 5)

    For i = 11 To 22
        ps0714(i + 10) = Sheet7.Cells(i, 5)
    Next
    ps0714(33) = qitacongyerenyuan
    '----层级
    ps0714(34) = 0
    ps0714(35) = 0
    If CityLevel = "3市州级单位" Then
        ps0714(36) = ps0714(18)
    ElseIf CityLevel = "4县级单位" Then
        ps0714(37) = ps0714(18)
    Else
        ps0714(38) = ps0714(18)
    End If
    '----双肩挑
    ps0714(39) = Sheet6.Cells(27, 5)
    ps0714(40) = Sheet6.Cells(28, 5)
    '----工勤岗位
    ps0714(41) = Sheet7.Cells(23, 5)

    For i = 26 To 32
        ps0714(i + 16) = Sheet7.Cells(i, 5)
    Next
    ps0714(49) = qitacongyerenyuan
    '----层级
    ps0714(50) = 0
    ps0714(51) = 0
    If CityLevel = "3市州级单位" Then
        ps0714(52) = ps0714(41)
    ElseIf CityLevel = "4县级单位" Then
        ps0714(53) = ps0714(41)
    Else
        ps0714(54) = ps0714(41)
    End If
End Function

Sub ps074Main()
    Dim ps074(55) As Integer
    InitPsArray ps074
    Dim ps07line As Integer
    Dim ps07sheetIndex As Integer
    ps07line = 0
    ps07sheetIndex = 0
    Call FillPs071_074Array(ps074)
    Call GetPs07Line(ps07line, ps07sheetIndex)

    For i = 0 To 28
        Sheet12.Cells(13, i + 3) = ps074(i)
    Next
    For i = 29 To 54
        Sheet14.Cells(13, i - 26) = ps074(i)
    Next

    If ps07sheetIndex = 0 Then
        For i = 0 To 28
            Sheet12.Cells(ps07line, i + 3) = ps074(i)
        Next
        For i = 29 To 54
            Sheet14.Cells(ps07line, i + 3) = ps074(i)
        Next
    Else
        For i = 0 To 28
            Sheet13.Cells(ps07line, i + 3) = ps074(i)
        Next
        For i = 29 To 54
            Sheet15.Cells(ps07line, i - 26) = ps074(i)
        Next
    End If

    ps07line = 0
End Sub

Function FillPs07456Array(ps07456() As Integer)
    For i = 5 To 23
        ps07456(i - 5) = Sheet6.Cells(11, i)
    Next
    ps07456(19) = Sheet6.Cells(27, 5)
End Function
Function FillPs077Array(ps077() As Integer)
ps077(0) = Sheet12.Cells(2, 3)  '农林
ps077(1) = Sheet12.Cells(19, 3)  '采矿业
ps077(2) = Sheet12.Cells(20, 3)  '制造业
ps077(3) = Sheet12.Cells(21, 3)  '电力、热力、燃气及水生产和供应业
ps077(4) = Sheet12.Cells(22, 3)  '建筑业
ps077(5) = Sheet12.Cells(23, 3)  '批发和零售业
ps077(6) = Sheet12.Cells(24, 3)  '交通运输、仓储和邮政业
ps077(7) = Sheet12.Cells(25, 3)  '住宿和餐饮业
ps077(8) = Sheet12.Cells(26, 3)  '信息传输、软件和信息技术服务业
ps077(9) = Sheet12.Cells(27, 3)  '金融业
ps077(10) = Sheet12.Cells(28, 3) '房地产业
ps077(11) = Sheet12.Cells(29, 3) '租赁和商务服务业
ps077(12) = Sheet12.Cells(30, 3) '科学研究和技术服务业
ps077(13) = Sheet13.Cells(31, 3) '水利、环境和公共设施管理业
ps077(14) = Sheet13.Cells(13, 3) '居民服务、修理和其他服务业
ps077(15) = Sheet13.Cells(14, 3) '教育
ps077(16) = Sheet13.Cells(21, 3) '卫生和社会工作
ps077(17) = Sheet13.Cells(26, 3) '文化、体育和娱乐业
ps077(18) = Sheet13.Cells(29, 3) '公共管理、社会保障和社会组织
ps077(19) = Sheet13.Cells(30, 3) '国际组织
ps077(20) = 0 '中央
ps077(21) = 0 '省（区、市）
ps077(22) = Sheet13.Cells(16, 15) '地（市、州、盟）
ps077(23) = Sheet13.Cells(16, 16) '县（市、区、旗）
ps077(24) = Sheet13.Cells(16, 17) '乡（镇）
End Function


Sub ps07456Main()
    Dim ps07456(19) As Integer
    Dim ps077(24) As Integer
    Call FillPs07456Array(ps07456)
    Call FillPs077Array(ps077)
    Call fillExcel(16, 12, 4, ps07456)
    Call fillExcel(19, 13, 4, ps077)
    
    If CityLevel = "3市州级单位" Then
        Call fillExcel(16, 15, 4, ps07456)
    ElseIf CityLevel = "4县级单位" Then
        Call fillExcel(16, 16, 4, ps07456)
    Else
        Call fillExcel(16, 17, 4, ps07456)
    End If

    Select Case industry
        Case "1.农林牧渔业"
            Call fillExcel(16, 18, 4, ps07456)
            '农业技术人员 15
            Call fillExcel(18, 15, 4, ps07456)
            Call fillExcel(19, 15, 4, ps077)
        Case "2.采矿业"
            Call fillExcel(16, 23, 4, ps07456)
            '工程技术人员 14
            Call fillExcel(18, 14, 4, ps07456)
            Call fillExcel(19, 14, 4, ps077)
        Case "3.制造业"
            Call fillExcel(16, 24, 4, ps07456)
            '工程技术人员 14
            Call fillExcel(18, 14, 4, ps07456)
            Call fillExcel(19, 14, 4, ps077)
        Case "4.电力、煤气及水的生产和供应业"
            Call fillExcel(16, 25, 4, ps07456)
        Case "5.建筑业"
            Call fillExcel(16, 26, 4, ps07456)
            '工程技术人员 14
            Call fillExcel(18, 14, 4, ps07456)
            Call fillExcel(19, 14, 4, ps077)
        Case "6.批发和零售业"
            Call fillExcel(16, 27, 4, ps07456)
        Case "7.交通运输、仓储和邮政业"
            Call fillExcel(16, 28, 4, ps07456)
        Case "8.住宿和餐饮业"
            Call fillExcel(16, 29, 4, ps07456)
        Case "9.信息传输、计算机服务和软件业"
            Call fillExcel(16, 30, 4, ps07456)
            '工程技术人员 14
            Call fillExcel(18, 14, 4, ps07456)
            Call fillExcel(19, 14, 4, ps077)
        Case "10.金融业"
            Call fillExcel(16, 31, 4, ps07456)
            '经济人员 19
            Call fillExcel(18, 19, 4, ps07456)
            Call fillExcel(19, 19, 4, ps077)
        Case "11.房地产业"
            Call fillExcel(16, 32, 4, ps07456)

        Case "12.租赁和商务服务业"
            Call fillExcel(17, 12, 4, ps07456)
        Case "13.科学研究、技术服务和地质勘查"
            Call fillExcel(17, 13, 4, ps07456)
            '科学研究人员 16
            Call fillExcel(18, 16, 4, ps07456)
            Call fillExcel(19, 16, 4, ps077)
        Case "14.水利、环境和公共设施管理业"
            Call fillExcel(17, 14, 4, ps07456)
        Case "15.居民服务和其他服务业"
            Call fillExcel(17, 15, 4, ps07456)
        Case "16.教育"
            Call fillExcel(17, 16, 4, ps07456)
            '教学人员 18
            Call fillExcel(18, 18, 4, ps07456)
            Call fillExcel(19, 18, 4, ps077)
        Case "17.卫生、社会保障和社会福利业"
            Call fillExcel(17, 23, 4, ps07456)
            '卫生技术人员 17
            Call fillExcel(18, 17, 4, ps07456)
            Call fillExcel(19, 17, 4, ps077)
        Case "18.文化体育和娱乐业"
            Call fillExcel(17, 28, 4, ps07456)
            '工艺美术人员 27 播音人员 26 体育人员 28 艺术人员 29
            Call fillExcel(18, 27, 4, ps07456)
            Call fillExcel(18, 26, 4, ps07456)
            Call fillExcel(18, 28, 4, ps07456)
            Call fillExcel(18, 29, 4, ps07456)

            Call fillExcel(19, 27, 4, ps077)
            Call fillExcel(19, 26, 4, ps077)
            Call fillExcel(19, 28, 4, ps077)
            Call fillExcel(19, 29, 4, ps077)
        Case "19.公共管理和社会组织"
            Call fillExcel(17, 31, 4, ps07456)
            '政工人员 30
            Call fillExcel(18, 30, 4, ps07456)
            Call fillExcel(19, 30, 4, ps077)
        Case "20.国际组织"
            Call fillExcel(17, 32, 4, ps07456)
    End Select
End Sub


Function GetPs08Line(ps08line As Integer, ps08sheetIndex As Integer)
    Select Case industry
        Case "1.农林牧渔业"
            ps08line = 35
            ps08sheetIndex = 20
        Case "2.采矿业"
            ps08line = 29
            ps08sheetIndex = 20
        Case "3.制造业"
            ps08line = 20
            ps08sheetIndex = 20
        Case "4.电力、煤气及水的生产和供应业"
            ps08line = 42
            ps08sheetIndex = 22
        Case "5.建筑业"
            ps08line = 20
            ps08sheetIndex = 31
        Case "6.批发和零售业"
            ps08line = 36
            ps08sheetIndex = 24
        Case "7.交通运输、仓储和邮政业"
            ps08line = 21
            ps08sheetIndex = 20
        Case "8.住宿和餐饮业"
            ps08line = 36
            ps08sheetIndex = 24
        Case "9.信息传输、计算机服务和软件业"
            ps08line = 20
            ps08sheetIndex = 20
        Case "10.金融业"
            ps08line = 36
            ps08sheetIndex = 24
        Case "11.房地产业"
            ps08line = 36
            ps08sheetIndex = 24
        Case "12.租赁和商务服务业"
            ps08line = 36
            ps08sheetIndex = 24
        Case "13.科学研究、技术服务和地质勘查"
            ps08line = 19
            ps08sheetIndex = 20
        Case "14.水利、环境和公共设施管理业"
            ps08line = 34
            ps08sheetIndex = 20
        Case "15.居民服务和其他服务业"
        Case "16.教育"
            ps08line = 18
            ps08sheetIndex = 20
        Case "17.卫生、社会保障和社会福利业"
            ps08line = 38
            ps08sheetIndex = 20
        Case "18.文化体育和娱乐业"
            ps08line = 23
            ps08sheetIndex = 22
        Case "19.公共管理和社会组织"
            ps08line = 36
            ps08sheetIndex = 24
        Case "20.国际组织"
            ps08line = 36
            ps08sheetIndex = 24
        Case Else
            ps08line = 36
            ps08sheetIndex = 24
        End Select
End Function

Sub ps08Main()
    Dim ps08(55) As Integer
    Call FillPs071_074Array(ps08)
    Dim pageindex As Integer
    Dim line As Integer

    Dim tmp1 As Integer
    Dim tmp2 As Integer
    tmp1 = ps08(39)
    tmp2 = ps08(40)
    For i = 40 To 36 Step -1
        ps08(i) = ps08(i - 2)
    Next
    ps08(34) = tmp1
    ps08(35) = tmp2

    'ps08:20 ps08.1:21
    For i = 0 To 25
        Sheet20.Cells(13, i + 4) = ps08(i)
    Next
    For i = 26 To 54
        Sheet21.Cells(13, i - 22) = ps08(i)
    Next

    Select Case system
        Case "1.党的系统 "
            For i = 0 To 25
                Sheet20.Cells(14, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(14, i - 22) = ps08(i)
            Next
        Case "2.行政系统"
            Call GetPs08Line(line, pageindex)

            Dim sht
            Set sht = ThisWorkbook.Sheets(pageindex)
            For i = 0 To 25
                sht.Cells(line, 4 + i) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(line, i - 22) = ps08(i)
            Next

            'ps8.4:24 ps8.5:25
        Case "3.人大"
            For i = 0 To 25
                Sheet20.Cells(37, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(37, i - 22) = ps08(i)
            Next
        Case "4.政协"
            For i = 0 To 25
                Sheet20.Cells(38, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(38, i - 22) = ps08(i)
            Next
        Case "5.法院"
            For i = 0 To 25
                Sheet20.Cells(39, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(39, i - 22) = ps08(i)
            Next
        Case "6.检察院"
            For i = 0 To 25
                Sheet20.Cells(40, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(40, i - 22) = ps08(i)
            Next
        Case "7.群众团体"
            For i = 0 To 25
                Sheet20.Cells(42, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(42, i - 22) = ps08(i)
            Next
        Case "8.民主党派"
            For i = 0 To 25
                Sheet20.Cells(41, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(41, i - 22) = ps08(i)
            Next
        Case "9.军队"
            For i = 0 To 25
                Sheet20.Cells(43, i + 4) = ps08(i)
            Next
            For i = 26 To 54
                Sheet21.Cells(43, i - 22) = ps08(i)
            Next
    End Select
End Sub

Sub Mian()
system = Sheet1.Cells(3, 1)
industry = Sheet1.Cells(3, 6)
CityLevel = Sheet1.Cells(3, 5)
ps0Main
ps01Main
ps02Main
ps03Main
ps04Main
ps05Main
ps074Main
ps07456Main
ps08Main
'ps09特殊人才不能求出
MsgBox "已成功生成"
End Sub
