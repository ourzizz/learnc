#include <malloc.h>
#define STACK_INIT_SIZE 100
#define STACKINCREMENT 10
#define CHENHAI 9
typedef int SElemType;
typedef struct{
    SElemType * top;
    SElemType * base;
    int stacksize;
}SqStack;

int InitStack(SqStack *s)//构造一个空栈
{
    s->base=(SElemType *)malloc(STACK_INIT_SIZE * sizeof(SElemType));
    if (!s->base)
        return 0;
    s->top = s->base;
    s->stacksize=STACK_INIT_SIZE;
    return 0;
}
int GetTop(SqStack s,SElemType &e)
{
    if (s.top==s.base) 
        return 0;
    e=*(s.top-1);
    return 1;
}
int Push(SqStack *s,SElemType e)
{
    if (s->top - s->base >= s->stacksize) {
       s->base=(SElemType*)realloc(s->base,(s->stacksize+STACKINCREMENT)*sizeof(SElemType));
       if (!s->base) return 0; 
       s->top = s->base+s->stacksize;
       s->stacksize += STACKINCREMENT;
    }
    *(s->top)= e;
    (s->top)++;
    return 1;
}

int Pop(SqStack *s,SElemType *e)
{
    if (s->top == s->base) return 0;
    *e=*s->top;
    s->top--;
    return 1;
}
