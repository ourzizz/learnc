#include<iostream>
#include<string>
#define MAXW 1024

using namespace std;
typedef struct MGraph{
    string vexs[10];
    int arcs[10][10];
    int vexnum,arcnum;
}MGraph;

typedef struct close
{
    string adjvex;
    int lowcost;
}Close;
Close Closedge[10];

int LocateVex(MGraph G,string u)
{
    for(int i=0;i<G.vexnum;i++)
    {
        if(G.vexs[i]==u)
            return i;
    }
    return -1;
}

void CreateUDG(MGraph *G)
{//
    G->vexnum=6;
    G->arcnum=10;
    int arcs[36]={
        MAXW,6,1,5,MAXW,MAXW,
        6,MAXW,5,MAXW,3,MAXW,
        1,5,MAXW,5,6,4,      
        5,MAXW,5,MAXW,MAXW,2,
        MAXW,3,6,MAXW,MAXW,6,
        MAXW,MAXW,4,2,6,MAXW,
    };
    string vexs[10]={ "v1", "v2", "v3", "v4", "v5", "v6","","","",""};
    int x=0;
    for (int i = 0; i < 10; ++i) {
        for (int j = 0; j < 10; ++j) {
            if (j<6) {
                G->arcs[i][j]=arcs[x];
                x++ ;
            }
            else
                G->arcs[i][j]=0;
        }
    }
    for ( int i = 0; i < 10; ++i) {
        G->vexs[i] = vexs[i];
    }
}
int minimun(Close Closedge[],int len)
{
    int min=MAXW;
    int post=0;
    for(int i=0;i<len;i++)
    {
        if(Closedge[i].lowcost<min && Closedge[i].lowcost>0)
        {
            min = Closedge[i].lowcost;
            post = i;
        }
    }
    return post;
}
void MiniSpanTree_PRIM(MGraph G,string u)
{
    int k = LocateVex(G,u);
    for(int j=0;j<G.vexnum;j++)
    {
            Closedge[j].adjvex = u;
            Closedge[j].lowcost =G.arcs[k][j];
    }
    Closedge[k].lowcost = 0;//lowcost?U?    
    for(int i=1;i<G.vexnum;i++)
    {
        k = minimun(Closedge,G.vexnum);
        cout <<Closedge[k].adjvex<<"-"<<Closedge[k].lowcost<<"-"<<G.vexs[k]<<endl;   
        Closedge[k].lowcost = 0;//lowcost?U?        
        for(int j=0;j<G.vexnum;j++)
        {
            if(G.arcs[k][j] <  Closedge[j].lowcost)//
            {
                Closedge[j].adjvex = G.vexs[k];
                Closedge[j].lowcost = G.arcs[k][j];
            }
        }
    }
}

int main()
{
    MGraph *G=new MGraph;
    CreateUDG(G);
    MiniSpanTree_PRIM(*G,"v3");
    return 0;
}

