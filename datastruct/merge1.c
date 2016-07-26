#include <stdio.h>
#include <malloc.h>
#define INFINITE 10000
void merge(int *A,int p,int q,int r)
{
    int n1 = q - p +1;
    int n2 = r - q;
    int *a=(int*)malloc(n1*sizeof(int));
    int *b=(int*)malloc(n2*sizeof(int));
    int i,j;
    for (i = 0; i < n1; ++i) {
        a[i]=A[p+i];
    }
    for (j = 0; j < n2; ++j) {
        b[j]=A[q+j+1]; 
    }
    a[i]=INFINITE;
    b[j]=INFINITE;
    int k;
    i=j=0;
    for (k = p; k <= r; ++k) {
        if (a[i]>b[j]) {
            A[k]=b[j];
            j++;
        }
        else if (a[i]<b[j]) {
            A[k]=a[i];
            i++;
        }
    }
}
void MERGE_SORT(int *A,int start,int end)
{
    if(start<end)
    {
        int q = (end+start)/2;
        MERGE_SORT(A,start,q);
        MERGE_SORT(A,q+1,end);
        merge(A,start,q,end);
    }
}
int main(void)
{
    int a[7]={7,5,4,20,10,9,8};
    int size=sizeof(a)/sizeof(int);
    MERGE_SORT(a,0,size-1);
    int i;
    for (i = 0; i < size; ++i) {
        printf("%d ",a[i] );
    }
    printf("\n");

    return 0;
}

