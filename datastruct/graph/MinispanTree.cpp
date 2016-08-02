#include <iostream>  
#include <string>  
using namespace std;  
typedef struct MGraph{  
    string vexs[10];//顶点信息  
    int arcs[10][10];//邻接矩阵  
    int vexnum, arcnum;  
}MGraph;  
typedef struct Closedge{  
    string adjvex;  
    int lowcost;  
}minside[10];  
int LocateVex(MGraph G, string u)//返回顶点u在图中的位置  
{  
    for(int i=0; i<G.vexnum;i++)  
        if(G.vexs[i]==u)  
            return i;  
    return -1;  
}  
void CreateUDG(MGraph &G)//构造无向图  
{  
    string v1, v2;  
    int w;  
    int i, j, k;  
    cout<<"请输入顶点数和边数：";  
    cin>>G.vexnum>>G.arcnum;  
    cout<<"请输入顶点：";  
    for(i=0; i<G.vexnum; i++)  
        cin>>G.vexs[i];  
    for(i=0; i<G.vexnum; i++)  
        for(j=0; j<G.vexnum; j++)  
            G.arcs[i][j]=1000;  
    cout<<"请输入边和权值："<<endl;  
    for(k=0; k<G.arcnum; k++)  
    {  
        cin>>v1>>v2>>w;  i=LocateVex(G, v1);  
        j=LocateVex(G, v2);  
        G.arcs[i][j]=G.arcs[j][i]=w;  
    }  
}  
int minimum(minside sz, MGraph G)//求sz中lowcost的最小值，返回序号  
{  
    int i=0, j, k, min;  
    while(!sz[i].lowcost)  
        i++;  
    min=sz[i].lowcost;  
    k=i;  
    for(j=i+1; j<G.vexnum; j++)  
    {  
        if(sz[j].lowcost>0 && min>sz[j].lowcost)  
        {  
            min=sz[j].lowcost;  
            k=j;  
        }  
    }  
    return k;  
}  
void MiniSpanTree_PRIM(MGraph G, string u)//普里姆算法  
{  
    int i, j, k;  
    minside closedge;  
    k=LocateVex(G, u);  
    for(j=0; j<G.vexnum; j++)  
    {  
        closedge[j].adjvex=u;  
        closedge[j].lowcost=G.arcs[k][j];  
    }  
    closedge[k].lowcost=0;  
    cout<<"最小生成树各边为："<<endl;  
    for(i=1; i<G.vexnum; i++)  
    {  
        k=minimum(closedge, G);  
        cout<<closedge[k].adjvex<<"-"<<G.vexs[k]<<endl;  
        closedge[k].lowcost=0;  
        for(j=0; j<G.vexnum; j++)  
        {  
            if(G.arcs[k][j] < closedge[j].lowcost)  
            {  
                closedge[j].adjvex=G.vexs[k];  
                closedge[j].lowcost=G.arcs[k][j];  
            }  
        }  
    }  
}  
int main()  
{  
    MGraph g;  
    CreateUDG(g);  
    MiniSpanTree_PRIM(g, g.vexs[0]);  
    cout<<endl;  
    return 0;
}  
