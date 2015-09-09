using UnityEngine;
using System.Collections;

public class TFSMLink{
    static public string LINK_NAME_GENERATOR(string from, string to)
    {
        return from + "->" + to;
    }

    private TFSMState _from;
    private TFSMState _to;

    public TFSMLink(TFSMState from, TFSMState to)
    {
        _from = from;
        _to = to;
    }

    ~TFSMLink()
    {
        _from = null;
        _to = null;
    }

    public TFSMState getStartState()
    {
        return _from;
    }

    public TFSMState getGoalState()
    {
        return _to;
    }
}
