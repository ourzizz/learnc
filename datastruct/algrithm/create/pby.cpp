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
{//事实证明这个函数不正确
    int i;
    int post=0;
    for (i = 0; i < V->len; ++i) {
        if (V->xuqiu[i].status < 0 ) {
            if (V->xuqiu[i].count <= value) {
                post = i;
            }
        }
    }
    return post;
}

int MinSuf(pingbiyi *laiyuan,int value)
{

    int i;
    int tmp;
    i = Locate(laiyuan,value);
    laiyuan->xuqiu[i].status = value;
    tmp = value-laiyuan->xuqiu[i].count;
    if (tmp  == 0) {
        return 0;
    }
    else{
        MinSuf(laiyuan,tmp);
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
    G->xuqiu[6].count=9; G->xuqiu[6].status=-1;
    G->xuqiu[7].count=11; G->xuqiu[7].status=-1;
    G->xuqiu[8].count=12; G->xuqiu[8].status=-1;
    G->xuqiu[9].count=14; G->xuqiu[9].status=-1;
}
int main(void)
{
    int i;
    pingbiyi laiyuan;
    create_xuqiuarray(&laiyuan);
    MinSuf(&laiyuan,44);
    for (i = 0; i < 10; ++i) {
        if (laiyuan.xuqiu[i].status >=0 ) {
            std::cout << laiyuan.xuqiu[i].count << std::endl;
        }
    }
    return 0;
}
