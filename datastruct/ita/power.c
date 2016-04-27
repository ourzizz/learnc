#include <stdio.h>
#include <malloc.h>

typedef struct {
    int *Array;
    int length;
    int index;
}list;
void initlist(list *A,int size)
{
    A->Array=(int*)malloc(size*sizeof(int));
    A->length = size;
    A->index=0;
    int i=0;
    while(i!=size) {
        A->Array[i]=-1;
        i++;
    }
}
void output(list *A)
{
    int i=0;
    /*while(i<A->length) {*/
        /*printf("%d ", A->Array[i]);*/
        /*i++;*/
    /*}*/
    while(i < A->index)
    {
        printf("%d ", A->Array[i]);
        i++;
    }
    printf("\n");
}
int getelement(list A,int i)
{
    return A.Array[i];
}
int listlength(list A)
{
    return A.length;
}

void deletem(list *A,int i)
{
    A->Array[i]=-1;
    A->index--;
}
void insert(list *A,int i,int x)
{
    A->Array[i]=x;
    A->index++;
}
void PowerSet(int i,list *A,list *B)
{
    if (listlength(*A)<=i) {
        output(B);
    }
    else{
        int x=getelement(*A,i);
        insert(B,B->index,x);
        PowerSet(i+1,A,B);
        deletem(B,B->index);
        PowerSet(i+1,A,B);
    }
}
int main(void)
{
    list A,B;
    initlist(&A,7);
    int i=0;
    while(i<A.length) {
        A.Array[i] = i;
        i++;
    }
    initlist(&B,7);
    PowerSet(0,&A,&B);
    return 0;
}
