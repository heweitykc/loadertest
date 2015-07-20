using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LoadTest : MonoBehaviour {
    public Text lb;
    string filename = "aliceast";

	void Start () {
        string filePath = Application.persistentDataPath + "/" + filename;
        if (File.Exists(filePath))
        {
            gameStart();
        }
        else {
            StartCoroutine(LoadFiles(filePath));
        }        
	}

    IEnumerator LoadFiles(string filePath)
    {
        string url = "http://asts.aliapp.com/" + filename;
        print("load " + url);
        lb.text = url + "\r\t";
        WWW www = new WWW(url);
        yield return www;

        byte[] data = www.bytes;
        
        print(" . " + filePath);
        FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
        lb.text += filePath + "\r\t";
        stream.Write(data, 0, data.Length);
        stream.Flush();
        stream.Close();
        www.Dispose();
        gameStart();
    }

    void gameStart()
    {
        AssetBundle ast = AssetBundle.CreateFromFile(Application.persistentDataPath + "/" + filename);
        GameObject obj = ast.LoadAsset<GameObject>("AliceAST");
        Instantiate(obj);
    }	
}
