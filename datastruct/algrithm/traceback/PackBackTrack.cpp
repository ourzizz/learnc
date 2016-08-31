#include <iostream>
#include <vector>
//这个程序只能计算出背包的最优价值，没有构造最优解
using namespace std;
class PackBackTrack
{
    public:
        PackBackTrack (vector<int>& p,vector<int>& w,int c,int n)
            :m_p(p),m_w(w),m_c(c),m_num(n)
        {
            bestValue=0;
            currentValue=0;
            currentWeight=0;
        }
        int GetBestValue()
        {
            BackTrack(0);
            //std::cout << currentWeight << std::endl;
            return bestValue;
        }
        //virtual ~PackBackTrack ();

    protected:
        vector<int> m_p;//N个背包的价格
        vector<int> m_w;//N个背包的重量
        int m_c;//背包的容量
        int m_num;//物品件数
        int bestValue;//背包最大价值
        int currentValue;//当前背包中物品的价值
        int currentWeight;//当前背包中物品的重量
    private:
        void BackTrack(int depth)
        {
            if (depth>m_num) {//递归边界触底
                if (bestValue<currentValue) {
                    bestValue=currentValue;
                }
                return;
            }
            if (currentWeight+m_w[depth] <= m_c) {//
                currentValue += m_p[depth];
                currentWeight += m_w[depth];
                BackTrack(depth+1);
                currentValue -= m_p[depth];
                currentWeight -= m_w[depth];
            }
            BackTrack(depth+1);
        }
};
int main(void)
{
    int n,c;
    n=6,c=6;
    std::vector<int> w(n);
    std::vector<int> p(n);
    w[0]=2;p[0]=6;
    w[1]=5;p[1]=1;
    w[2]=3;p[2]=3;
    w[3]=7;p[3]=4;
    w[4]=9;p[4]=2;
    w[5]=2;p[5]=9;
    PackBackTrack pack(p,w,c,n);
    int bestValue=pack.GetBestValue();
    std::cout << bestValue << std::endl;
    return 0;
}
