using UnityEngine;
using System.Collections;

public class TFSMWorkingData : TAny {
    internal TFSMLink _stimulus;
    internal TFSMState _currentState;
    internal bool _isFirstUpdate;
    internal TFSMState _defaultState;

    public TFSMWorkingData()
    { }

    ~TFSMWorkingData()
    {
        _stimulus = null;
        _currentState = null;
        _defaultState = null;
    }
}
