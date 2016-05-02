#include <stack>
#include <iostream>
#include <stdio.h>
#include <malloc.h>
using namespace std;
typedef char ElemenType;
typedef struct BiTNode{
    ElemenType element;
    struct BiTNode * Lchild,*Rchild;
}BiTNode,* BiTree;
typedef BiTNode SElemType;

void InitNode(BiTree T)
{
    T->element='1';
    T->Rchild=T->Lchild=NULL;
}

int CreateBiTree(BiTree *T)
{//create bitree by preorder input
    char element;
    printf("please input a char to fill node\n") ;
    std::cin >> element;
    if (element == '0') T=NULL;
    else
    {
        if (!(*T=(BiTree)malloc(sizeof(BiTNode)))) return 0;
        (*T)->element=element;
        CreateBiTree(&((*T)->Lchild ));
        CreateBiTree(&((*T)->Rchild ));
    }
    return 1;
}


int Vist(ElemenType e)
{
    std::cout << e;
    return 1;
}

int PreOrderTraverse(BiTree T,int (* Vist)(ElemenType e )) {
    if (T!=NULL) {
        std::cout << T->element << std::endl;
        PreOrderTraverse(T->Lchild,Vist);
        PreOrderTraverse(T->Rchild,Vist);
    }
    return 1;
}

int PreOrderTraverse_nonrecurs(BiTree T,int (* Vist)(ElemenType e ))
{
    stack<BiTree> s;
    BiTree p = NULL;
    s.push(T);
    while(!s.empty()) {
        while((p=s.top()) && p) 
            s.push(p->Lchild);
        s.pop();
        if (!s.empty()) 
        {
            p=s.top();
            s.pop();
            Vist(p->element);
            s.push(p->Rchild);
        }//endif
    }//endwhile
    return 1;
}
int InOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int PostOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int LevelOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int main()
{
    printf("Create Binarytree by preoder input\n");
    printf("Enter '#' to finish input ,every time just one number can be inputed\n");
    printf("Get Start\n");
    BiTree T=NULL;
    CreateBiTree(&T);
    PreOrderTraverse_nonrecurs(T,Vist);
    //PreOrderTraverse(T,Vist);
    return 0;
}
