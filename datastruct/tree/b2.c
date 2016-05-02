#include <stdio.h>
#include <malloc.h>
struct btree_node
{
    int tag;
    char   data;
    int    weight;
    struct btree_node  *lchild;
    struct btree_node  *rchild;
};
struct  snode
{
    struct btree_node  *data;
    struct snode *next;
};
void root_first(struct btree_node  *tmp_node)
{
    printf("%c", tmp_node->data);
    if(tmp_node->lchild != NULL)
        root_first(tmp_node->lchild);
    if(tmp_node->rchild != NULL)
        root_first(tmp_node->rchild);
}
int main()
{
    int i;
    struct  snode *top;
    struct  snode *sp;
    struct btree_node   btree_array[10] = 
    {
        {0, 'A',  200,  NULL, NULL},
        {0, 'B',  100,  NULL, NULL},
        {0, 'C',  400,  NULL, NULL},
        {0, 'D',   50,  NULL, NULL},
        {0, 'E',  150,  NULL, NULL},
        {0, 'F',  500,  NULL, NULL},
        {0, 'G',  120,  NULL, NULL},
        {0, 'H',  180,  NULL, NULL},
        {0, 'I',  450,  NULL, NULL},
        {0, 'J',  480,  NULL, NULL}
    };
    struct btree_node  *tmp_node;
    struct btree_node  *root_node;
    root_node = &btree_array[0];
    int max=sizeof(btree_array)/sizeof(struct btree_node);
    for(i = 1; i < max;  i++)
    {
        tmp_node = root_node;
        while(1)
        {
            if(tmp_node->weight  >   btree_array[i].weight)
            {
                if(tmp_node->lchild != NULL)
                    tmp_node = tmp_node->lchild;
                else
                {
                    tmp_node->lchild = &btree_array[i];
                    break;
                }
            }
            else
            {
                if(tmp_node->rchild != NULL)
                    tmp_node = tmp_node->rchild;
                else
                {
                    tmp_node->rchild = &btree_array[i];
                    break;
                }
            }
        }
    }
    root_first(&btree_array[0]);
    printf("---------no rotate algrithm  ------------\n");
    top = NULL;
    tmp_node = root_node;
    /*  while(tmp_node != NULL)//
        {
        printf("%c", tmp_node->data);
        if(tmp_node->rchild != NULL)
        {
        sp = (struct snode *)malloc(sizeof(struct snode));
        sp->next = top;
        sp->data = tmp_node->rchild;
        top = sp;
        }
        if(tmp_node->lchild != NULL)
        {
        tmp_node = tmp_node->lchild;
        }
        else
        {
    //  pop data from stack 
    if(top != NULL)
    {
    struct snode *old = top;
    tmp_node = top->data;
    top = top->next;
    free(old);
    }
    else
    {
    //  browse btree finished
    break;
    }
    }
    }
    while(tmp_node != NULL)//
    {
    if(tmp_node->lchild != NULL)//temp
    {
    sp = (struct snode *)malloc(sizeof(struct snode));
    sp->next = top;
    sp->data = tmp_node;
    top = sp;
    tmp_node=tmp_node->lchild;
    }
    else
    { 
    while(1)
    {   
    printf("%c",tmp_node->data);//????
    if(tmp_node->rchild != NULL)// 
    {
    tmp_node=tmp_node->rchild;
    break;
    }
    else
    {
    if(top != NULL)//top tmp
    { 
    struct snode *old = top;
    tmp_node = top->data;
    top = top->next;
    free(old);
    //break;tmp
    }
    else 
    {
    tmp_node = NULL;//tmp
    break;
    }
}
}
}
}*/
int exit=0; 
while(tmp_node != NULL)// 
{
    if(tmp_node->lchild != NULL)//temp
    {
        sp = (struct snode *)malloc(sizeof(struct snode));
        sp->next = top;
        sp->data = tmp_node;//
        top = sp;//see
        if(tmp_node->rchild==NULL)
        {
            tmp_node->tag=2;// 
        }
        else
            tmp_node->tag=1;// 
        tmp_node=tmp_node->lchild;
    }
    else//
    { 
        while(1)//
        {
            if(exit==1)
                break;
            //printf("%c",tmp_node->data);????
            if(tmp_node->rchild != NULL)// tag2
            {
                if(tmp_node->tag==0)//tag=0
                {
                    sp = (struct snode *)malloc(sizeof(struct snode));
                    sp->next = top;
                    sp->data = tmp_node;
                    top = sp;
                    tmp_node->tag=2;
                    tmp_node=tmp_node->rchild;
                    break;
                }
                else
                {
                    tmp_node->tag++;
                    tmp_node=tmp_node->rchild;
                    break;
                }
            }
            else//
            {
                while(1)//3
                {
                    printf("%c",tmp_node->data);
                    if(top != NULL)//top tmp
                    {                             
                        tmp_node = top->data;
                        if(tmp_node->tag==2)
                        {
                            struct snode *old = top;
                            top = top->next;
                            free(old);
                        }   
                        else
                            break;
                        //break;tmp
                    }
                    else 
                    {
                        tmp_node = NULL;//tmp
                        exit=1;
                        break;//2
                    }//
                    if(tmp_node->tag==1)//3
                        break;
                }
            }
        }//////
    }
}
printf("---------no rotate algrithm  ------------\n");
return 0;
}
