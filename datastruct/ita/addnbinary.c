#include <stdio.h>

int main(void)
{
    int a[10]={1,1,1,1,0,0,0,0,1,1};
    int b[10]={1,0,1,0,0,1,0,1,1,0};
    int c[11];
    int n = sizeof(a)/sizeof(int);
    int i,jw=0;
    for (i = 0; i <= n; ++i) {
        if (i!=n) {
            if (a[i]+b[i] ==2 ) {
                c[i] = jw;
                jw=1;
            }
            else if (a[i]+b[i] == 1 ) {
                if (jw!=0) {
                    c[i] = 0;
                }
                else{
                    c[i] = 1;
                }
            }
            else if(a[i]+b[i] == 0 ){
                c[i]=jw;
                jw=0;
            }
        }
        else{
            c[i]=jw;
        }
    }
    return 0;

}
