﻿using UnityEngine;
using System.Collections;

public class front : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        var inter = new interpreter("1+4");
        Debug.Log("计算结果" + inter.expr());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
