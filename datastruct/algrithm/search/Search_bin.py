import time
start=time.clock()

def Bin_search(x,search): #import pdb; pdb.set_trace()
    mid=len(search)/2
    low=0
    high=len(search)
    while high>low:
        mid=(high+low)/2
        print low,mid,high
        if x==search[mid]:
            return mid
        elif x>search[mid]:
            low=mid+1
        else:
            high=mid-1
    return 0

def sq_search(x,search):
    i=len(search)-1
    while i!=0:
        if x==search[i]:
            return i
        i=i-1
    return i
        
if __name__ == "__main__":
    x=1
    search=[i for i in range(90000)]
    y=sq_search(x,search)
    #y=Bin_search(x,search)
    end=time.clock()
    print y
    print end-start
