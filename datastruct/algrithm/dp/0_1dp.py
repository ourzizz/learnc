# -*- coding: utf-8 -*-
#01背包求解，c开发太累赘，理解算法的基础上还要耗费很大精力debug，考量指针下标这些蛋疼的东西
def Knapsack(w,v,n,W,c):
    for m in range(W):
        c[0][m]=0
    for i in range(n):
        c[i][0]=0
    for i in range(1,n):
        for m in range(1,W+1):
            if m >= w[i]: #最起码要够装第i个物品 所以m>=w[i]
                if c[i-1][m-w[i]]+v[i] > c[i-1][m]: #放入第i个物品后的（其中肯定是要剔除第i个物品的重量时背包的最优解）与不放人第i个物品进行比较取较大的填入c[i][m]
                    c[i][m] = c[i-1][m-w[i]]+v[i]
                else:
                    c[i][m] = c[i-1][m]
            else:
                c[i][m]=c[i-1][m]

def Print(w,v,n,W,c):
    m=W
    i=n
    while i>=1:
        if c[i][m]==c[i-1][m-w[i]]+v[i]:
            print i,
            m=m-w[i]
            i=i-1
        else:
            i=i-1

if __name__ == "__main__":
    w=(0,1,2,3,4,12)
    v=(0,6,8,3,7,20)
    W=20
    C=[([0]*(W+1)) for i in range(len(w))]
    Knapsack(w,v,len(w),W,C)
    for i in range(len(C)):
        print C[i]
    print w
    print v
    Print(w,v,len(w)-1,W,C)
