#include <sys/types.h>
#include <sys/wait.h>
#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
void map()
{
    int a[3][3]= {{1,2,3}, {4,5,6}, {7,8,9}};
    int **p=a;
    printf("%d\n", p[4]);
}
int main(void)
{
    /*pid_t pid;*/
    /*char *message;*/
    /*int n;*/
    /*int exit_code;*/
    /*printf("fork program starting\n");*/
    /*pid = fork();*/
    /*switch (pid) {*/
        /*case -1:*/
            /*perror("fork failed");*/
            /*exit(1);*/
        /*case 0:*/
            /*message = "Child";*/
            /*n=3;*/
            /*exit_code = 37;*/
            /*break;*/
        /*default:*/
            /*message = "Parent";*/
            /*n=5;*/
            /*exit_code = 0;*/
            /*break;*/
    /*}*/
    /*for (; n > 0; n--) {*/
        /*puts(message);*/
        /*sleep(1);*/
    /*}*/

    /*if (pid != 0) { //pid!=0表示这段代码只在父进程中执行*/
        /*int stat_val;*/
        /*pid_t child_pid;*/
        /*child_pid = wait(&stat_val);*/
        /*printf("child has finished :pid=%d\n", child_pid);*/
        /*if (WIFEXITED(stat_val)) {*/
            /*printf("Child exited with code %d\n",WEXITSTATUS(stat_val));*/
        /*}*/
        /*else*/
            /*printf("Child terminated abnormally\n");*/
    /*}*/
    /*exit(exit_code);*/
    /*map();*/
    int i;
    for (i = 0; i < 2; ++i) {
        printf("chenhai在fork之前\n");
        fork();
        printf("a\n");
    }
    return 0;
}
