using UnityEngine;
using System.Net.Sockets;
using System.Collections;

//tcp client 组件
public class GameClient : MonoBehaviour {
    TcpClient _client;
	void Start () {
        _client = new TcpClient();
        _client.Connect("127.0.0.1",8080);        
    }
		
	void Update () {
        if (_client.Connected && _client.Available > 0) {
            byte[] buffer = new byte[1024];
            _client.GetStream().Read(buffer,0,1024);
            string str = System.Text.Encoding.UTF8.GetString(buffer);
            Debug.Log("recv: " + str);
        }
	}

    void OnDestrory()
    {
        if (_client.Connected)
            _client.Close();
    }
}
