using UnityEngine;
using System.Collections;

public class TAIToolkit {
	
    static public void init () {
        TLogger.instance.init();
	}

    static public void uninit()
    {
        TLogger.instance.uninit();
	}
}
