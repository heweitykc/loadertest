using UnityEngine;
using System.Collections;

public class TAny {
    public T As<T>() where T : TAny
    {
        return (T)this;
    }
}
