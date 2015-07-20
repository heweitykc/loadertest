using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;

//AssetBundle在线更新
public class ABUpdateManager : MonoBehaviour {
    public Action<String> msgShow;
    public Text lb;

    string[] abs;
    int fileCnt;    

    void Start()
    {
        msgShow = displayMsg;
        print("存储地址：" + Application.persistentDataPath);
        abs = new string[] { "aliceast" };
        fileCnt = abs.Length;

        foreach (string file in abs)
        {
            if (isContain(file))
            {
                fileCnt--;
                continue;
            }
            StartCoroutine(Download(file));
        }

        if (fileCnt == 0) {
            msgShow("不用更新");
            Application.LoadLevel(1);
        }            
    }

    //下载文件
    IEnumerator Download(string filename)
    {
        string filePath = Application.persistentDataPath + "/" + filename;
        string url = "http://asts.aliapp.com/" + filename;

        msgShow("下载：" + url);        
        WWW www = new WWW(url);
        yield return www;

        byte[] data = www.bytes;

        print(" . " + filePath);
        FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);        
        stream.Write(data, 0, data.Length);
        stream.Flush();
        stream.Close();
        www.Dispose();
        fileCnt--;

        if (fileCnt == 0) {
            msgShow("更新完成");
            Application.LoadLevel(1);
        }            
    }

    bool isContain(string filename)
    {
        return File.Exists(Application.persistentDataPath + "/" + filename);
    }

    void displayMsg(string msg)
    {
        lb.text = msg;
    }
}
