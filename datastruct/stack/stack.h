#define STACK_INIT_SIZE 100
#define STACKINCREMENT 10
#define CHENHAI 9
typedef struct{
    SElemType * top;
    SElemType * base;
    int stacksize;
}SqStack;
int InitStack(SqStack *s)//构造一个空栈
{
    S->base=(SElemType *)malloc(STACK_INIT_SIZE * sizeof(SElemType));
    if (!s->base)
        exit(OVERFLOW);
    s->top = s->base;
    s->stacksize=STACK_INIT_SIZE;
    return 0;
}
int DestroyStack(SqStack &s);

