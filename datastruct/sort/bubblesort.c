#include <stdio.h>
int sort(int a[],int size)
{
    int i,j,last,tmp;
    i = size - 1;
    while (i>0) {
        last = 0;
        for (j = 0; j < i; ++j) {
            if (a[j] > a[j+1]) {
                tmp = a[j] ;
                a[j]=a[j+1];
                a[j+1]=tmp;
                last = j;
            } 
        }
        i = last;
    }
}
int main()
{
    int a[10]={1,8,4,6,5,9,0,10,11,7};
    char chenhai={ 'c', 'h', 'e', 'n', 'h', 'h', }
    sort(a,sizeof(a)/sizeof(int));
    /*printf("%d\n", sizeof(a)/sizeof(int));*/
    int i;
    for (i = 0; i < 10; ++i) {
        printf("%d\n", a[i]);
    }
    return 0;
}

