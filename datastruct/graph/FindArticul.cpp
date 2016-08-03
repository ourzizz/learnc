#include <iostream>
#include <string.h>
#include <malloc.h>

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

int count = 0;
int visited[13];
int low[13];
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
    char vexs[] = {'A', 'B', 'C', 'D', 'E', 'F', 'G'};
    char edges[][2] = {
        {'A', 'C'}, 
        {'A', 'D'}, 
        {'A', 'F'}, 
        {'B', 'C'}, 
        {'C', 'D'}, 
        {'E', 'G'}, 
        {'F', 'G'}}; 
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
        // 将node1链接到"p1所在链表的末尾"
        if(pG->vertices[p1].firstarc == NULL)
            pG->vertices[p1].firstarc = node1;
        else
            link_last(pG->vertices[p1].firstarc, node1);
        // 初始化node2
        node2 = (ArcNode*)malloc(sizeof(ArcNode));
        node2->adjvex = p1;
        // 将node2链接到"p2所在链表的末尾"
        if( pG->vertices[p2].firstarc == NULL )
            pG->vertices[p2].firstarc = node2;
        else
            link_last( pG->vertices[p2].firstarc, node2 );
    }
    return pG;
}
void DFSArticul(ALGraph G,int v0)
{
    int min;//声明一个变量min保存计算中产生的最浅访问层
    int w; //w代表当前v0节点的孩子节点
    visited[v0] = min = ++count; //着重讲下count,这个全局变量记录着访问每个节点的次序，每次自增后填充visited数组相应的节点
    for (ArcNode *p=G.vertices[v0].firstarc;p;p=p->nextarc) {
        w = p->adjvex;//只需要从某一行开始，递归会逐次访问到各个邻接点，因为DFSArticul(G,w)
        if (0 == visited[w]) { //标识w结点尚未被访问
            DFSArticul(G,w);//没有访问过就对其进行递归
            if (low[w]< min) min= low[w]; //递归将会对w结点的最浅访问层次进行求解，并将结果写入全局数组low中，如果low[w]最浅层次比min(当前栈节点的访问层次要浅)小就更新min
            if (low[w]>= visited[v0]) //low[w]大于等于v0的访问层次，说明w一下节点就算有回边也最多回到v0一层，即无法回到v0的祖先节点,那么v0必为割点
                std::cout << v0<<G.vertices[v0].data<<"关节点"<< std::endl;
        }
        else if(visited[w] < min) //如果w结点已经访问过了，说明w在v0之前就被访问过，是v0的祖先节点，这是一条回边
            min = visited[w]; //更新min
    }
    low[v0] = min;//此时v0的low值就得出，min是v0自身访问层次以及孩子节点的low值和自身回边中层次最浅的值
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
int main(void)
{
    ALGraph *G=create_example_lgraph();
    FindArticul(*G);
    return 0;
}
