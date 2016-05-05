#include <stack>
#include <stdio.h>
#include <malloc.h>
#include <iostream>
using namespace std;

typedef char ElemenType;

enum  PointerTag{Link,Thread};

typedef struct BiThrNode{
    ElemenType element;
    struct BiThrNode *lchild,*rchild;
    PointerTag LTag,RTag;
}BiThrNode,*BiThrTree;

int CreateBiTree(BiThrTree *T)
{//create BiThrTree by preorder input
    char element;
    printf("please input a char to fill node\n") ;
    std::cin >> element;
    if (element == '0') T=NULL;
    else
    {
        if (!(*T=(BiThrTree)malloc(sizeof(BiThrNode)))) return 0;
        (*T)->element=element;
        CreateBiTree(&((*T)->lchild ));
        CreateBiTree(&((*T)->rchild ));
    }
    return 1;
}
int InOrderTraverse_Thr(BiThrTree T,int (* Vist )(ElemenType e))
{
    BiThrTree p = T->lchild;
    while(p != T) {
        while(p->LTag==Link) {
            p=p->lchild;
        }
        Vist(p->element);
        while(p->RTag == Thread && p->rchild!=T) {
            p = p->rchild;
            Vist(p->element);
        }
        p = p->rchild;
    }
    return 1;
}
void InThreading(BiThrTree p,BiThrTree pre) 
{
    if (p!=NULL) {
        InThreading(p->lchild,pre);
        if (!p->lchild) { p->LTag = Thread; p->lchild=pre; }
        if(! pre->rchild){pre->RTag=Thread;pre->rchild = p; }
        pre = p;
        InThreading(p->rchild,pre);
    }
}
int InOrderThreading(BiThrTree &Thrt,BiThrTree T)
{
    BiThrTree pre;
    if (!( Thrt = (BiThrTree)malloc(sizeof(BiThrNode)) ))  return 0;
    Thrt->LTag = Link; Thrt->RTag = Thread;
    Thrt->rchild = Thrt;
    if (!T) {
        Thrt->rchild=Thrt;
    }
    else{
        Thrt->lchild=T;
        pre = Thrt;
        InThreading(T,pre);
        pre->rchild=Thrt;
        pre->RTag=Thread;
        Thrt->rchild=pre;
    }
    return 1;
}
int Vist(ElemenType e)
{
    std::cout << e;
    return 1;
}

int main()
{
    printf("Create Binarytree by preoder input\n");
    printf("Enter '#' to finish input ,every time just one number can be inputed\n");
    printf("Get Start\n");

    BiThrTree T=NULL;
    BiThrTree Temp=NULL;
    CreateBiTree(&T);
    InOrderThreading(Temp,T);
    InOrderTraverse_Thr(T,Vist);
    //PreOrderTraverse(T,Vist);
    return 0;
}
