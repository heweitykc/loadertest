using UnityEngine;
using System.Collections;

public class TBTActionContext
{ 
}

public class TBTAction {
    static private int sUNIQUEKEY = 0;
    static private int genUniqueKey()
    {
        if (sUNIQUEKEY >= int.MaxValue)
        {
            sUNIQUEKEY = 0;
        }
        else {
            sUNIQUEKEY++;
        }
        return sUNIQUEKEY;
    }

    protected int _uniqueKey;    
}
