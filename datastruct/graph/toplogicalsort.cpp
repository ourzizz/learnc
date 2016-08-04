#include <iostream>
#include <string.h>
#include <malloc.h>
#include <stack>
#define MAX 100
#define isLetter(a)  ((((a)>='a')&&((a)<='z')) || (((a)>='A')&&((a)<='Z')))
#define LENGTH(a)  (sizeof(a)/sizeof(a[0]))
#define MAX_VERTEX_NUM 20

using namespace std;
typedef struct ArcNode{
    int adjvex;
    struct ArcNode * nextarc;
    char *info;
}ArcNode;
typedef struct VNode{
    char data;
    ArcNode * firstarc;
}VNode,AdjList[MAX_VERTEX_NUM];
typedef struct{
    AdjList vertices;
    int vexnum,arcnum;
    int kind;//图的种类标识，没啥用
}ALGraph;

int indegree[10];
int get_position(ALGraph G, char c)
{
    for (int i = 0; i < G.vexnum; ++i) {
        if (G.vertices[i].data == c) {
            return  i;
        }
    }
    return 0;
}
void link_last(ArcNode * arc,ArcNode* node)
{
    while(arc->nextarc != NULL)  arc=arc->nextarc;
    arc->nextarc = node;
}
ALGraph* create_example_lgraph()
{
    char c1, c2;
    char vexs[] = {'A', 'B', 'C', 'D', 'E', 'F'};
    char edges[][2] = {
        {'A','B'}, 
        {'A','C'},
        {'A','D'},
        {'E','C'},
        {'E','F'},
        {'C','F'},
        {'D','B'},
        {'D','F'},
    }; 
    int vlen = LENGTH(vexs);
    int elen = LENGTH(edges);
    int i, p1, p2;
    ArcNode *node1, *node2;
    ALGraph* pG;


    if ((pG=(ALGraph*)malloc(sizeof(ALGraph))) == NULL )
        return NULL;
    memset(pG, 0, sizeof(ALGraph));

    // 初始化"顶点数"和"边数"
    pG->vexnum = vlen;
    pG->arcnum = elen;
    // 初始化"邻接表"的顶点
    for(i=0; i<pG->vexnum; i++)
    {
        pG->vertices[i].data = vexs[i];
        pG->vertices[i].firstarc = NULL;
    }

    // 初始化"邻接表"的边
    for(i=0; i<pG->arcnum; i++)
    {
        // 读取边的起始顶点和结束顶点
        c1 = edges[i][0];
        c2 = edges[i][1];

        p1 = get_position(*pG, c1);
        p2 = get_position(*pG, c2);

        // 初始化node1
        node1 = (ArcNode*)malloc(sizeof(ArcNode));
        node1->adjvex = p2;
        node1->nextarc = NULL;
        // 将node1链接到"p1所在链表的末尾"
        if(pG->vertices[p1].firstarc == NULL)
            pG->vertices[p1].firstarc = node1;
        else
            link_last(pG->vertices[p1].firstarc, node1);
        // 初始化node2
        //node2 = (ArcNode*)malloc(sizeof(ArcNode));
        //node2->adjvex = p1;
        //node2->nextarc = NULL;
        // 将node2链接到"p2所在链表的末尾"
        //if( pG->vertices[p2].firstarc == NULL )
            //pG->vertices[p2].firstarc = node2;
        //else
            //link_last( pG->vertices[p2].firstarc, node2 );
    }
    return pG;
}
void FindInDegree(ALGraph G,int *indegree)
{
    for (int i = 0; i < G.vexnum; ++i) {
        for (ArcNode* p=G.vertices[i].firstarc;p; p=p->nextarc) 
        {
            indegree[p->adjvex]++;
        }
    }
}
int TopologicalSort(ALGraph G)
{
    int k=0;
    int count=0;
    int j=0;
    FindInDegree(G,indegree);
    std::stack<int> S;
    for (int i = 0; i < G.vexnum; ++i) {
        if (!indegree[i]) {
            S.push(i);
        }
    }
    while(!S.empty()) {
        j=S.top();
        std::cout << G.vertices[j].data << std::endl; S.pop();count++;
        for (ArcNode* p=G.vertices[j].firstarc;p; p=p->nextarc) {
            k=p->adjvex;
            if (!(--indegree[k])) {
                S.push(k);
            }
        }
    }
    if (count < G.vexnum) 
    {
        std::cout << "图中存在环" << std::endl;
        return 0;
    }
    else
        return 1;
}

int main(void)
{
    ALGraph * G=create_example_lgraph(); 
    TopologicalSort(*G);
    return 0;
}
