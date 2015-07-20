using UnityEngine;
using System;
using System.Collections;
using System.IO;

//AssetBundle在线更新
public class ABUpdateManager : MonoBehaviour {
    string[] abs;
    int fileCnt;

    void Start()
    {
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
            print("不用更新");
            gameObject.AddComponent<LoadTest>();
        }            
    }

    //下载文件
    IEnumerator Download(string filename)
    {
        string filePath = Application.persistentDataPath + "/" + filename;
        string url = "http://asts.aliapp.com/" + filename;

        print("load " + url);        
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
            print("更新完成");
            gameObject.AddComponent<LoadTest>();
        }            
    }

    bool isContain(string filename)
    {
        return File.Exists(Application.persistentDataPath + "/" + filename);
    }
}
