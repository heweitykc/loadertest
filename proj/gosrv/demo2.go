/*
package main

import(
	"fmt"
	"time"
	"math/rand"
)

func sell(c chan int){
	for{
		num := <- c
		fmt.Println("Sell", num, "bread")
	}
}

func produce(c chan int){
	for{
		num := rand.Intn(10)
		t := time.Duration(num)
		fmt.Println("Product ", num, " bread")
		c <- num
		time.Sleep(time.Second * t)
	}
}

func main(){
	var c chan int = make(chan int)
	go sell(c)
	go produce(c)
	var input string
	fmt.Scanln(&input)
	fmt.Println("End")
}*/

package main
import "fmt"

func print(ch chan int, i int) {
	ch<- 1
    fmt.Println("Hello world", i)
}

func main() {
    chs := make([]chan int, 10)
    for i := 0; i < 10; i++ {
        chs[i] = make(chan int)
        go print(chs[i], i)
    }
    
    for _, ch := range(chs){
        <-ch
    }
}
