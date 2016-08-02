#include <iostream>
#include <string>

using namespace std;
typedef struct closedge
{
    string adj;
    int lowcost;
}closedge;
int main(void)
{
    closedge x[10];
    x[0].adj="chenhai";
    x[0].lowcost=1;
    std::cout << x[0].adj << std::endl;

    return 0;
}
