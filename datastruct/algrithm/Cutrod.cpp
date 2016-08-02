#include <iostream>
#include <malloc.h>
using namespace std;

//带备忘录的自顶向下求解切钢条，求解最优解的c实现版本
int MEMOIZED_CUT_ROD_AUX(int *p,int n,int *r)
{
    if ( r[n] >= 0)
    {
        return r[n];
    }
    int q;
    if (n==0) {
        q = 0;
    }
    else 
    {
        q=-1;
        for (int i = 1; i <= n; ++i) {
            int tmp = p[i] + MEMOIZED_CUT_ROD_AUX(p,n-i,r);
            q=(q>tmp)?q:tmp;
        }
    }
    std::cout << q << std::endl;
    r[n]=q;
    return q;
}

int MEMOIZED_CUT_ROD(int *p,int n)
{
    int *r =  new int[n];
    for (int i=0;i<=n;i++) {
        r[i]=-1;
    }
    return MEMOIZED_CUT_ROD_AUX(p,n,r);
}
int main(void)
{
    int p[11] = { 0,1 , 5 , 8 , 9 , 10, 17, 17, 20, 24, 30};
    std::cout << MEMOIZED_CUT_ROD(p,9)<< std::endl;
    return 0;
}
