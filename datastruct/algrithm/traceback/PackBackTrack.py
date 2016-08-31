# -*- coding: utf-8 -*
def Bound(w,v,cp,cw,n,k,W):
    #将背包剩余部分c-w 全给装满，因为是做贪心选择，挨个装入就可以了，直到最后一个装不进去了怎么办，取出部分装进背包，计算出得出最大值
    #最大值只是个理论上限，这个上限都比其他解的小，那么就不可能得出最优答案
    b=cp
    c=cw
    for i in range(k+1,n):
        c=c+w[i]
        if c<W:
            b=b+v[i]
        else:
            b=round(b+(1.-float(c-W)/w[i])*v[i],2)
            return  b
    return b

def BKNAP(W,n,w,v,fw,fp,X):
    Y=[0 for x in range(n+1)]
    cw=cp=0
    k=0
    fp=-1
    while 1:
        #while k<=n and cw+w[k]<=W:
        while k<=(n-1) and cw+w[k]<=W:
            cw=cw+w[k]
            cp=cp+v[k]
            Y[k]=1
            k=k+1

        if k>n-1: #这是装满后，将得到的解保存在各个变量中,所有的解都在这里体现，直接输出就行了
            fp=cp
            fw=cw
            k=n
            X=Y
            print X,fp,fw
        else:
            Y[k]=0 #既然装不下，那么k当让是不放入的，就设置为0,再判断k值不值得扩展

        test=Bound(w,v,cp,cw,n,k,W)
        if k==(n-1) and test<=fp:
            print X,cp,cw

        while test<=fp:
            while k!=0 and Y[k]!=1:
                k=k-1
            if k==0:
                return 
            Y[k]=0
            cw=cw-w[k]
            cp=cp-v[k]
            test=Bound(w,v,cp,cw,n,k,W)
        k=k+1

if __name__ == "__main__":
    W=110
    v=[11,21,31,33,43,53,55,65]
    w=[1,11,21,23,33,43,45,55]
    n=len(v)
    X=[0 for x in range(n)]
    fp=0
    fw=0
    print '背包容量为',W
    print "待选物品重量以及价值"
    print w
    print v
    BKNAP(W,n,w,v,fw,fp,X)
