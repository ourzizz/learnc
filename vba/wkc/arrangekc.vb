Type wkc
status As Integer
kemu As String
renshu As Integer
jiaoshi As Integer
ksno As Integer
End Type

Type jiaoshi
post As Integer
banji As String
renshu As Integer
ksno As Integer
my_wkc(20) As wkc
End Type

Public Fix_kh As Long
Public my_jiaoshi(50) As jiaoshi
Public my_wkc(50) As wkc

Public Fix_kh As Long
Public my_jiaoshi(50) As jiaoshi
Public my_wkc(50) As wkc
Function locate_kemu(kemu As String) '为统计考场人数降低查找时间，直接定位,一般挨着的几个人都是同一个科目，没有必要每次遍历科目位置
    Dim i%
    i = 13
    While Sheet2.Cells(i, 2) <> ""
        If Sheet2.Cells(i, 2) = kemu Then
            locate_kemu = i
            Exit Function
        End If
        i = i + 1
    Wend
        locate_kemu = 0
End Function

Sub TongJiKaoChang() '统计各个科目有多少人报考
    Dim sheet1_row%, sheet2_row%, index%, pointer%
    sheet1_row = 3
    sheet2_row = 12
    pointer = 13
    Sheets(2).Range("b13:I100").ClearContents  '清除内容
    While Sheet1.Cells(sheet1_row, 12) <> ""
        If Sheet1.Cells(sheet1_row, 12) = Sheet1.Cells(sheet1_row - 1, 12) Then
            Sheet2.Cells(pointer, 3) = Sheet2.Cells(pointer, 3) + 1
        Else
            pointer = locate_kemu(Sheet1.Cells(sheet1_row, 12))
            If pointer = 0 Then '定位失败重启一行
                sheet2_row = sheet2_row + 1
                pointer = sheet2_row
            End If
            Sheet2.Cells(pointer, 2) = Sheet1.Cells(sheet1_row, 12)
            Sheet2.Cells(pointer, 3) = Sheet2.Cells(pointer, 3) + 1
        End If
        sheet1_row = sheet1_row + 1
    Wend
End Sub

Sub Div_Kc() '划分考场，得到每个科目的报考人数后，按照每考场最多30人来进行划分
    Dim row%, divrow%, tmp%
    row = 13
    divrow = 13
    While Sheet2.Cells(row, 3) <> ""
        If Sheet2.Cells(row, 3) <= 30 Then
            Sheet2.Cells(divrow, 4) = Sheet2.Cells(row, 2)
            Sheet2.Cells(divrow, 5) = Sheet2.Cells(row, 3)
            divrow = divrow + 1
        Else
            tmp = Sheet2.Cells(row, 3)
            While tmp > 30
                Sheet2.Cells(divrow, 4) = Sheet2.Cells(row, 2)
                Sheet2.Cells(divrow, 5) = 30
                tmp = tmp - 30
                divrow = divrow + 1
            Wend
            If tmp <> 0 Then
                Sheet2.Cells(divrow, 4) = Sheet2.Cells(row, 2)
                Sheet2.Cells(divrow, 5) = tmp
                divrow = divrow + 1
            End If
        End If
        row = row + 1
        tmp = 0
    Wend
    Range("d13:e65536").Select
    Selection.Sort key1:=Range("e12"), Order1:=xlDescending, Header:=xlNo, Orientation:=xlTopToBottom
End Sub

Sub wkc_sort(my_wkc() As wkc) '此函数不能单独使用，仅仅作为初始化尾考场数组后将不满30人的考场进行大小组合，避免出现小科目全部安排在一个考场中的情况
    Dim tmp_kc As wkc, i%, divpoint%, array_size%
    divpoint = 1
    array_size = 1
    While my_wkc(array_size).kemu <> ""
        If my_wkc(divpoint).renshu = 30 Then
            divpoint = divpoint + 1
        End If
        array_size = array_size + 1
    Wend
    divpoint = divpoint + 1
    array_size = array_size - 1
    i = divpoint
    While i <= (Int(array_size + divpoint) / 2)
        If (i Mod 2) = 0 Then
            tmp_kc = my_wkc(i)
            my_wkc(i) = my_wkc(array_size + divpoint - i)
            my_wkc(array_size + divpoint - i) = tmp_kc
        End If
        i = i + 1
    Wend
