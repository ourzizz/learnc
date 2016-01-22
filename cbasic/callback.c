void PrintfText_A(char* s) 
{
    printf("1\n");
    printf(s);
}

void PrintfText_B(char* s) 
{
    printf("2\n");
    printf(s);
}

//定义实现带参回调函数的"调用函数"
void CallPrintfText(void (*callfuct)(char*),char* s)
{
    callfuct(s);
}

//在main函数中实现带参的函数回调
int main(int argc,char* argv[])
{
    CallPrintfText(PrintfText_A,"Hello World!\n");
    CallPrintfText(PrintfText_B,"Hello World!\n");
    return 0;
}
