//go routine学习

package main

import (
 //       "net"
        "fmt"
 //       "os"
        "time"
)

var c chan int

func dofunc(w string, sec int64){
	time.Sleep(time.Duration(sec) * time.Second)
	fmt.Println(w, "is ready!")
	sec += int64(10)
	c <- int(sec)
}

func main() {
	c = make(chan int)
	go dofunc("Tea", 2)
	go dofunc("Coffee", 1)
	fmt.Println("Im waiting.",time.Second)
	msg := <-c	
	fmt.Println("Exit1...", msg)
}