End Sub

Sub Init_jiaoshi_array(my_jiaoshi() As jiaoshi)
    For i = 1 To UBound(my_jiaoshi)
        my_jiaoshi(i).post = 1
        my_jiaoshi(i).banji = ""
        my_jiaoshi(i).renshu = 30
        my_jiaoshi(i).ksno = 1
        For j = 1 To 10
            my_jiaoshi(i).my_wkc(j).kemu = ""
            my_jiaoshi(i).my_wkc(j).renshu = 0
            my_jiaoshi(i).my_wkc(j).jiaoshi = 0
        Next
    Next
End Sub

Sub Init_wkc_array(my_wkc() As wkc)
    Dim i%, j%
    i = 13
    j = 1
    While Sheet2.Cells(i, 5) <> ""
        my_wkc(j).kemu = ""
        my_wkc(j).renshu = 0
        my_wkc(j).status = 0
        my_wkc(j).ksno = 1
        i = i + 1
        j = j + 1
    Wend
    Call wkc_sort(my_wkc)
End Sub

Sub Fill_wkc_array(my_wkc() As wkc)
    '将Excel中考场划分得到的数据初始化wkc数组
    Dim i%, j%
    i = 13
    j = 1
    While Sheet2.Cells(i, 5) <> ""
        my_wkc(j).kemu = Sheet2.Cells(i, 4)
        my_wkc(j).renshu = Sheet2.Cells(i, 5)
        my_wkc(j).status = 0
        my_wkc(j).ksno = 1
        i = i + 1
        j = j + 1
    Wend
    Call wkc_sort(my_wkc)
End Sub

Sub Auto_Combine_kc(my_wkc() As wkc)
    '合并考场算法，使用了最优适宜，遍历所有教室找出满足1能放入科目 2剩余座位最少的教室
    '将结构体将处在同一教室的科目结构体压入jiaoshi结构体的wkc数组中，方便输出遍历
    Dim Min_cost_jiaoshi%, Min_cost%, i%, j%, my_jiaoshi(30) As Integer
    i = 1
    j = 1
    For k = 1 To UBound(my_jiaoshi)
        my_jiaoshi(k) = 30
    Next

    While my_wkc(i).kemu <> ""
        Min_cost = 30
        For j = 1 To UBound(my_jiaoshi)
            If my_jiaoshi(j) - my_wkc(i).renshu < Min_cost And my_jiaoshi(j) - my_wkc(i).renshu >= 0 Then
                Min_cost = my_jiaoshi(j) - my_wkc(i).renshu
                Min_cost_jiaoshi = j
            End If
        Next
        my_wkc(i).jiaoshi = Min_cost_jiaoshi
        my_jiaoshi(Min_cost_jiaoshi) = my_jiaoshi(Min_cost_jiaoshi) - my_wkc(i).renshu
        i = i + 1
    Wend
End Sub

Sub Manu_Combine_kc(my_wkc() As wkc)
    Range("f13:i65536").Select
    Selection.Sort key1:=Range("h13"), Order1:=xlAscending, Header:=xlNo, Orientation:=xlTopToBottom
    Dim i%, j%, k%
    i = 13
    j = 1
    k = 1
    While Sheet2.Cells(i, 6) <> ""
        my_wkc(j).kemu = Sheet2.Cells(i, 6)
        my_wkc(j).renshu = Sheet2.Cells(i, 7)
        my_wkc(j).jiaoshi = Sheet2.Cells(i, 8)
        i = i + 1
        j = j + 1
    Wend
End Sub

