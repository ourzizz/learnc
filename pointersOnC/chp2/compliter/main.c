/*#include "./increment.c"*/
/*#include "./negate.c"*/
#include <stdio.h>
int main(int argc, char *argv[])
{
    int i,j;
    i=increment(-5);
    j=negate(5);
    printf("%d\n",i);
    printf("%d\n",j);
    return 0;
}
