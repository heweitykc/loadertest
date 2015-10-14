package main

import (
	"fmt"
	"net/http"
	"runtime"
	"sync"
)

func main() {
    fmt.Println("num:", runtime.NumGoroutine())
    waitGroup := sync.WaitGroup{}
    for i := 0; i < 100; i++ {
        waitGroup.Add(1)
        go func(i int, wait *sync.WaitGroup) {
            defer func() {
                wait.Done()
            }()
            Println(i)
        }(i, &waitGroup)
    }
    waitGroup.Wait()
    fmt.Println("num:",runtime.NumGoroutine())
}
func Println(i int) {
    println(i)
    client := http.Client{}
    resp, err := client.Get("http://www.baidu.com")
	if err != nil{
		return
	}
	defer resp.Body.Close()
}

