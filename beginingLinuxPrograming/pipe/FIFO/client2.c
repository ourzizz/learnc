#include <ctype.h>
#include "client.h"
int main(void)
{
    int server_fifo_fd,client_fifo_fd;
    struct data_to_pass_st my_data;
    int times_to_send;
    char client_fifo[256];

    server_fifo_fd = open(SERVER_FIFO_NAME,O_WRONLY);//客户端打开服务器端的管道
    if (server_fifo_fd == -1) {
        fprintf(stderr, "Sorry,no server\n");
        exit(EXIT_SUCCESS);
    }
    my_data.client_pid = getpid();

    sprintf(client_fifo,CLIENT_FIFO_NAME,my_data.client_pid);
    if (mkfifo(client_fifo,0777) == -1) {
        fprintf(stderr, "Sorry,can't make %s\n",client_fifo);
        exit(EXIT_SUCCESS);
    }

    for (times_to_send = 0; times_to_send < 5; times_to_send++) {
        sprintf(my_data.some_data,"Hello from %d",my_data.client_pid);
        write(server_fifo_fd,&my_data,sizeof(my_data));//向服务器端管道写入数据
        client_fifo_fd = open(client_fifo,O_RDONLY);//打开客户端管道读数据
        if (client_fifo_fd != -1) {
            if (read(client_fifo_fd,&my_data,sizeof(my_data)) > 0) {
                printf("received:%s\n", my_data.some_data);
            }
            close(client_fifo_fd);
        }
    }
    close(server_fifo_fd);
    unlink(client_fifo);
    return 0;
}
