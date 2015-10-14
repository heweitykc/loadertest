package main

import(
	"fmt"
	"time"
)

const(
	count = 10
)

func main(){
	chs := make([]chan int, count)
	
	for i,_:=range(chs){
		chs[i] = make(chan int)
		fmt.Printf("go (%d, %s)\n", i, chs[i])
		go do(i, chs[i])
	}
	
	for i, ch := range(chs){
		v := <- ch
		time.Sleep(time.Second)
		fmt.Printf("chan[%d]->%d\n",i,v)
	}
}

func do(i int, ch chan int){
	fmt.Printf("%d->chan[%d]\n",i,i);
	ch <- i
}