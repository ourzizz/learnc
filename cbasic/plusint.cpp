#include<iostream>
int main(void)
{
    int a=0;
    std::cin >> a;
    int temp = 0;
    while (a) {
        temp+=(a%10);
        a = a/10;
    }
    std::cout << temp << std::endl;
    return 0;
}
