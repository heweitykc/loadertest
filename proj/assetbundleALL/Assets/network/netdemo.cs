using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class netdemo : MonoBehaviour {
    TcpClient client;
	// Use this for initialization
	void Start () {
        client = new TcpClient();        
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(client.Available);
	}

    void OnGUI()
    {
        if (!client.Connected)
        {
            if (GUI.Button(new Rect(200, 200, 200, 50), "连接"))
            {
                client.Connect("127.0.0.1", 9090);
            }
        }
        else {
            if (GUI.Button(new Rect(200, 200, 200, 50), "发送"))
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes("hello,world!");
                client.GetStream().Write(data,0,data.Length);
            }
            GUI.Label(new Rect(200, 300, 200, 50), "len=" + client.Available.ToString());
        }

        
    }

    void OnDestroy()
    {
        client.Close();
    }
}
