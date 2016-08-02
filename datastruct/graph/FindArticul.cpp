#include <iostream>
#include "./AdjList.h"
using namespace std;
int count = 0;
int visited[13];
int low[13];
void DFSArticul(ALGraph G,int v0)
{
    int min;
    int w;
    visited[v0] = min = ++count;
    for (ArcNode *p=G.vertices[v0].firstarc;p;p=p->nextarc) {
        w = p->adjvex;
        if (0 == visited[w]) {
            DFSArticul(G,w);
            if (low[w]< min) min= low[w];
            if (low[w]>= visited[v0]) std::cout << v0<<G.vertices[v0].data<<"关节点"<< std::endl;
        }
        else if(visited[w] < min)
            min = visited[w];
    }
    low[v0] = min;
}
void FindArticul(ALGraph G)
{
    count = 1; visited[0]=1;
    for (int i = 1; i < G.vexnum; ++i) {
       visited[i]  = 0;
    }
    ArcNode *p = G.vertices[0].firstarc;
    int v=p->adjvex;
    DFSArticul(G,v);
    if (count < G.vexnum) {
        std::cout << 0<<G.vertices[0].data<<"关节点"<< std::endl;
        while(p->nextarc) {
            p = p->nextarc ;
            v = p->adjvex;
            if (visited[v] == 0) {
                DFSArticul(G,v);
            }
        }
    }
}
