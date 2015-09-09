using UnityEngine;
using System.Collections;

public class TSingleton<T> where T : class,new() {
    private static T _instance = null;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}
