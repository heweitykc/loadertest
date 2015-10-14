using UnityEngine;
using System.Collections;

public class cameratest : MonoBehaviour {
    public Transform target;
	
	void Start () {
	
	}
		
	void Update () {
        Vector3 pos = Camera.main.WorldToScreenPoint(target.position);
        Debug.Log(pos);
	}
}
