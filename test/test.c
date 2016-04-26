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
    int i;
    for (i = 0; i < 5;
            ++i) {
        printf("%d\n", i);
    }
}
