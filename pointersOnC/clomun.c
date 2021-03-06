/*每个输入行的后面一行是该行内容的一部分
 * 
 *
*/
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define MAX_COLS 20
#define MAX_INPUT 1000 
int read_column_numbers(int columns[],int max);
void rearrange(char *output,char const *input, int n_columns,int const columns[]);
int main()
{
    int n_columns;
    int columns[MAX_COLS];
    char input[MAX_INPUT];
    char output[MAX_INPUT];
    n_columns=read_column_numbers(columns,MAX_COLS);
    printf("%s\n", "请输入字符串");
    while(gets(input) != NULL) {
        printf("original input :%s\n", input);
        rearrange(output,input,n_columns,columns);
        printf("Rearrange line:%s\n", output);
    }

    return EXIT_SUCCESS;
}
int read_column_numbers(int columns[],int max) //读取列标号，如果超出规定范围则不予理会
{
    int num = 0;
    int ch;
    printf("%s\n", "请输入数字");
    while(num<max && scanf("%d",&columns[num])==1 && columns[num]>=0 ){
        num++;
    }
    if (num %2 != 0) {
        puts("last column number is not paired.");
        exit(EXIT_FAILURE);
    }
    while((ch = getchar()) != EOF && ch != '\n');
    return num;
}

#if 0
void rearrange(char *output,char const *input, int n_columns,int const columns[])
{
    int low = 0;
    int col = 0;
    while(low < n_columns) {
        col = columns[low];
        while(col <= columns[low+1])
        {
            /*if (input[col] != '\n') {*/
                printf("%c",input[col]);
                col++;
            /*}*/
            /*else*/
                /*break;*/
        }
        printf("%s\n", " ");
        low = low + 2;
    }
    printf("\n");
}
#endif

void rearrange(char *output,char const *input, int n_columns,int const columns[])
{
    int col;
    int output_col;
    int len;
    len = strlen(input);
    output_col = 0;
    for (col = 0; col < n_columns; col += 2) {
        int nchars =  MAX_INPUT - 1;
        if (columns[col] >= len || output_col == MAX_INPUT -1) {
            break;
        }
        if (output_col + nchars > MAX_INPUT -1) {
            nchars = MAX_INPUT - output_col -1;
        }
        strncpy(output + output_col,input + columns[col],nchars);
        output_col += nchars;
    }
}

