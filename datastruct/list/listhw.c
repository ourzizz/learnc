#include <stdio.h>
#include <malloc.h>
#define TRUE 1
#define FALSE 0
#define OK 1
#define ERROR 0
#define INFEASIBLE -1
#define OVERFLOW -2

typedef int Status ;

typedef int ElemType ;
typedef struct LNode
{
    ElemType data;
    struct LNode * next;
}*Link,*Position;

typedef struct
{
    Link head,tail;
    int len;
}LinkList;

Status MakeNode(Link *p,ElemType e) {
//分配由p指向的值为e的节点，并返回ok；若分配失败返回error
    *p = (Link)malloc(sizeof(struct LNode));
    if(NULL == p)
        return ERROR;
    (*p)->data = e;
    (*p)->next = NULL;
    return OK;
}
Status FreeNode(Link *p) 
{
/*释放p所指向的结点 */
    if (NULL==p) {
        return FALSE;
    }
    free(*p);
    return OK;
    *p = NULL;
}
Status InitList(LinkList *L) {
    /*构造一个空的线性链表L*/
    Link head = (Link)malloc(sizeof(struct LNode));
    head->next = NULL;
    L->len = 0;
    L->head = L->tail = head;
   return OK;
}

Status DestroyList(LinkList *L) { /*销毁线性链表L*/
    Link p,q;
    p = q = L->head;
    while(p != NULL) {
        q = p->next;
        free(p);
        p = q;
    }
    L->head = L->tail = NULL;
    L->len = 0;
    return OK;
}

/*Status ClearList(LinkList *L){ [>将线性链表L重置为空链表<] }*/
Status InsFirst(LinkList *L,Link h,Link s){
    /*已知h指向线性链表的头结点，将s所指节点插入到第一个结点之前*/
    if (h == NULL | s==NULL) {
        return FALSE;
    }
    s->next = h->next;
    h->next = s;
    L->len++;
    return OK;
}
Status DelFirst(Link h,Link *q){
/*已知h指向线性链表的头结点，删去链表中第一个结点并以q返回*/
    if (h->next == NULL) {
        q=NULL;
        return FALSE;
    }
    *q = h->next;
    h->next = (*q)->next;
    return OK;
}
Status Append(LinkList *L,Link s){
/*将指针s所指(彼此以指针相连)的一串结点链接在线性链表L的最后一个结点，之后并改变链表L的尾指针指向新的尾结点*/
    L->tail->next = s;
    /*Link tmp = L->tail;*/
    while( L->tail->next != NULL) {
        L->tail = L->tail->next;
    }
    return OK;
}
Status Remove(LinkList *L,Link *q){
    /*删除线性链表L中的尾结点并以q返回，改变链表L的尾指针指向新的尾结点*/
    *q = L->tail;
    Link p = L->head;
    while(p->next != L->tail) 
        p = p->next;
    L->tail = p;
    L->tail->next = NULL;
    return OK;
}
Status InsBefore(LinkList *L,Link *p,Link s){
    /*已知p指向线性链表L中的一个结点，将s所指结点插入在p所指节点之前，并修改指针p指向新插入的结点*/
    if ((*p) == NULL | s==NULL) {
        return FALSE;
    }
    Link tmp = L->head;
    while(tmp->next != *p) {
        tmp = tmp->next;
    }
    s->next = *p;
    tmp->next = s;
    return OK;
}

Status InsAfter(LinkList *L,Link *p,Link s){
    /*已知p指向线性链表L中的一个结点，将s所指结点插入在p所指节点之后，并修改指针p指向新插入的结点*/
    if ((*p) == NULL | s==NULL) {
        return FALSE;
    }
    s->next = (*p)->next;
    (*p)->next = s;
    return OK;
}
Status SetCurElem(Link *p,ElemType e){
    /*已知p指向线性链表中的一个结点，用e更新p所指结点中数据元素的值*/
    if (p == NULL) {
        return FALSE;
    }
    (*p)->data = e;
    return OK;
}
ElemType GetCurElem(Link p){
    /*已知p指向线性链表中的一个结点，返回p所指结点中数据元素的值*/
    if (NULL == p) {
        return FALSE ;
    }
    return p->data;
}
Status ListEmpty(LinkList L){
    /*对线性链表判空，空TURE，否则FALSE*/
    if (L.head == L.tail ) {
        return TRUE;
    }
    return FALSE;
}

