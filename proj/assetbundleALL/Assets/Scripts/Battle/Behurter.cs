using UnityEngine;
using System.Collections;

//受击体
public class Behurter : MonoBehaviour {
    public Transform[] bodies;
	
	void Start () {
        bodies = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            bodies[i] = transform.GetChild(i);
        }        
	}
		
	void Update () {
	
	}
}
