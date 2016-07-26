/*************************************************
 * 为了将来的自己还是花点时间写注释吧
 * 这个文件是手工输入二叉树的前序序列建立线索二叉树
 * 然后再通过线索中序遍历二叉树的算法
 * 例如输入AB00C00
 * 遍历结果为BAC
**************************************************/
#include <stack>
#include <stdio.h>
#include <malloc.h>
#include <iostream>
using namespace std;

typedef char ElemenType;

enum  PointerTag{Link,Thread};

// |lchild|ltag|data|rtag|rchild|
//   Thread || link
typedef struct BiThrNode{
    ElemenType element;
    struct BiThrNode *lchild,*rchild;
    PointerTag LTag,RTag;
}BiThrNode,*BiThrTree;

int CreateBiTree(BiThrTree *T)
{//输入前序序列，递归地建立二叉树，这里的二叉树还不具备线索，只是有存储线索的空间
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
{//中序遍历线索二叉树，起始点为二叉树的头结点，这个结点是在线索化的过程中加入的
    BiThrTree p = T->lchild;
    while(p != T) {
        while(p->LTag==Link) {
            p=p->lchild;
        }
        Vist(p->element);
        while(p->RTag == Thread && p->rchild!=T) {//右孩子为线索的话，这个线索就是ｐ的后继，那么ｐ就ｋｅｙ６ｉ跳到后继输出后继然后继续循环，直到ｐ回到头结点
            p = p->rchild;
            Vist(p->element);
        }
        p = p->rchild;
    }
    return 1;
}

void InThreading(BiThrTree p,BiThrTree *pre) 
{//pre必须作为二级指针传入递归函数，否则在退栈后pre的值就得不到保持，回退为上级栈中的值
 //p和ｔ交替走指针，靠函数递归进行指针ｐ的回溯,算法想不起来可以参考自考网动画
    if (p!=NULL) {
        InThreading(p->lchild,pre);
        if (!p->lchild) { 
            p->LTag = Thread;
            p->lchild=*pre; 
        }
        if(! ( *pre )->rchild){
            ( *pre )->RTag=Thread;
            ( *pre )->rchild = p; 
        }
        *pre = p;
        InThreading(p->rchild,pre);
    }
}

BiThrTree InOrderThreading(BiThrTree &Thrt,BiThrTree T)
{//这个函数只是为InThreading函数做铺垫和善后的，没什么难度，主要是引入头结点,并返回头结点作为遍历函数的入口
    BiThrTree pre=NULL;
    if (!(Thrt = (BiThrTree)malloc(sizeof(BiThrNode))))  return 0;
    Thrt->LTag = Link; Thrt->RTag = Thread;
    Thrt->rchild = Thrt;
    if (!T) {
        Thrt->rchild=Thrt;
    }
    else{
        Thrt->lchild=T;
        pre = Thrt;
        InThreading(T,&pre);
        pre->rchild=Thrt;
        pre->RTag=Thread;
        Thrt->rchild=pre;
    }
    return Thrt;
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
    T = InOrderThreading(Temp,T);
    InOrderTraverse_Thr(T,Vist);
    //PreOrderTraverse(T,Vist);
    return 0;
}
