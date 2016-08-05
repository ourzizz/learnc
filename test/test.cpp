#include <iostream>
#include <string>
#include <stack>

using namespace std;
typedef struct closedge
{
    string adj;
    int lowcost;
}closedge;
int main(void)
{
    int a;
    std::stack<int>  s;
    s.push(1);
    s.push(2);
    s.push(3);
    s.push(4);
    s.push(5);
    s.push(6);
    s.push(0);
    //while(!s.empty()){
        //a=s.top();
        //std::cout << a << std::endl;
        //s.pop();
    //}
    char b='2';
    int c;
    c=int(b);
    std::cout << c<< std::endl;
    std::cout << 2+b-'0'  << std::endl;
    return 0;
}
