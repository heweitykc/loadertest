using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class TestGame : MonoBehaviour {
    public Transform ball;
    Socket _sock;

	// Use this for initialization
	void Start () {
        IPHostEntry hostEntry = Dns.GetHostEntry("www.baidu.com");
        foreach (IPAddress ip in hostEntry.AddressList) {
            IPEndPoint iep = new IPEndPoint(ip, 80);
            _sock = new Socket(iep.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            _sock.Connect(iep);            
        }
        Application.targetFrameRate = 20;
	}
	
	// Update is called once per frame
	void Update () {
        if (_sock.Connected) {
            print("connected!!!!");
        }
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100f,60f),"发射")) {
            print("shfshe");
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(100,0,100));
        }
    }
}
