# -*- coding: utf-8 -*-
def matrix_mutiply(m,s,n):
    for l in range(2,n+1):
        for i in range(1,n-l+1+1):
            j=i+l-1
            m[i][j] = 65536
            for k in range(i,j-1+1):
                q=m[i][k]+m[k+1][j] + matrix[i][0]*matrix[k][1]*matrix[j][1]
                if  q < m[i][j]:
                    m[i][j] = q
                    s[i][j] = k

def Print_optimal_parents(s,i,j):
    if i==j:
        print "A"+str(i),
    else :
        print "(",
        Print_optimal_parents(s,i,s[i][j])
        Print_optimal_parents(s,s[i][j]+1,j)
        print ")",

if __name__ == '__main__':
    m=( [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0])
    s=( [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0],
     [0,0,0,0,0,0,0])
    matrix=( 
            [0,0],
            [30,35],
            [35,15],
            [15,5],
            [5,10],
            [10,20],
            [20,25]
    )
    matrixList=( "A1", "A2", "A3", "A4", "A5", "A6")
    n=6
    matrix_mutiply(m,s,6)
    print matrix
    print "乘法标量次数求解最小代价矩阵"
    print m[1]
    print m[2]
    print m[3]
    print m[4]
    print m[5]
    print "分割点矩阵"
    print s[1]
    print s[2]
    print s[3]
    print s[4]
    print s[5]
    print matrixList
    print "完全括号化后的矩阵链"
    Print_optimal_parents(s,1,6)
