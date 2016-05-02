#include <stdio.h>
#include <malloc.h>

typedef struct {
    int *A;
    int length;
}list;
void initlist(list *a,int size)
{
    a->A=(int*)malloc(size*sizeof(int));
    a->length = size;
    int i=0;
    while(i!=size) {
        a->A[i]=-1;
        i++;
    }
}
void output(list *a)
{
    int i=0;
    while(i<a->length) {
        printf("%d ", a->A[i]);
    }
}
int getelement(list a,int i)
{
    return a.A[i];
}
int listlength(list a)
{
    return a.length;
}

void deletem(list *a,int i)
{
    a->A[i]=-1;
}
void insert(list *a,int i,int x)
{
    a->A[i]=x;
}
void PowerSet(int i,list *A,list *B)
{
    if (listlength(*A)<i) {
        output(B);
    }
    else{
        int x=getelement(*A,i);
        insert(B,i,x);
        PowerSet(i+1,A,B);
        deletem(B,i);
        PowerSet(i+1,A,B);
    }
}
int main(void)
{
    list A,B;
    initlist(&A,7);
    int i=0;
    while(i<A.length) {
        A.A[i] = i;
        i++;
    }
    initlist(&B,7);
    PowerSet(0,&A,&B);
    return 0;
}
