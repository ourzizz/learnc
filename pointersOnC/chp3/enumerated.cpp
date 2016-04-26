#include <iostream>
using namespace std;
enum Jar_Type{CUP,PRINT,QUART,HALF_GALLON,GALLON};
enum Jar_Type mil_jug,gas_can,medicine_bottle;
int main(void)
{
    Jar_Type f = CUP;
    gas_can=GALLON;
    std::cout << gas_can << std::endl;
    std::cout << f << std::endl;
    return 0;
}
