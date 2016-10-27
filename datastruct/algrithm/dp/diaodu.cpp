#include <stdio.h>
int n,m;
int a[60],b[60],p[100][100][60];
void read()/*{{{*/
{
    int sum=0,sum2=0,i;
    n=6;
    a[0]=2; a[1]=5; a[2]=7; a[3]=10; a[4]=5; a[5]=2;
    b[0]=3; b[1]=8; b[2]=4; b[3]=11; b[4]=3; b[5]=4;
    for (i = 0; i < 6; ++i) {
        sum=sum+a[i];
        sum2=sum+b[i];
    }
    m=(sum<sum2)?sum:sum2;
}/*}}}*/
void schedule(){
    int x,y,k;/*{{{*/
    for (x = 0;x <= m; ++x) {
        for (y = 0;y <= m; ++y) {
            p[x][y][0] = 1;
            for (k = 1;k <= n; ++k) {
                p[x][y][k] = 0;
            }
        }
    }
    for (k = 1;k <= n; ++k) {
        for (y = 0;y <= m; ++y) {
            for (x = 0;x <= m; ++x) {
                if (x-a[k-1]>=0) p[x][y][k] =p[x-a[k-1]][y][k-1];
                if (y-b[k-1]>=0) p[x][y][k] =(p[x][y][k] || p[x][y-b[k-1]][k-1]);
            }
        }
    }/*}}}*/
}
void write(){
    int x,y,temp,max=m;/*{{{*/
    for (x = 0;x <= m; ++x) {
        for (y = 0;y <= m; ++y) {
            if (p[x][y][n]) {
                temp=( x>=y )?x:y;
                if (temp < max) max = temp;
            }
        }
    }
    printf("\n%d\n", max);/*}}}*/
}
int main()
{
    read();
    schedule();
    write();
    return 0;
}
