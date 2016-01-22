#include <signal.h>
#include <stdio.h>
#include <unistd.h>
void ouch(int sig)
{
    printf("OUCH! - I got signal %d\n", sig);
    (void) signal(SIGINT,SIG_DFL);
}
int main(int argc, char *argv[])
{
    (void) signal(SIGINT,ouch);
    while (1) {
        printf("hello world\n");
        sleep(1);
    }
}
