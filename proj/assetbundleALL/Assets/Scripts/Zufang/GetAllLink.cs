using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Text.RegularExpressions;

struct ItemData
{
    public string dt;
    public int price;
    public string name;
    public string tel;
}

// "http://sh.zu.anjuke.com/rent/F64585524">

class GetAllLink
{       
    static Regex reg0 = new Regex(@"http://sh.zu.anjuke.com/rent/\w+");
    static Regex reg1 = new Regex(@"发布时间：\w+");
    static Regex reg2 = new Regex(@"\d+</span>元/月");
    static Regex reg3 = new Regex("<strong class=\"name\">\\w+</strong>");
    static Regex reg4 = new Regex("<strong class=\"phone\">.+</strong>");

    static public List<string> getAll(string all)
    {
        List<string> list0 = new List<string>();
        MatchCollection mc = reg0.Matches(all);
        for (int i = 0; i < mc.Count; i++)
        {
            string url = mc[i].Value;
            //Debug.Log(url + " POS:" + mc[i].Index);
            if (list0.IndexOf(url) < 0)
                list0.Add(url);
        }
        return list0;
    }

    static public ItemData getDate(string all)
    {
        ItemData data = new ItemData();
        Match m = reg1.Match(all);
        data.dt = m.Value.Replace("发布时间：","");        
        m = reg2.Match(all);
        data.price = int.Parse(m.Value.Replace("</span>元/月",""));
        m = reg3.Match(all);
        data.name = (m.Value.Replace("<strong class=\"name\">", ""));
        data.name = data.name.Replace("</strong>", "");
        m = reg4.Match(all);
        data.tel = (m.Value.Replace("<strong class=\"phone\">", ""));
        data.tel = data.tel.Replace("</strong>", "");
        data.tel = data.tel.Replace("-", "");
        return data;
    }
}
