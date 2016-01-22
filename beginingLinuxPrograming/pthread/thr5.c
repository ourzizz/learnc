#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <pthread.h>

void *thread_fun(void* arg);
char message[] = "helloworld";
int thread_finished = 0;

int main()
{
    int res;
    pthread_t thread_a;

    pthread_attr_t thread_attr;

    res = pthread_attr_init(&thread_attr);
    if (res != 0) {
        perror("Attribute creation failed");
        exit(EXIT_FAILURE);
    }
    res = pthread_attr_setdetachstate(&thread_attr,PTHREAD_CREATE_DETACHED);
    if (res != 0) {
        perror("setting detached attribute failed");
        exit(EXIT_FAILURE);
    }

    res = pthread_create(&thread_a,&thread_attr,thread_fun,(void *)message);
    if (res != 0) {
        perror("thread failed");
    }
    (void)pthread_attr_destroy(&thread_attr);

    while (!thread_finished) {
        printf("wating for thread to say it's finished.....\n");
        sleep(1);
    }
    printf("Other thread finished,bye\n");
    exit(EXIT_SUCCESS);
}
void *thread_fun(void* arg)
{
    printf("thread_fun is running.Argument was %s\n",(char *)arg);
    sleep(4);
    printf("Second thread setting finished flag,and exiting now\n");
    thread_finished = 1;
    pthread_exit(NULL);
}
