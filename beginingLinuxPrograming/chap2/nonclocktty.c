#include <unistd.h>
#include <fcntl.h>
#include <errno.h>
#include <string.h>
#include <stdlib.h>
 
#define MSG_TRY "try again\n"
int main(void)
{
    char buf[10];
    int fd,n;
    fd=open("/dev/tty",O_RDONLY|O_NONBLOCK);
    if (fd<0) {
        perror("open /dev/tty");
        exit(1);
    }
    return 0;
}
