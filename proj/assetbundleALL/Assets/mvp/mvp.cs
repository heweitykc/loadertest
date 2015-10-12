using UnityEngine;
using System.Collections;

public class mvp : MonoBehaviour {

	
	void Start () {
        Vector3 v0 = new Vector3(1, 0, 0);
        Vector3 v1 = new Vector3(1, 0, 1);

        Vector3 v = Vector3.Cross(v1,v0);
        Debug.Log(v.y);
    }
		
	void Update () {
        Debug.Log("delta: " + Time.deltaTime);
	}
}
