#include <stdio.h>
typedef int ElemenType;
typedef struct BiTNode{
    ElemenType element;
    struct BiTNode * Lchild,*Rchild;
}BiTNode,* BiTree;

int CreateBiTree(BiTree T)
{//create bitree by preorder input 
    printf("Create Binarytree by preoder input\n");
    printf("Enter '#' to finish input ,every time just one number can be inputed\n");
    printf("Get Start\n");
    

}
int PreOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int InOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int PostOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int LevelOrderTraverse(BiTree T,int (* Vist)(ElemenType e ));
int main(void)
{
    BiTNode T;
    CreateBiTree(&T);
    return 0;
}
