#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <pthread.h>

void *thread_fun(void* arg);

int main()
{
    int res;
    pthread_t thread_a;
    void *thread_result;

    res = pthread_create(&thread_a,NULL,thread_fun,NULL);
    if (res != 0) {
        perror("thread failed");
        exit(EXIT_FAILURE);
    }
    sleep(5);
    printf("Canceling thread...\n");
    res = pthread_cancel(thread_a);
    if (res != 0) {
        perror("Thread cancelation failed");
        exit(EXIT_FAILURE);
    }
    printf("Waiting for thread to finish\n");
    res = pthread_join(thread_a,&thread_result);
    if (res != 0) {
        perror("thread join failed");
        exit(EXIT_FAILURE);
    }

    exit(EXIT_SUCCESS);
}

void *thread_fun(void* arg)
{
    int i,res;
    res = pthread_setcancelstate(PTHREAD_CANCEL_ENABLE,NULL);
    if (res != 0) {
        perror("thread setcancel failed");
        exit(EXIT_FAILURE);
    }
    res = pthread_setcanceltype(PTHREAD_CANCEL_DEFERRED,NULL);
    if (res != 0) {
        perror("thread pthread_setcanceltype failed");
        exit(EXIT_FAILURE);
    }
    printf("thread_function is running\n");
    for (i = 0; i < 10; ++i) {
        printf("Thread is still running(%d)...\n", i);
        sleep(1);
    }
    pthread_exit(0);
}
