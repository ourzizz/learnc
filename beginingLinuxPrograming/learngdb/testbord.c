#include <stdio.h>
typedef struct personal
{
    int age;
    char* name;

}personal;
typedef struct node {
    struct node * next;
};
int main(void)
{
    personal chenhai;
    chenhai.age=30;
    chenhai.name="CHENHAI";
    printf("%s\n", chenhai.name);
    return 0;
}
