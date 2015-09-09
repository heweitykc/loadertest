using UnityEngine;
using System.Collections;

public class TFSMSignal {
    private string _targetState;
    public TFSMSignal(string to)
    {
        _targetState = to;
    }
    public string targetState
    {
        get {
            return _targetState;
        }
    }
}
