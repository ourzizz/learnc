#include "client.h"
#include <ctype.h>
#include <pthread.h>
int main(void)
{
    int server_fifo_fd,client_fifo_fd;
    struct data_to_pass_st my_data;
    int read_res;
    char client_fifo[256];
    pid_t client_pid=getpid();
    my_data.client_pid=client_pid;
    my_data.some_data = "chenhai";

    sprintf(client_fifo,CLIENT_FIFO_NAME,my_data.client_pid);
    mkfifo(CLIENT_FIFO_NAME,0777);

    server_fifo_fd = open(SERVER_FIFO_NAME,O_WRONLY);//向服务器管道写入数据
    write(server_fifo_fd,&my_data,sizeof(my_data));

    client_fifo_fd=open(CLIENT_FIFO_NAME,O_RDONLY);
    memset(my_data.some_data,'\0',sizeof(my_data.some_data));
    read_res = read(client_fifo_fd,&my_data,sizeof(my_data));
    printf("%s\n", my_data.some_data);
}
