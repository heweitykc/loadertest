package main

import(
//	"fmt"
//	"os"
)

type GameObject struct{
	x float32
	y float32
	width float32
	height float32
	name string
	children []GameObject
}

type GamePlayer struct{
	GameObject
}

type GameScene struct{
	GameObject	
}

func (m *GameObject) AddObj(newobj GameObject){
	m.children = append(m.children, newobj)
}