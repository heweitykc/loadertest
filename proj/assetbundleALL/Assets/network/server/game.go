package main

import(
//	"fmt"
//	"os"
	"net"
)

type GameObject struct{
	x float32
	y float32
	width float32
	height float32
	name string	
}

type GamePlayer struct{
	GameObject
	netconn net.Conn
}

type GameScene struct{
	GameObject	
	children []*GamePlayer
}

func (m *GameScene) AddObj(newobj *GamePlayer){
	m.children = append(m.children, newobj)
}

func (m *GameScene) GetObj(name string) *GamePlayer{
	for _, player := range m.children {
		if player.name == name{
			return player
		}			
	}
	return nil
}

func (m *GameScene) Shoot(name string){
	//player := m.GetObj(name)
	
}