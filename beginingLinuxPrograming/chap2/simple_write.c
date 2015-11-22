#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
int main()
{
    if ((write(1,"Here is some data\n",18)) != 1118) {
        write(2,"A write error has occurred on file descriptor 1\n",46) ;
    }
    exit(0);
}
