#include <stdio.h>
void f(char *p)
{
    char* q=p;
    int j=0,i=0;
    while(*q != '\0')
    {
        if(*q == '{')
        {
            i++;
        }
        if (*q == '}') {
            j++ ;
        }
        if (j>i) {
            printf("不匹配\n");
            return ;
        }
        q++;
    }
    if(i == j) {
        printf("匹配\n");
    }
    else{
        printf("不匹配\n");
    }

}
int main(void)
{
    f("chenhai{ah}{}sdf{sd}");
    f("{{}}}{");
    return 0;
}

