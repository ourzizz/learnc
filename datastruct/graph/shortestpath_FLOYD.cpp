/*
 * 这个算法的确比迪杰斯特拉难理解，花了3天才弄完，也并不是完全弄清，例如
 * 三层循环中间发生的过程就不是很清除，代码也是按照网上的现成代码撸了一遍
 * 动态规划自底向上，每个子问题的结果其实都存储在了D和p矩阵中，循环最外层的
 * k每次都作为cn2的中间媒介进行计算，当k从0取到n的时候相当于所有结点中间的
 * 路径把所有n-1个结点逐次加入到路径中测试一遍，最终得到的必然是两两结点之间的最短路径。
 * 说实话这个算法真的非常简洁优雅，漂亮
 * */
#include<iostream>
using namespace std;

#define MAXEDGE 20
#define MAXVEX 20
#define INFINITY 65535

typedef struct
{
    int vexs[MAXVEX];
    int arc[MAXVEX][MAXVEX];
    int numVertexes, numEdges;
} MGraph;

typedef int Patharc[MAXVEX][MAXVEX];
typedef int ShortPathTable[MAXVEX][MAXVEX];

/* 构建图 */
void CreateMGraph(MGraph *G)
{
    int i, j;

    /* printf("请输入边数和顶点数:"); */
    G->numEdges = 16;
    G->numVertexes = 9;

    for (i = 0; i < G->numVertexes; i++)/* 初始化图 */
    {
        G->vexs[i] = i;
    }

    for (i = 0; i < G->numVertexes; i++)/* 初始化图 */
    {
        for ( j = 0; j < G->numVertexes; j++)
        {
            if (i == j)
                G->arc[i][j] = 0;
            else
                G->arc[i][j] = G->arc[j][i] = INFINITY;
        }
    }

    G->arc[0][1] = 1;
    G->arc[0][2] = 5;
    G->arc[1][2] = 3;
    G->arc[1][3] = 7;
    G->arc[1][4] = 5;

    G->arc[2][4] = 1;
    G->arc[2][5] = 7;
    G->arc[3][4] = 2;
    G->arc[3][6] = 3;
    G->arc[4][5] = 3;

    G->arc[4][6] = 6;
    G->arc[4][7] = 9;
    G->arc[5][7] = 5;
    G->arc[6][7] = 2;
    G->arc[6][8] = 7;

    G->arc[7][8] = 4;


    for(i = 0; i < G->numVertexes; i++)
    {
        for(j = i; j < G->numVertexes; j++)
        {
            G->arc[j][i] = G->arc[i][j];
        }
    }

}
/* Floyd算法，求网图G中各顶点v到其余顶点w的最短路径P[v][w]及带权长度D[v][w]。 */
void ShortestPath_Floyd(MGraph MG, Patharc P, ShortPathTable D)
{
    int i,j,k;
    for (i = 0; i < MG.numVertexes; ++i) {
        for (j = 0; j < MG.numVertexes; ++j) {
            D[i][j]=MG.arc[i][j];
            P[i][j]=j;
        }
    }
    for (k = 0; k < MG.numVertexes; ++k) {
        for (i = 0; i < MG.numVertexes; ++i) {
            for (j = 0; j < MG.numVertexes; ++j) {
                if(D[i][j] > (D[i][k] + D[k][j]))
                {
                    D[i][j] = D[i][k] + D[k][j];
                    P[i][j] = P[i][k];
                }
            }
        }
    }
}

int main(void)
{
    int v, w, k;
    MGraph MG;
    Patharc P;
    ShortPathTable D;
    CreateMGraph(&MG);
    ShortestPath_Floyd(MG, P, D);

    cout << "D矩阵: " << endl;
    for (v = 0; v < MG.numVertexes; v++) {
        for (w = 0; w < MG.numVertexes; w++) {
            std::cout << D[v][w]<<" " ;
        }
        cout<<std::endl;
    }
    cout << "P矩阵: " << endl;
    for (v = 0; v < MG.numVertexes; v++) {
        for (w = 0; w < MG.numVertexes; w++) {
            std::cout << P[v][w]<<" " ;
        }
        cout<<std::endl;
    }

    cout << "各顶点间最短路径如下: " << endl;
    for (v = 0; v < MG.numVertexes; v++)
    {
        for (w = v + 1; w < MG.numVertexes; w++)
        {
            cout << "v" << v << "--" << "v" << w << " weight: " << D[v][w]
                << " Path: " << v << ' ';
            k = P[v][w];
            while (k != w)
            {
                cout << "-> " << k << " ";
                k = P[k][w];
            }
            cout << "-> " << w << endl;
        }
        cout << endl;
    }
    return 0;
}
