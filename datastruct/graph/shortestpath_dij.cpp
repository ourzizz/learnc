/*************************************************************
 * 这是第二次研究迪杰斯特拉算法了，这个算法不是很好懂的
 * 看严蔚敏版本的自然语言夹杂数学语言的描述是无法看懂的
 * 我的方法是先大体知道算法的意思，如集合S和集合V-S
 * 其实就是求在两个集合之间所有连线最短的那条,然后把在连线V-S那一端的
 * 结点拉过来放入S中，再重新寻找一次最短的那条路径，直到所有
 * 点都被拉入集合S
************************************************************ */
#include <iostream>
#include <string>
#define  INFINITY 65536
#define  MAX  6

using namespace std;

struct pathsort
{
    bool status;
    int distance;
    int no;
    char * vexname;
};
typedef struct MGraph
{
    char*  vex[MAX];
    int arcs[MAX][MAX];
    int vexnum;
}MGraph;

MGraph create_example_lgraph()
{//手动输入生成图的矩阵数组，反正只是为了能把算法运行起来，够调试所用即可
    MGraph G;
    G.vexnum= 6;
    G.vex[0]="v0";
    G.vex[1]="v1";
    G.vex[2]="v2";
    G.vex[3]="v3";
    G.vex[4]="v4";
    G.vex[5]="v5";
    int arcs[6][6]={
        { INFINITY,INFINITY,10,INFINITY,30,100 },
        { INFINITY,INFINITY,5,INFINITY,INFINITY,INFINITY },
        { INFINITY,INFINITY,INFINITY,50,INFINITY,INFINITY },
        { INFINITY,INFINITY,INFINITY,INFINITY,INFINITY,10 },
        { INFINITY,INFINITY,INFINITY,20,INFINITY,60 },
        { INFINITY,INFINITY,INFINITY,INFINITY,INFINITY,INFINITY }
    };
    for (int i = 0; i < G.vexnum; ++i) {
        for (int j = 0; j < G.vexnum; ++j) {
            G.arcs[i][j] = arcs[i][j];
        }
    }
    return G;
}

int sortarry(pathsort a[],int size)
{
    int i,j,last;
    i = size - 1;
    pathsort tmp;
    while(i>0) {
        last = 0;
        for (j = 0; j < i; ++j) {
            if (a[j].distance > a[j+1].distance) {
                tmp = a[j];
                a[j] = a[j+1];
                a[j+1] = tmp;
                last = j;
            }
        }
        i = last;
    }
    i=0;
    return 1;

}
void printpath(MGraph G,int p[][6],int D[])
{
    //路径输出有拓扑次序的，这个次序其实就是按照距离的远近来排列结点，借助辅助结构pathsort，现将
    //数组按照距离由近及远进行排序，输出路径时只需要将辅助数组中的状态位置为1然后挨个输出即可得到
    //排列后的路径
    pathsort ps[6];
    for (int j = 0; j < G.vexnum; ++j) {
        ps[j].distance = D[j];
        ps[j].no = j;
        ps[j].status = 0;
        ps[j].vexname=G.vex[j];
    }
    sortarry(ps,6); //按照路径长度将数组ps进行排序

    for (int i = 0; i < G.vexnum; ++i) {
        std::cout << "从v0到"<< G.vex[i] <<"的距离为"<<D[i]<<"路径为：";
        for (int j = 0; j < G.vexnum; ++j) {
            ps[j].status = 0;
        }//将状态全部置为false

        for (int j = 0; j < G.vexnum; ++j) {
            if (p[i][j]!=0 ) { //表示路径存在
                for (int n = 0; n < G.vexnum; ++n) {
                    if (ps[n].no==j) { //若到j结点有路径，那么在ps数组中将j结点所在域的状态置为true
                        ps[n].status=1;
                    }
                }
            }
        }
        for (int n = 0; n < G.vexnum; ++n) {
            if (ps[n].status) { //如果状态为true才输出结点
                std::cout << ps[n].vexname<<"->" ;
            }
        }
        cout<<endl;
    }
}

void ShortestPath_DIJ(MGraph G,int v0,int P[][MAX],int D[])
{
    int final[MAX];
    for (int i = 0; i < G.vexnum; ++i) {
        D[i] = G.arcs[v0][i]; final[i] = false;
        for (int j = 0; j < G.vexnum; ++j) {
            P[i][j] = false;
        }
        if (D[i]<INFINITY) 
        {
            P[i][v0] = true; P[i][i] = true;
        }
    }
    D[v0] = 0;
    final[v0]=true;

    int min;
    int w;
    for (int v = 0; v < G.vexnum; ++v) { //这里的循环条件应该为final数组中全为ture.其实按代码中的循环每次循环加入一个节点这个循环条件也可以用,循环结束时所有结点全部进入S集合
        min = INFINITY; //设置min的好处在于遍历D数组的过程中自动就得到了最小值min和离v0最近的顶点
        for (int j = 1; j < G.vexnum; ++j) {
            if (!final[j] && D[j]<min) { //V-S中离v0最近的顶点 w
                min = D[j];
                w = j;
            }
        }
        final[w]=true; //将最近的w顶点加入到S集合中

        //下面的代码功能为用新进入S集合的w点来将所有V-S中结点离v0的距离进行更新，
        //只更新不在S中的点，因为S中的点距都小于到w的距离
        //只有w到下一个结点的距离加上D[w] 时表示找到一条更近的路，才需要更新
        for (int i = 0; i < G.vexnum; ++i) {
            if ((min+G.arcs[w][i] < D[i]) && !final[i]) {
                D[i] = min+G.arcs[w][i]; //更新路径
                for (int n = 0; n < G.vexnum; ++n) 
                    P[i][n]=P[w][n]; //路径其实缺少了顺序，虽然指导要经过那些点。我们可以借助D[]数组排序
                P[i][i] = true;
            }
        }
    }
}


int main(void)
{
    MGraph G = create_example_lgraph();
    int p[6][6];
    int D[6];
    ShortestPath_DIJ(G,0,p,D);
    printpath(G,p,D);
    return 0;
}
