/* this is an important file 
filename:$RCSfile: test.c,v $
 */
#include <stdio.h>
#include <stdlib.h>
int add_number(int n)
{
    static int i = 100;
    i+=n;
    return i;
}
int main(void)
{
    int k = add_number(100);
    k+=add_number(100);
    printf("%d\n", k);
    return 0;
    /*int i,j;*/
    /*i=1;*/
    /*j=10;*/
    /*do {*/
        /*if (i++>--j) {*/
            /*continue;*/
        /*}*/
    /*} while (i<5);*/
    /*printf("%d,%d\n",i,j);*/
    /*return 0;*/
}
