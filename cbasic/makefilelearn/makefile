all: myapp
#which compiler
CC = gcc
#where are include files kept
INCLUDE = .

INSTDIR = /usr/local/bin

#options for development
CFLAGS = -O -Wall -ansi

myapp: main.o 2.o 3.o
	$(CC) -o myapp main.o 2.o 3.o
main.o: main.c a.h
	$(CC) -I$(INCLUDE) $(CFLAGS) -c main.c
2.o: 2.c a.h b.h
	$(CC) -I$(INCLUDE) $(CFLAGS) -c 2.c
3.o: 2.c a.h b.h
	$(CC) -I$(INCLUDE) $(CFLAGS) -c 3.c
clean:
	-rm main.o 2.o 3.o
install: myapp
	@if [ -d ${INSTDIR} ] ; \
		then \
		cp myapp ${INSTDIR}; \
		chmod a+x ${INSTDIR}/myapp; \
		chmod og-w ${INSTDIR}/myapp; \
		echo "Installed in ${INSTDIR}"; \
	else \
		echo "sorry,${INSTDIR} does not exist"; \
	fi

#myapp:main.o
#main.o: main.c a.h
#2.o: 2.c a.h b.h
#3.o: 3.c b.h c.h
