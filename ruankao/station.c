#include <stdio.h>
typedef struct stack{ }STACK;
void InitStack(STACK *s);
void Push(STACK *s,int e);
void Pop(STACK *s);
int Top(STACK s);
int IsEmpty(STACK s);
int main(void)
{
    STACK station;
    int state[1000];
    int n;
    int begin,i,j,maxNo;
    printf("请输入车厢数： \n");
    scanf("%d",&n);
    printf("请输入车厢数： \n");
    for (i = 0; i < n; ++i) {
        scanf("%d",&state[i]);
    }
    InitStack(&station);
    maxNo=1;
    for (i = 0; i < n;) {//检查输出序列中的每个车厢号state[i]是否能从栈中获取
        if(!IsEmpty(station))//当栈不为空
        {
            if (state[i]==Top(station)) {
                printf("%d \n",Top(state));
                Pop(&station);
                i++;
            }
            else if (state[i] < Top(station)){
                printf("error\n");
                return 1;
            }
            else{  //state[i] > Top(station)
                begin = maxNo+1;
                for (j = begin+1; j < state[i]; ++j) {
                    Push(&station,j);
                }
            }

        }
        else{/* 当栈为空*/
            begin=maxNo;
            for (j = begin; j < state[i]; ++j) {
                Push(&station,j);
            }
            maxNo=j;
        }
    }
    printf("OK\n");
    return 0;
}
