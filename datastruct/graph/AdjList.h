#define MAX_VERTEX_NUM 20
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
