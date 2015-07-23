using UnityEngine;
using System.Collections;

public class TestGame : MonoBehaviour {
    public Transform ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100f,60f),"发射")) {
            print("shfshe");
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(100,0,100));
        }
    }
}