Sub Fill_jiaoshi_array(my_wkc() As wkc, my_jiaoshi() As jiaoshi)
    Dim i%
    i = 1
    While my_wkc(i).kemu <> ""
        my_jiaoshi(my_wkc(i).jiaoshi).renshu = my_jiaoshi(my_wkc(i).jiaoshi).renshu - my_wkc(i).renshu
        my_jiaoshi(my_wkc(i).jiaoshi).my_wkc(my_jiaoshi(my_wkc(i).jiaoshi).post) = my_wkc(i)
        my_jiaoshi(my_wkc(i).jiaoshi).post = my_jiaoshi(my_wkc(i).jiaoshi).post + 1
        
        If my_jiaoshi(my_wkc(i).jiaoshi).renshu < 0 Then
            MsgBox ("再想想！第" & my_wkc(i).jiaoshi & "考场已经超过30个人了")
        End If
        i = i + 1
    Wend
End Sub

Sub fill_excel(my_jiaoshi() As jiaoshi) '遍历jiaoshi数组输出到sheet2
    Dim i%, j%, k%
    j = 13
    i = 1
    k = 1
    While my_jiaoshi(i).renshu < 30
        Sheet2.Cells(j, 9) = 30 - my_jiaoshi(i).renshu
        While k < my_jiaoshi(i).post
            Sheet2.Cells(j, 6) = my_jiaoshi(i).my_wkc(k).kemu
            Sheet2.Cells(j, 7) = my_jiaoshi(i).my_wkc(k).renshu
            Sheet2.Cells(j, 8) = my_jiaoshi(i).my_wkc(k).jiaoshi
            k = k + 1
            j = j + 1
        Wend
        k = 1
        i = i + 1
    Wend
End Sub

Sub arrange_kh()
    '将每个考生依次放入考场
    Call Manu_main
    Fix_kh = Sheet2.Cells(8, 3)
    Dim i%, k%, kaochang$, zuowei$
    i = Sheet1.Range("e65536").End(xlUp).row

    For j = 3 To i
        k = 1
        Do While my_wkc(k).kemu <> ""
            If (my_wkc(k).kemu = Sheet1.Cells(j, 12)) And (my_wkc(k).ksno <= my_wkc(k).renshu) Then
                Sheet1.Cells(j, 2) = my_wkc(k).jiaoshi
                Sheet1.Cells(j, 3) = my_jiaoshi(my_wkc(k).jiaoshi).ksno
                '存在的bug jiaoshi的人数做文章
                my_wkc(k).ksno = my_wkc(k).ksno + 1
                my_jiaoshi(my_wkc(k).jiaoshi).ksno = my_jiaoshi(my_wkc(k).jiaoshi).ksno + 1
                Exit Do
            End If
            k = k + 1
        Loop

        If Sheet1.Cells(j, 2) < 10 Then
            kaochang = "0" & Sheet1.Cells(j, 2)
        Else
            kaochang = Sheet1.Cells(j, 2)
        End If
        If Sheet1.Cells(j, 3) < 10 Then
            zuowei = "0" & Sheet1.Cells(j, 3)
        Else
            zuowei = Sheet1.Cells(j, 3)
        End If
        Sheet1.Cells(j, 11) = "201701" & kaochang & zuowei
    Next
End Sub

Sub Auto_main()
    Call TongJiKaoChang
    Call Div_Kc
    Call Init_jiaoshi_array(my_jiaoshi)
    Call Init_wkc_array(my_wkc)
    Call Fill_wkc_array(my_wkc)
    Call Auto_Combine_kc(my_wkc)
    Call Fill_jiaoshi_array(my_wkc, my_jiaoshi)
    Call fill_excel(my_jiaoshi)
End Sub

Sub Manu_main()
    Call Init_jiaoshi_array(my_jiaoshi)
    Call Init_wkc_array(my_wkc)
    Call Manu_Combine_kc(my_wkc) '将手工合并的考场保存在wkc数组中
    Call Fill_jiaoshi_array(my_wkc, my_jiaoshi)
    Call fill_excel(my_jiaoshi)
End Sub
