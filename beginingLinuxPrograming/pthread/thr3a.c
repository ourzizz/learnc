#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <pthread.h>
#include <semaphore.h>
void *thread_fun(void* arg);
sem_t bin_sem;
#define WORK_SIZE 1024
char workarea[WORK_SIZE];
int main(void)
{
    int res;
    pthread_t thread_a;
    void * thread_result;
    res = sem_init(&bin_sem,0,0);
    if (res != 0) {
        perror("Semaphore initiation failed");
        exit(EXIT_FAILURE);
    }
    res = pthread_create(&thread_a,NULL,thread_fun,NULL);
    if (res != 0) {
        perror("thread failed");
        exit(EXIT_FAILURE);
    }
    printf("Input some text.Enter end to finish...\n");
    while (strncmp("end",workarea,3) != 0) {
        if (strncmp(workarea,"FAST",4) == 0) {
            sem_post(&bin_sem);
            strcpy(workarea,"123456789");
        }
        else{
            fgets(workarea,WORK_SIZE,stdin);
        }
        sem_post(&bin_sem);
    }
    printf("Waiting for thread to finish...\n");
    res = pthread_join(thread_a,&thread_result);
    if (res != 0) {
        perror("Thread join failed");
        exit(EXIT_FAILURE);
    }
    printf("Join success\n");
    sem_destroy(&bin_sem);
    return 0;
}
void *thread_fun(void* arg)
{
    sem_wait(&bin_sem);
    while (strncmp("end",workarea,3) != 0) {
        printf("You input %d characters\n", strlen(workarea) - 1);
        printf("%s\n", workarea);
        sem_wait(&bin_sem);
    }
    pthread_exit(NULL);
}
