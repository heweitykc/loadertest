package main

import(
	"fmt"
	"os"
	"net"
)

func checkError(err error,info string) (res bool) {
	if(err != nil){
		fmt.Println(info+"  " + err.Error())
		return false
	}
	return true
}

func Handler(conn net.Conn,messages chan string){
	fmt.Println("connection is connected from ...",conn.RemoteAddr().String())
	buf := make([]byte,1024)
	for{
		lenght, err := conn.Read(buf)
		if(checkError(err,"Connection")==false){
			conn.Close()
			break
		}
		if lenght > 0{
			buf[lenght]=0
		}
		fmt.Println("Rec[",conn.RemoteAddr().String(),"] Say :" ,string(buf[0:lenght]))
		reciveStr := conn.RemoteAddr().String()+","+string(buf[0:lenght])
		messages <- reciveStr
	}
}

func echoHandler(conns *map[string]net.Conn,messages chan string){
	for{
		msg:= <- messages
		fmt.Println(msg)
		
		for key,value := range *conns {
			//fmt.Println("connection is connected from ...",key)
			_,err :=value.Write([]byte(msg))
			if(err != nil){
				fmt.Println(err.Error())
				delete(*conns,key)
			}
		}
	}
}

func StartServer(port string){
		service:=":"+port //strconv.Itoa(port);
		tcpAddr, err := net.ResolveTCPAddr("tcp4", service)
		checkError(err,"ResolveTCPAddr")
		l,err := net.ListenTCP("tcp",tcpAddr)
		checkError(err,"ListenTCP")
		conns:=make(map[string]net.Conn)
		messages := make(chan string,10)
		//启动服务器广播线程
		go echoHandler(&conns,messages)
		
		for  {
			fmt.Println("Listening ...")
			conn,err := l.Accept()
			checkError(err,"Accept")
			fmt.Println("Accepting ...")
			conns[conn.RemoteAddr().String()]=conn
			//启动一个新线程
			go Handler(conn,messages) 
		}
}


//	启动服务器端：  Chat server [port]	    eg: Chat server 9090
func main(){
	if len(os.Args)!=3  {	
		fmt.Println("Wrong pare")
		os.Exit(0)
	}
	
	if os.Args[1]=="server" && len(os.Args)==3 {
		
		StartServer(os.Args[2])
	}
	
	if os.Args[1]=="client" && len(os.Args)==3 {
		
		StartClient(os.Args[2])
	}
}

////////////////////////////////////////////////////////
//
//客户端发送线程
//参数
//		发送连接 conn
//
////////////////////////////////////////////////////////
func chatSend(conn net.Conn){
	
	var input string
	username := conn.LocalAddr().String()
	for {
		
		fmt.Scanln(&input)
		if input == "/quit"{
			fmt.Println("ByeBye..")
			conn.Close()
			os.Exit(0);
		}
		
		
		lens,err :=conn.Write([]byte(username + " Say :::" + input))
		fmt.Println(lens)
		if(err != nil){
			fmt.Println(err.Error())
			conn.Close()
			break
		}
		
	}
	
}

////////////////////////////////////////////////////////
//
//客户端启动函数
//参数
//		远程ip地址和端口 tcpaddr
//
////////////////////////////////////////////////////////
func StartClient(tcpaddr string){
	
	tcpAddr, err := net.ResolveTCPAddr("tcp4", tcpaddr)
	checkError(err,"ResolveTCPAddr")
	conn, err := net.DialTCP("tcp", nil, tcpAddr)
	checkError(err,"DialTCP")
	//启动客户端发送线程
	go chatSend(conn)	
	
	//开始客户端轮训
	buf := make([]byte,1024)
	for{
		
		lenght, err := conn.Read(buf)
		if(checkError(err,"Connection")==false){
			conn.Close()
			fmt.Println("Server is dead ...ByeBye")
			os.Exit(0)
		}
		fmt.Println(string(buf[0:lenght]))
		
	}
}