#include <stdio.h>
#include <string.h>
#define MAX 10000
void function_name(int i)
{
    while (i<10) {
        printf("%d\n", i);
        i++;
    }
}
int main(void)
{
    function_name(0);
    printf("chenhai\n");
}
