using UnityEngine;
using System.Collections;

public class TStaticHelperBase<T> : 
    TSingleton<T> where T : class, new() 
{
    public void init()
    {
        onInit();
        _hasInited = true;
    }

    public void uninit()
    {
        onUnInit();
        _hasInited = false;
    }

    protected virtual void onInit()
    {
 
    }

    protected virtual void onUnInit()
    {
 
    }

    private bool _hasInited = false;
    public bool hasInited
    {
        get {
            return _hasInited;
        }
    }
}
