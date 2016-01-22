#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <pthread.h>
#include <semaphore.h>

void *thread_fun(void* arg);
pthread_mutex_t work_mutex;

#define WORK_SIZE 1024
char workarea[WORK_SIZE];
int time_to_exit = 0;

int main(void)
{
    int res;
    pthread_t thread_a;
    void * thread_result;
    res = pthread_mutex_init(&work_mutex,NULL);
    if (res != 0) {
        perror("Semaphore initiation failed");
        exit(EXIT_FAILURE);
    }
    res = pthread_create(&thread_a,NULL,thread_fun,NULL);
    if (res != 0) {
        perror("thread failed");
        exit(EXIT_FAILURE);
    }

    pthread_mutex_lock(&work_mutex);
    printf("Enter some text.enter end to finish\n");
    while (!time_to_exit) {
        fgets(workarea,WORK_SIZE,stdin);
        pthread_mutex_unlock(&work_mutex);
        while (1) {
            pthread_mutex_lock(&work_mutex);
            if (workarea[0] != '\0') {
                pthread_mutex_unlock(&work_mutex);
                sleep(1);
            }
            else{
                break;
            }
        }
    }

    printf("Waiting for thread to finish...\n");
    res = pthread_join(thread_a,&thread_result);
    if (res != 0) {
        perror("Thread join failed");
        exit(EXIT_FAILURE);
    }
    printf("Join success\n");
    return 0;
}
void *thread_fun(void* arg)
{
    sleep(1);
    pthread_mutex_lock(&work_mutex);
    while (strncmp("end",workarea,3) != 0) {
        printf("You input %d characters\n", strlen(workarea) - 1);
        workarea[0] = '\0';
        pthread_mutex_unlock(&work_mutex);
        sleep(1);
        pthread_mutex_lock(&work_mutex);
        while (workarea[0] == '\0') {
            pthread_mutex_unlock(&work_mutex);
            sleep(1);
            pthread_mutex_lock(&work_mutex);
        }
    }
    time_to_exit = 1;
    workarea[0] = '\0';
    pthread_mutex_unlock(&work_mutex);
    pthread_exit(0);
}
