#define MAXSIZE 20
typedef int KeyType;
typedef struct {
    KeyType key;
}RedType;
typedef struct{
    RedType r[MAXSIZE+1]; //r[0]闲置，或者作为哨兵
    int length;
}SqList;
