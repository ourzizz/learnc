#include<stdlib.h>
#include<stdio.h>
#include "a.h"
extern void function_two();
extern void function_three();
int main(void)
{
    function_two();
    function_three();
    printf("chenhai\n");
    printf("hello world\n");
    exit(EXIT_SUCCESS);;
    printf("chenhai\n");
}
