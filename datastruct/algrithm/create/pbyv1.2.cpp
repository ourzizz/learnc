/*
 * 必须养成谢注释的习惯
 * 这个程序解决的问题来源于，考试中心借屏蔽仪问题。问题是这样的，假如abcde五
 * 个学校分别有屏蔽仪1 3 4 9 11 20个，现有fghi四个学校分别需要屏蔽仪
 * 4 8 10 13 20个，如何将供需关系进行匹配，要做到需求方对提供方是1对多的关系
 * 第一步是将供需相等的组合去掉，
 * 第二步是将供方之和为需方所需的组合去掉
 * 第三部剩下的就是求和不等的，可采取1将供方一个大单元拆开来分给多个需方
 * 需方找最临近既比需求大
 * 具体算法用了递归，每次贪心选最临近的一个值再继续递归
 * 如果找不到和相等的，就往post所在为止的右边最临近的一个数找来搭配。
 * 虽然把算法写了，可是实际情况更复杂，供方组合之和等于需方的就那么几个，大多都是
 * 多几个少几个，作为练习到此为止，继续学习动态规划
 * */
#include <iostream>
#include <malloc.h>

using namespace std;
#define MAX 20
typedef struct
{
    int count;
    int status;
}cell;
typedef struct{
    int len;
    cell xuqiu[MAX];
}pingbiyi;

int Locate(pingbiyi *V,int value)
{
    int i;
    int post=-1;
    for (i = 0; i < V->len; ++i) {
        if (V->xuqiu[i].status < 0 ) {
            if (V->xuqiu[i].count <= value) {
                post = i;
            }
        }
    }
    return post;
}

int MinSuf(pingbiyi *laiyuan,int value,int status)
{
    int i;
    int tmp;
    i = Locate(laiyuan,value);
    if (i<0) {//表示value在供方数组中没有组合之和等于他，那么就不应该继续运行，继而退栈，在第57行进行重置供方数组
        return -1;
    }
    laiyuan->xuqiu[i].status = status;
    tmp = value-laiyuan->xuqiu[i].count;
    if (tmp  == 0) {
        return 0;
    }
    else{
        if(MinSuf(laiyuan,tmp,status) == -1)
        {
            laiyuan->xuqiu[i].status = -1;//退栈的时候先把状态位重置，否则就会显示被使用
            return -1;
        }
    }
    return 0;
}
void  create_xuqiuarray(pingbiyi *G)
{
    G->len=10;
    G->xuqiu[0].count=1; G->xuqiu[0].status=-1;
    G->xuqiu[1].count=3; G->xuqiu[1].status=-1;
    G->xuqiu[2].count=4; G->xuqiu[2].status=-1;
    G->xuqiu[3].count=5; G->xuqiu[3].status=-1;
    G->xuqiu[4].count=7; G->xuqiu[4].status=-1;
    G->xuqiu[5].count=8; G->xuqiu[5].status=-1;
    G->xuqiu[6].count=10; G->xuqiu[6].status=-1;
    G->xuqiu[7].count=11; G->xuqiu[7].status=-1;
    G->xuqiu[8].count=12; G->xuqiu[8].status=-1;
    G->xuqiu[9].count=14; G->xuqiu[9].status=-1;
}
void printresult(pingbiyi laiyuan,int value)
{
    int i;
    std::cout << value<<"=" ;
    for (i = 0; i < 10; ++i) {
        if (laiyuan.xuqiu[i].status ==value ) {
            std::cout << laiyuan.xuqiu[i].count<<"+" ;
        }
    }
    cout<< std::endl;
}
int main(void)
{
    pingbiyi laiyuan;
    create_xuqiuarray(&laiyuan);
    MinSuf(&laiyuan,100,100);
    MinSuf(&laiyuan,22,22);
    MinSuf(&laiyuan,9,9);
    printresult(laiyuan,100);
    printresult(laiyuan,22);
    printresult(laiyuan,9);
    return 0;
}
