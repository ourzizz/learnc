#include <stdio.h>
#include <malloc.h>
typedef char ElemenType;
typedef struct BiTNode{
    ElemenType element;
    struct BiTNode * Lchild,*Rchild;
}BiTNode,* BiTree;

void InitNode(BiTree T)
{
    T->element='1';
    T->Rchild=T->Lchild=NULL;
}
int CreateBiTree(BiTree T)
{//create bitree by preorder input 
    char element;
    printf("please input a char to fill node\n") ;
    scanf("%c",&element);
    if (element == ' ') T=NULL;
    else
    {
        if (!(T=(BiTree)malloc(sizeof(BiTNode)))) return 0;
        T->element=element;
        CreateBiTree(T->Lchild);
        CreateBiTree(T->Rchild);
    }
    return 1;
}

int PreOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int InOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int PostOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int LevelOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int main(void)
{
    printf("Create Binarytree by preoder input\n");
    printf("Enter '#' to finish input ,every time just one number can be inputed\n");
    printf("Get Start\n");
    BiTNode T;
    CreateBiTree(&T);
    return 0;
}
