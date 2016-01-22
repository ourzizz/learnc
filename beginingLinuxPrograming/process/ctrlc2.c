#include <signal.h>
#include <stdio.h>
#include <unistd.h>
void ouch(int sig)
{
    printf("OUCH! - I got signal %d\n", sig);
    /*(void) signal(SIGINT,SIG_DFL);*/
}
int main(int argc, char *argv[])
{
    struct sigaction act;
    act.sa_handler = ouch;
    sigemptyset(&act.sa_mask);
    act.sa_flags = 0;

    sigaction(SIGINT,&act,0);
    while (1) {
        printf("hello world\n");
        sleep(1);
    }
}
