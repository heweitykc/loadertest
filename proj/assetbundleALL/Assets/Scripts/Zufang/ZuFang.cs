using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ZuFang : MonoBehaviour {
    string cun1 = "http://shanghai.anjuke.com/community/props/rent/10261/fx2-px3/";
    string cun2 = "http://shanghai.anjuke.com/community/props/rent/6791/fx2-px3/";
    string cun3 = "http://shanghai.anjuke.com/community/props/rent/10284/fx2-px3/";
    int maxPrice = 3500;
    int jiage = 1;

    public Transform panel0;

	void Start () {
        
	}
		
	void Update () {
	
	}

    void startSearch(string desc,string url)
    {
        clear();
        StopAllCoroutines();
        showmsg(desc);
        StartCoroutine(DoRequest(url));
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 20f, 100f, 80f), "水清一村"))
        {
            startSearch("开始查找水清一村", cun1);
        }

        if (GUI.Button(new Rect(0, 100f, 100f, 80f), "水清二村"))
        {
            startSearch("开始查找水清二村", cun2);
        }

        if (GUI.Button(new Rect(0, 180f, 100f, 80f), "水清三村"))
        {
            startSearch("开始查找水清三村", cun3);
        }
    }

    int startIndex = 0;

    IEnumerator DoRequest(string yurl)
    {        
        WWW www = new WWW(yurl);
        yield return www;        
        Debug.Log("DoRequest = " + www.bytes.Length);
        List<string> list0 = GetAllLink.getAll(www.text);
        yield return new WaitForSeconds(jiage);
        foreach (string url in list0)
        {
            StartCoroutine(GetInfo(url));
            yield return new WaitForSeconds(jiage);
        }
        addmsg(",读取完毕");
        yield return new WaitForSeconds(15f);
        if(yurl == cun1)
            startSearch("开始查找水清一村", cun1);
        else if(yurl == cun2)
            startSearch("开始查找水清二村", cun2);
        else
            startSearch("开始查找水清三村", cun3);

    }

    IEnumerator GetInfo(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        Debug.Log("GetInfo = " + www.bytes.Length);
        ItemData data = GetAllLink.getDate(www.text);
        Debug.Log(url + " 发布时间：" + data.dt + "，价格：" + data.price);
        if (data.price <= maxPrice)
        {
            panel0.GetChild(startIndex).GetComponent<Text>().text = (data.name + " " + data.price + " " + data.tel + "\n" + data.dt);
            startIndex++;
        }
    }

    void clear()
    {
        foreach (Transform t in panel0) {
            t.GetComponent<Text>().text = "";
        }
        startIndex = 0;
    }

    void showmsg(string msg)
    {
        panel0.GetChild(panel0.childCount - 1).GetComponent<Text>().text = msg;
    }

    void addmsg(string msg)
    {
        panel0.GetChild(panel0.childCount - 1).GetComponent<Text>().text += msg;
    }
}
