#include <iostream>
using namespace std;
void all_p(int i,int j)
{
    int k=0;
    if (i==j)
        std::cout << "A"<<i; 
    else{
        std::cout << "(";
        for (k = i; k < j; ++k) {
            all_p(i,k);
            all_p(k+1,j);
        }
        cout<< ")";
    }

}
int main(void)
{
    all_p(1,4);
    return 0;
}
