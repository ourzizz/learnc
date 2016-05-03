/*
 ============================================================================
 Name        : mysql_test.c
 Author      : 
 Version     :
 Copyright   : Your copyright notice
 Description : Hello World in C, Ansi-style
 python虽然开发快但是不稳定，他妈的字符集有所改变就他妈的玩玩
 ============================================================================
 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <iostream>

#include <mysql/mysql.h>
using namespace std;

MYSQL *g_conn; // mysql 连接
 MYSQL_RES *g_res; // mysql 记录集
 MYSQL_RES *g_res_post; // mysql 记录集
 MYSQL_ROW g_row; // 字符串数组，mysql 记录行
 MYSQL_ROW g_row_post; // 字符串数组，mysql 记录行
 
#define MAX_BUF_SIZE 1024 // 缓冲区最大字节数

const char *g_host_name = "localhost";
const char *g_user_name = "root";
const char *g_password = "123123";
const char *g_db_name = "gwycf";
const unsigned int g_db_port = 3306;

void print_mysql_error(const char *msg) { // 打印最后一次错误
    if (msg)
        printf("%s: %s\n", msg, mysql_error(g_conn));
    else
        puts(mysql_error(g_conn));
}

int executesql(const char * sql) {
    /*query the database according the sql*/
    if (mysql_real_query(g_conn, sql, strlen(sql))) // 如果失败
        return -1; // 表示失败

    return 0; // 成功执行
}


int init_mysql() { // 初始化连接
    // init the database connection
    g_conn = mysql_init(NULL);

    /* connect the database */
    if(!mysql_real_connect(g_conn, g_host_name, g_user_name, g_password, g_db_name, g_db_port, NULL, 0)) // 如果失败
        return -1;

    // 是否连接已经可用
    if (executesql("set names utf8")) // 如果失败
        return -1;

    return 0; // 返回成功
}


int main(void) {
    puts("!!!Hello World!!!"); /* prints !!!Hello World!!! */
    int paiming=0;
    char * pzongfen= (char*)malloc(sizeof(char)*30);
    char * test= (char*)malloc(sizeof(char)*30);

    if (init_mysql())
        print_mysql_error(NULL);

    char sql[MAX_BUF_SIZE];
    //sprintf(sql, "INSERT INTO `test`(`name`) VALUES('testname')");
    sprintf(sql, "select * from info");

    //if (executesql(sql))
        //print_mysql_error(NULL);

    //if (executesql("SELECT * FROM `info` where name='陈海'")) // 句末没有分号
    if (executesql("select distinct epost,ecode from info")) // 句末没有分号
        print_mysql_error(NULL);

    g_res = mysql_store_result(g_conn); // 从服务器传送结果集至本地，mysql_use_result直接使用服务器上的记录集

    int iNum_rows = mysql_num_rows(g_res); // 得到记录的行数
    int iNum_fields = mysql_num_fields(g_res); // 得到记录的列数

    printf("共%d个职位，每个记录%d字段\n", iNum_rows, iNum_fields);

    puts("id\tname\n");
    string x;
    std::cout << "开始" << std::endl;
    while ((g_row=mysql_fetch_row(g_res))) // 打印结果集
    {
        sprintf(sql,"select name,kaohao,zongfen from info where epost='%s' and ecode='%s' order by zongfen desc",g_row[0],g_row[1]);
        if (executesql(sql)) // 句末没有分号
        {
            print_mysql_error(NULL);
        }

        g_res_post = mysql_store_result(g_conn);
        while((g_row_post=mysql_fetch_row(g_res_post))) {
            strcpy(test,g_row_post[2]);
            if (strcmp(pzongfen,test)) //strcmp l1==l2 return 0;l1<l2 return -;l1>l2 return +
                paiming ++;
            sprintf(sql,"update info set rank='%d' where name='%s' and kaohao='%s'",paiming,g_row_post[0],g_row_post[1]);
            strcpy(pzongfen,g_row_post[2]);
            executesql(sql);
            std::cout << g_row_post[0];
            std::cout << paiming << std::endl;
            if (!(strcmp(pzongfen,"-1.0"))){
                std::cout << "缺考" << std::endl;
                break;
            } 
        }
        strcpy(pzongfen,"0");
        paiming=0;
    }
    std::cout << "结束" << std::endl;
    mysql_free_result(g_res); // 释放结果集
    mysql_close(g_conn); // 关闭链接

    return EXIT_SUCCESS;
}
