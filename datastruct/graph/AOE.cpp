/*
 * 求解关键路径，图的构造与以往一致，严蔚敏书本版本手工填入自动生成，方便调试
 * 正向拓扑排序求出每个节点的最早开始是时间，其拓扑排序记录入辅助栈中，然后再
 * 从栈中输出每个节点就形成了逆向拓扑，此时再逐次求得每个节点的最晚开始时间
 * 最早开始时间等于最晚开始时间的节点即为关键路径上的节点。关键路径并不唯一
 */
#include <iostream>
#include <string.h>
#include <malloc.h>
#include <stack>
#include <stdio.h>
#define MAX 100
#define isLetter(a)  ((((a)>='a')&&((a)<='z')) || (((a)>='A')&&((a)<='Z')))
#define LENGTH(a)  (sizeof(a)/sizeof(a[0]))
#define MAX_VERTEX_NUM 20

using namespace std;
typedef struct ArcNode{
    int adjvex;
    int weight;
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
int earlytime[10];
int lasttime[10];
std::stack<int> R;
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
    char c1, c2,c3;
    char vexs[] = {'A', 'B', 'C', 'D', 'E', 'F','G','H','I'};
    char edges[][3] = {
        {'A','B','6'}, 
        {'A','C','4'},
        {'A','D','5'},
        {'B','E','1'},
        {'C','E','1'},
        {'D','F','2'},
        {'E','G','9'},
        {'E','H','7'},
        {'F','H','4'},
        {'G','I','2'},
        {'H','I','4'},

    }; 
    int vlen = LENGTH(vexs);
    int elen = LENGTH(edges);
    int i, p1, p2;
    ArcNode *node1;
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
        c3 = edges[i][2];

        p1 = get_position(*pG, c1);
        p2 = get_position(*pG, c2);

        // 初始化node1
        node1 = (ArcNode*)malloc(sizeof(ArcNode));
        node1->adjvex = p2;
        node1->weight = c3-'0';
        node1->nextarc = NULL;
        // 将node1链接到"p1所在链表的末尾"
        if(pG->vertices[p1].firstarc == NULL)
            pG->vertices[p1].firstarc = node1;
        else
            link_last(pG->vertices[p1].firstarc, node1);
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
        R.push(j);
        std::cout << G.vertices[j].data <<earlytime[j]<< std::endl; S.pop();count++;
        for (ArcNode* p=G.vertices[j].firstarc;p; p=p->nextarc) {
            k=p->adjvex;
            if (earlytime[k] < p->weight + earlytime[j]) {
                earlytime[k] = p->weight + earlytime[j];
            }
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
int CriticalPath(ALGraph G)
{
    if (!TopologicalSort(G))  return 0;
    int j=0;
    for (int i = 0; i < G.vexnum; ++i) {
       lasttime[i] = earlytime[G.vexnum-1];
    }
    while(!R.empty()) {
        j=R.top(); R.pop();
        for (ArcNode* p=G.vertices[j].firstarc;p; p=p->nextarc) {
            if (lasttime[j] >lasttime[p->adjvex]-p->weight) {
                lasttime[j] =lasttime[p->adjvex]-p->weight;
            }
        }
    }
    int k=0;
    int dut=0;
    int ee=0;
    int el=0;
    char tag=' ';
    std::cout<< "j"<<" "<< "k"<<" " << "dut"<<" " << "ee" <<" "<< "el"<<" "<<"tag"<<" "  << std::endl;
    for (int j = 0; j < G.vexnum; ++j) {
        //std::cout << lasttime[i] << std::endl;
        for (ArcNode* p=G.vertices[j].firstarc;p; p=p->nextarc) {
            k=p->adjvex;dut=p->weight;
            ee=earlytime[j];el=lasttime[j];
            tag = (ee==el)?'*':' ';
            std::cout<< j<<" "<< k<<" " << dut<<"    " << ee <<"  "<< el<<"   "<<tag<<" "  << std::endl;

        }
    }
    return 1;
}

int main(void)
{
    ALGraph * G=create_example_lgraph(); 
    CriticalPath(*G);
    return 0;
}
