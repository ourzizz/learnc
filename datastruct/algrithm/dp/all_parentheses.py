def all_p(i,j):
    if i==j:
        print "A"+str(i),
    else:
        #import pdb; pdb.set_trace()
        for k in range(i,j):
            print "(",
            all_p(i,k)
            all_p(k+1,j)
            print ")",
        print "+",

stra=" "
#def all_p(i,j,str):
    #if i==j:
        #stra=stra+ "A"+str(i),
    #else:
        #for k in range(i,j):
            #stra=stra+"("
            #all_p(i,k)
            #all_p(k+1,j)
            #stra=stra+")"

if __name__ == "__main__":
    #all_p(1,4,stra)
    all_p(1,4)
