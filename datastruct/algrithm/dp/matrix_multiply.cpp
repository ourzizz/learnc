#include <iostream>
using namespace std;
#define MAX 10
typedef struct{
    int rows;
    int clus;
}Matrix;
Matrix p[MAX];
int m[MAX][MAX];
int s[MAX][MAX];
int Matrix_muti_play(int m[MAX][MAX],int s[MAX][MAX],Matrix *p,int length)
{
    int l,i,q,n,j,k;
    n=length - 1;
    for (i = 0; i <= n; ++i) {
        for (j = 0; j < n; ++j) {
            if (i==j) {
                m[i][j]=0;
            }
        }
    }
    for (l = 2; l < n; ++l) {
        for (i = 1; i < n-l+1; ++i) {
            j=i+l-1;
            m[i][j]=65536;
            for (k = 0; k < j-1; ++k) {
                q=m[i][k]+m[k+1][j]+p[i].rows*p[k].clus*p[j].clus;
                if (q<m[i][j]) {
                    m[i][j]=q;
                    s[i][j]=k;
                }
            }
        }
    }
    return 0;
}
int main(void)
{
    
    p[0].rows=30,p[0].clus=35;
    p[1].rows=35,p[1].clus=15;
    p[2].rows=15,p[2].clus=5;
    p[3].rows=5,p[3].clus=10;
    p[4].rows=10,p[4].clus=20;
    Matrix_muti_play(m,s,p,5);
    return 0;
}
