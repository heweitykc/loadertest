package main

import (
        "fmt"
        "time"
		"runtime"
)

func c()(i int){
	defer func(){i++}()
	return 1
}

func main() {
	c := make(chan int)
	fmt.Printf("%s\n", c)
	go do(c)
	go ret(c)
	fmt.Println("now: ",runtime.NumGoroutine())
	var input string
	fmt.Scanln(&input)
	fmt.Println("End")
}

func do(c chan int){
	time.Sleep(time.Duration(0.5*float32(time.Second)))
	c <- 100
	fmt.Println("doend")
}

func ret(c chan int){
	time.Sleep(time.Duration(1*float32(time.Second)))
	fmt.Println("ret start")
	select{
		case num := <- c :
			fmt.Println("ret = ", num)
		case <- time.After(time.Second):
			fmt.Println("timeout")
	}
}
