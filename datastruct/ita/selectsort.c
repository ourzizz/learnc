#include <stdio.h>
int main(void)
{
    int a[7]={5,7,4,20,10,9,8};
    int size=sizeof(a)/sizeof(int);

    int minpost;
    int sp=0;
    int tmp;
    int i;
    while (sp < size) {
        minpost=sp;
        for (i = sp; i < size; ++i) {
            if (a[i]<a[minpost]) {
                minpost=i;
            }
        }
        tmp=a[sp];
        a[sp]=a[minpost];
        a[minpost]=tmp;
        sp++;
    }
    return 0;
}
