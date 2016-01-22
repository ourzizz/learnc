#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
int main()
{
    printf("Running ps with execlp\n");
    execlp("ps","ps","ef",0);
    printf("Done\n");
    exit(0);
}
