#include <stdio.h>
void test1()
{
    printf("chenhai\n");
    test2();
}
void test2()
{
    printf("lingsu\n");
    test1();
}
int main(void)
{
    test1();
    return 0;
}
