# -*- coding: utf-8 -*-
#两个数组C和B分别记录LCS的长度和前驱序列
def LCS(X,Y,C,B):
    m=len(X)
    n=len(Y)
    for i in range(0,m+1):
        C[i][0] = 0
    for j in range(0,n+1):
        C[0][j] = 0
    for i in range(1,m):#最关键的是理解末尾两个字符如果不等的情况如何规划子问题
        for j in range(1,n):
            if X[i]==Y[j]:
                C[i][j]=C[i-1][j-1] + 1
                B[i][j]="V"
            elif C[i-1][j]>=C[i][j-1]:
                C[i][j] = C[i-1][j]
                B[i][j] = "U"
            else:
                C[i][j] = C[i][j-1]
                B[i][j] = "L"

def PRINT_LCS(b,X,i,j):
    #import pdb; pdb.set_trace()
    if i==0 or j==0:
        return 
    if b[i][j]=='V':
        PRINT_LCS(b,X,i-1,j-1)
        print x[i],
    elif b[i][j]=='U':
        PRINT_LCS(b,X,i-1,j)
    else:
        PRINT_LCS(b,X,i,j-1)

if __name__ == "__main__":
    x="aABCBDAB"
    y="aBDCABAB"
    c=[([0]*9) for i in range(9)]
    b=[([0]*9) for i in range(9)]
    LCS(x,y,c,b)
    for i in range(8):
        print c[i]
    print ""
    for i in range(8):
        print b[i]
    print x
    print y
    print "最长公共子序列为"
    PRINT_LCS(b,x,len(x)-1,len(y)-1)