int ListLength(LinkList L){
    /*返回线性链表中元素的个数*/
    Link tmp = L.head->next;
    int len = 0;
    while(tmp != NULL) {
        len ++;
        tmp=tmp->next;
    }
    return len;
}
/*Position GetHead(LinkList L){[>返回线性链表L中头结点的位置<]}*/
/*Position GetLast(LinkList L){[>返回线性链表L中最后一个结点的位置<]}*/
Position PriorPos(LinkList L,Link p){
    /*已知p指向线性链表L中的一个结点，返回p所指结点的直接前驱位置，若无前驱则返回NULL*/
    Link tmp = L.head;
    while(tmp->next != p) {
        tmp = tmp->next;
    }
    if (tmp == L.head) {
        return NULL;
    }
    return tmp;
}
Position NextPos(LinkList L,Link p){
    /*已知p指向线性链表L中的一个结点，返回p所指结点的直接后继位置，若无后继则返回NULL*/
    if (p == NULL | p->next == NULL) {
        return NULL;
    }
    return p->next;
}
Status LocatePos(LinkList L,int i,Link *p){
    /*返回p指示线性链表L中第i个结点的位置，并返回ok，i若不合法返回ERROR*/
    int j = 0;
    Link tmp = L.head;
    for (j = 0;j<i;j++) {
        if (tmp != NULL) {
            tmp=tmp->next;
        }
        else{
            (*p) = NULL;
            return ERROR;
        }
    }
    (*p) = tmp;
    return OK;
}

Position LocateElem(LinkList L,ElemType e,Status (*compare)(ElemType,ElemType)){
    /*返回线性链表L中第1个与e满足函数compare()判定关系的元素的位置，若不存在这样的元素，则返回NULL*/
    Link tmp = L.head;
    while(tmp != NULL) {
        if(OK == compare(tmp->data,e))
            return tmp;
        else 
            tmp=tmp->next;
    }
    /*if(tmp == NULL)*/
    /*return NULL;*/
    return tmp;
}

Status ListTraverse(LinkList L,Status (*vist)(int ,Link )){
    /*依次对Ｌ的每个元素调用函数vist()一旦vist失败，则操作失败 */
    Link tmp = L.head->next;
    int i = 1;
    while(tmp != NULL) {
        if (FALSE==vist(i,tmp)) 
            return FALSE ;
        else
            tmp=tmp->next;
        i++;
    }
    return OK;
}
Status Myvist(int i,Link p)
{//仅仅是为遍历提供一种输出方式
    if(NULL != p )
    {
        printf("第%d个数据为%d\n",i,p->data);
        return OK;
    }
    else
        return FALSE;
}

Status CreateListByArray(ElemType *array,int ArraySize,LinkList *L,Status (*InsertFun)(LinkList *,Link,Link))
{
    Link tmp = NULL;
    int i = 0;
    for (i = 0; i < ArraySize; ++i) {
        MakeNode(&tmp,array[i]);
        /*InsFirst(L->head,tmp);*/
        InsertFun(L,L->head,tmp);
    }
    return OK;
}
Status MyCompareEqu(ElemType a,ElemType b)
{
    if (a==b) 
        return OK;
    return FALSE;
}
Status MyCompareMore(ElemType a,ElemType b)
{
    if (a>b) 
        return OK;
    return FALSE;
}
void ViewList(LinkList L)
{
    Link p = L.head->next;
    while(p != NULL) {
        printf("%d\n",p->data);
        p = p->next;
    }
}
int MergeList_L(LinkList &La,LinkList &Lb,LinkList &Lc)
{/*已知单链线性表La和Lb的元素按值非递减排列，归并La Lb得到Lc，Lc的值也非递减排列*/
}
int main()
{
    LinkList L;
    InitList(&L);
    Link test=NULL;
    ElemType array[10] = {10,9,8,7,6,5,4,3,2,1};
    /*ElemType array[10] = {0,9,8,7,6,5,4,3,2,1};*/

    CreateListByArray(array,sizeof(array)/sizeof(ElemType),&L,&InsFirst);
    printf("Length= %d\n",ListLength(L));
    LocatePos(L,9,&test);
    printf("第9个节点数据为%d\n",test->data) ;
    printf("%s\n","------------------------------------") ;
    ListTraverse(L,&Myvist);
    DestroyList(&L);
    return 0;
}
