#include <iostream>
#include <string>
using namespace std;
void suforder(string s1,string s2)
{
    if (s1.length() < 2) {
        std::cout << s1 << std::endl;
    }
    else
    {
        char point = s1[0];
        int i=0;
        while(point != s2[i]) i++;
        string strtmp1,strtmp2,strtmp3,strtmp4;
        strtmp1.append(s1,1,i);
        strtmp2.append(s2,0,i);
        strtmp3.append(s1,i+1,s1.length()-i);
        strtmp4.append(s2,i+1,s2.length()-i);

        suforder(strtmp1,strtmp2);
        suforder(strtmp3,strtmp4);
        std::cout << s1[0] << std::endl;

    }
}
int main()
{
    string s1="ABDECFGX";
    string s2="DBEAECGX";
    suforder(s1,s2);
    return 0;
}
