#include <stdio.h>
#define MAXSIZE 100
int decimal2hex(int decimal,int hex[],int size)
{
    int tmp=decimal;
    int i=0;
    while ( tmp != 0)
    {
        hex[i] = tmp%8;
        i++;
        tmp = tmp/8;
    }
    return i;
}
int main(void)
{
    int hex[MAXSIZE];
    int i = decimal2hex(20,hex,MAXSIZE)-1;
    for ( ; i >= 0 ;i--) {
        printf("%d", hex[i]);
    }
    return 0;
}
