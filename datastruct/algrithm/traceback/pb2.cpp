#include <iostream>  
#include <vector>  
  
using namespace std;  
  
class PackBackTrack  
{  
  
protected:  
    vector<int> m_p; //N个背包的价格  
    vector<int> m_w; //N个背包的重量  
    int         m_c; //背包的容量  
    int         m_num; //物品的件数  
    int         bestValue;          //背包最大价值  
    int         currentValue;       //当前背包中物品的价值  
    int         currentWeight;      //当前背包中物品的重量  
  
private:  
    //辅助函数，用于回溯搜索  
    void BackTrack(int depth)  
    {  
        if(depth >= m_num)    //达到最大深度  
        {  
            if(bestValue < currentValue)  //保存最优解  
                bestValue = currentValue;  
            return ;  
        }  
  
        if(currentWeight +m_w[depth] <= m_c)  //是否满足约束条件  
        {  
            currentWeight += m_w[depth];  
            currentValue += m_p[depth];  
              
            //选取了第i件物品  
            BackTrack(depth+1); //递归求解下一个结点  
              
            //恢复背包的容量和价值  
            currentWeight -= m_w[depth];  
            currentValue  -= m_p[depth];  
        }  
        //不取第i件物品  
        BackTrack(depth+1);  
    }  
  
public:  
    //构造函数  
    PackBackTrack();  
    PackBackTrack(vector<int>& p,vector<int>& w, int c,int n)  
        :m_p(p),m_w(w),m_c(c),m_num(n)  
    {  
        bestValue =0;  
        currentValue =0;  
        currentWeight =0;  
    }  
  
    //获取背包内物品的最大值  
    int GetBestValue()  
    {  
        BackTrack(0);  
        return bestValue;  
    }  
};  
  
  
int main(void)  
{  
    //测试程序  
    int n;  
    int c;  
  
    cout << "请输入物品的件数" << endl;  
    cin >>n;  
    cout << "请输入背包的容量" << endl;  
    cin >>c;  
    vector<int> w(n);  
    vector<int> p(n);  
  
    cout << "请输入物品的重量:" << endl;  
    for(int i=0;i<n;++i)  
        cin >> w[i];  
    cout << "请输入物品的价格:" << endl;  
    for(int j=0;j<n;++j)  
        cin >> p[j];  
  
    PackBackTrack pack(p,w,c,n);  
  
    int bestValue = pack.GetBestValue();  
  
    cout << "背包内的物品的最大价值为：" << bestValue << endl;  
  
    return 0;  
}  
