Dim bmddic
Function Initbmddc() '建立字典
    Dim i%
    i = 3
    Set bmddic = CreateObject("scripting.dictionary")
    While Sheet1.Cells(i, 5) <> ""
        bmddic.Add Sheet1.Cells(i, 5).Value, Sheet1.Cells(i, 3).Value
        i = i + 1
    Wend
End Function
'------------------------------------------------------------生成拼音首字母---------------
Function getpychar(char)
    tmp = 65536 + Asc(char)
    If (tmp >= 45217 And tmp <= 45252) Then
        getpychar = "A"
    ElseIf (tmp >= 45253 And tmp <= 45760) Then
        getpychar = "B"
    ElseIf (tmp >= 45761 And tmp <= 46317) Then
        getpychar = "C"
    ElseIf (tmp >= 46318 And tmp <= 46825) Then
        getpychar = "D"
    ElseIf (tmp >= 46826 And tmp <= 47009) Then
        getpychar = "E"
    ElseIf (tmp >= 47010 And tmp <= 47296) Then
        getpychar = "F"
    ElseIf (tmp >= 47297 And tmp <= 47613) Then
        getpychar = "G"
    ElseIf (tmp >= 47614 And tmp <= 48118) Then
        getpychar = "H"
    ElseIf (tmp >= 48119 And tmp <= 49061) Then
        getpychar = "J"
    ElseIf (tmp >= 49062 And tmp <= 49323) Then
        getpychar = "K"
    ElseIf (tmp >= 49324 And tmp <= 49895) Then
        getpychar = "L"
    ElseIf (tmp >= 49896 And tmp <= 50370) Then
        getpychar = "M"
    ElseIf (tmp >= 50371 And tmp <= 50613) Then
        getpychar = "N"
    ElseIf (tmp >= 50614 And tmp <= 50621) Then
        getpychar = "O"
    ElseIf (tmp >= 50622 And tmp <= 50905) Then
        getpychar = "P"
    ElseIf (tmp >= 50906 And tmp <= 51386) Then
        getpychar = "Q"
    ElseIf (tmp >= 51387 And tmp <= 51445) Then
        getpychar = "R"
    ElseIf (tmp >= 51446 And tmp <= 52217) Then
        getpychar = "S"
    ElseIf (tmp >= 52218 And tmp <= 52697) Then
        getpychar = "T"
    ElseIf (tmp >= 52698 And tmp <= 52979) Then
        getpychar = "W"
    ElseIf (tmp >= 52980 And tmp <= 53688) Then
        getpychar = "X"
    ElseIf (tmp >= 53689 And tmp <= 54480) Then
        getpychar = "Y"
    ElseIf (tmp >= 54481 And tmp <= 62289) Then
        getpychar = "Z"
    Else '如果不是中文，则不处理
        getpychar = ""
    End If
End Function

Function getpy(str)
    For i = 1 To Len(str)
        getpy = getpy & getpychar(Mid(str, i, 1))
    Next i
End Function
'------------------------------------------------------------生成拼音首字母---------------

Function g_xianqu_code() '根据表1中的信息生成第一层目录，这层目录为各地区代码
    Dim i%, root$
    i = 1
    root = Sheet1.Cells(2, 2)
    While Sheet1.Cells(i + 2, 3) <> ""
        Sheet3.Cells(i, 1) = root
        Sheet3.Cells(i, 4) = root
        Sheet3.Cells(i, 2) = Sheet1.Cells(i + 2, 3)
        Sheet3.Cells(i, 3) = Sheet1.Cells(i + 2, 4)
        Sheet3.Cells(i, 5) = 1

        Sheet3.Cells(i, 6) = getpy(Sheet1.Cells(i + 2, 4))
        Sheet3.Cells(i, 7) = "NULL"
        Sheet3.Cells(i, 8) = 0
        Sheet3.Cells(i, 9) = 0
        Sheet3.Cells(i, 10) = Sheet3.Cells(i, 3)
        i = i + 1
    Wend
    g_xianqu_code = i '返回表三中最新行的位置方便下一次填入正确的位置
End Function

Function g_dw_code(sheet3_index As Integer)
    Dim i%, dw_code$
    i = 2
    While Sheet2.Cells(i, 1) <> ""
        If Sheet2.Cells(i, 1) <> Sheet2.Cells(i - 1, 1) Then
            dw_code = bmddic(Sheet2.Cells(i, 5).Value) & Sheet2.Cells(i, 1)
            Sheet3.Cells(sheet3_index, 1) = Sheet1.Cells(2, 2)
            Sheet3.Cells(sheet3_index, 4) = bmddic(Sheet2.Cells(i, 5).Value) '上级目录代码
            Sheet3.Cells(sheet3_index, 2) = dw_code '自身代码
            Sheet3.Cells(sheet3_index, 3) = Sheet2.Cells(i, 1) & Sheet2.Cells(i, 2) '名称
            Sheet3.Cells(sheet3_index, 5) = 1 '是否有下级目录
            Sheet3.Cells(sheet3_index, 6) = getpy(Sheet2.Cells(i, 2))
            Sheet3.Cells(sheet3_index, 7) = "NULL"
            Sheet3.Cells(sheet3_index, 8) = 0
            Sheet3.Cells(sheet3_index, 9) = 0
            Sheet3.Cells(sheet3_index, 10) = Sheet3.Cells(sheet3_index, 3)
            sheet3_index = sheet3_index + 1
        End If
        Sheet3.Cells(sheet3_index, 1) = Sheet1.Cells(2, 2)
        Sheet3.Cells(sheet3_index, 4) = dw_code '上级目录代码
        Sheet3.Cells(sheet3_index, 2) = dw_code & Sheet2.Cells(i, 4) '自身代码
        Sheet3.Cells(sheet3_index, 3) = Sheet2.Cells(i, 4) & Sheet2.Cells(i, 3) '名称
        Sheet3.Cells(sheet3_index, 5) = 0 '是否有下级目录
        Sheet3.Cells(sheet3_index, 6) = getpy(Sheet2.Cells(i, 3))
        Sheet3.Cells(sheet3_index, 7) = "NULL"
        Sheet3.Cells(sheet3_index, 8) = 0
        Sheet3.Cells(sheet3_index, 9) = 0
        Sheet3.Cells(sheet3_index, 10) = Sheet3.Cells(sheet3_index, 3)
        sheet3_index = sheet3_index + 1
        i = i + 1
    Wend
End Function

Sub main()
    Dim index%
    Call Initbmddc
    index = g_xianqu_code()
    Call g_dw_code(index)
End Sub

