using UnityEngine;
using System.Collections;

public class TFSMState {
    private string _name;
    private TFSMMachine _stateMachine;

    public TFSMState()
    {
        _name = null;
        _stateMachine = null;
    }

    ~TFSMState()
    {
        _stateMachine = null;
    }

    public string name
    {
        get
        {
            return _name;
        }
        internal set
        {
            _name = value;
        }
    }

    internal TFSMMachine stateMachine
    {
        set
        {
            _stateMachine = value;
        }
    }

    internal void enter(TFSMWorkingData wData)
    {
        onEnter(wData);
    }

    internal void execute(TFSMWorkingData wData)
    {
        onExecute(wData);
    }

    internal void exit(TFSMWorkingData wData)
    {
        onExit(wData);
    }

    protected virtual void onEnter(TFSMWorkingData wData) { }
    protected virtual void onExecute(TFSMWorkingData wData) { }
    protected virtual void onExit(TFSMWorkingData wData) { }
}
