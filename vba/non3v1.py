#endcoding=utf-8
#面试要求，面试人员分为公务员、警察、选调生三种类型，选调生排秀山A警察不能跨天，每个考场容量maxrs人，职位不能再分割
#此脚本功能类似合并尾考场，可多次使用背包算法
#将每个考场尽最大量填充
#公务员、警察、选调生分别执行脚本,
#数据库表结构ksinfo:ksxm/zkzh/sfzh/bkdw/bkzw/leixing/minzu
#            zhiwei:dwmc/zwmc/leixing/rs/kcxh/msdate
#            mianshishi:kcxh/mssinfo/hksinfo
#zhiwei表中的数据是脚本根据基本考生数据，计算得出考场序号后一一填入数据库中
#执行脚本后SELECT ksinfo.*,zhiwei.kcxh FROM ksinfo,zhiwei where ksinfo.bkdw=zhiwei.dwmc and ksinfo.bkzw=zhiwei.zwmc order by kcxh;即可得出每个考生的考场分配
import sys      
reload(sys)    
sys.setdefaultencoding('utf8')  
import MySQLdb
#------------------------------------------------全局变量设置区域--------------------------------------------------------------------
maxrs=30 #每个考场设置人数
maxkcxh=40   #考场序号初始1
msdate=1
conn= MySQLdb.connect( host='192.168.1.107', port = 3306, user='root', passwd='123123', db ='mianshi',charset='utf8')
cur = conn.cursor()
#------------------------------------------------全局变量设置区域--------------------------------------------------------------------
class zhiwei(object):
    def __init__(self,dwmc,zwmc,leixing,rs):
        self.dwmc=dwmc
        self.zwmc=zwmc
        self.leixing=leixing
        self.rs=rs

def Initzhiwei(leixing,zhiweilist):
    #这里从数据库读取职位形成列表
    #zhiweilist=[] #清空列表
    zhiweilist.append(zhiwei('null','null','null',0))#插入第一条空数据，物品0不存在
    query="SELECT bkdw,bkzw,leixing,count(*) FROM ksinfo where leixing='%s' group by bkdw,bkzw ;" % (leixing)
    print query
    cur.execute(query)
    zw=cur.fetchall()
    for item in zw:
        zhiweilist.append(zhiwei(item[0],item[1],item[2],item[3]))

def arrange_kc(kcxh,zhiweilist):
    w=[]
    v=[]
    for item in zhiweilist:
        w.append(item.rs)
    while len(zhiweilist)!=0:#结束条件为职位列表为空，即所有职位均分配到考场中
        tmp=[]
        res=bag(w,w,len(w),maxrs)
        sol=show(maxrs,w,res)
        sum=0
        if len(sol)==0: #解数组中没有解就跳出循环
            break
        print u"------------------------第",kcxh,u"考场-----------------"
        #import pdb; pdb.set_trace()
        msdate=2
        thiskcxh=kcxh
        if kcxh>maxkcxh:
            thiskcxh=kcxh-maxkcxh
            msdate=1
        for item in sol:
            tmp.append(zhiweilist[item])
            query="insert into zhiwei (`dwmc`, `zwmc`, `leixing`, `rs`, `kcxh`,`msdate`) values('%s','%s','%s',%s,%s,%s)" % (zhiweilist[item].dwmc,zhiweilist[item].zwmc,zhiweilist[item].leixing,zhiweilist[item].rs,thiskcxh,msdate)
            cur.execute(query)
            print query
            sum=zhiweilist[item].rs+sum
        print u"共有",sum,u"人"
        for item in sol:
            del w[item]
            del zhiweilist[item]
        kcxh=kcxh+1
        conn.commit()
    return kcxh
#---------------------------------------------------------------------------------------------------------------------------
#核心算法01背包来做，懒得改进算法，当质量数组和价值数组为同一数组就是我要的结果
#务必注意质量数组中是从第一个物品开始计算的，第0个物品和正常人的思维有冲突，故数组第0单元不要填入数据。
#封装好就不要动了记得每个参数的意义，直接传值计算
def bag(v,w,n,W):#w:物品重量数组，n物品个数，W背包总容量，C价值数组,计算数组存储整个计算过程#{{{
    res=[([0]*(W+1)) for i in range(n)] #生成状态数组维度为横向背包容量，纵向为物品序号
    for m in range(W):                  #res[i][m]的意义：
        res[0][m]=0
    for i in range(1,n):
        res[i][0]=0
    for i in range(1,n):
        for m in range(1,W+1):
            if m >= w[i]: #至少能放入i物品，可能只有i或者多个物品
                res[i][m]=max(res[i-1][m-w[i]]+v[i],res[i-1][m]) #这里是计算价值，不是重量的
            else:
                res[i][m]=res[i-1][m]
    return res
def show(c,w,res): #c背包容量，w重量数组，res状态数组 
    sol=[]
    m=c
    i=len(w)-1
    while i>=1 and m>=1:
        if (m>=w[i]) and (res[i][m]==res[i-1][m-w[i]]+w[i]):
            m=m-w[i]
            sol.append(i)
        i=i-1
    return sol #sol数组存放了最优解的物品序号
#---------------------------------------------------------------------------------------------------------------------------
if __name__ == "__main__":
    leixinglist=['选调生','警察','公务员']#先排警察选调生再排公务员
    kcxh=1
    cur.execute("truncate table zhiwei")
    conn.commit()
    for leixing in leixinglist:
        zhiweilist=[] #复用数组每次都要记得清空
        Initzhiwei3_1(leixing,zhiweilist)
        kcxh=arrange_kc(kcxh,zhiweilist)
    conn.commit()
    conn.close()
