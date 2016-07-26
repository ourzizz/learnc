#include<iostream>
using namespace std;
#define MAX 8
int count=0;
int A[MAX][MAX]={0};
int Judge(int i,int j)
{//
    int h,f=1;
    for(h=i-1;h>=0;h--,f++)
    {
        if(A[h][j]==1)
            return 0;
        if(j-f >= 0)//
        {
            if(A[h][j-f]==1)
                return 0;
        }
        if(j+f < MAX)
        {
            if(A[h][j+f]==1)
                return 0;
        }
    }
    return 1;
}
void Queen(int i,int n)
{
    if(i>n-1)
    {
        for(int f=0;f<n;f++)
        {
            for(int j=0;j<n;j++)
                cout<<A[f][j];
            cout<<endl;
        }
        count++;
        cout<<endl;
    }
    else
    {
        for(int k=0;k<n;k++)
        {
            A[i][k]=1;
            if(Judge(i,k))//ik
                Queen(i+1,n);
            A[i][k]=0;
        }
    }

}
int main()
{
    Queen(0,MAX);
    cout<<MAX<<"Queen resolve"<<count<<endl;
    return 0;
}
