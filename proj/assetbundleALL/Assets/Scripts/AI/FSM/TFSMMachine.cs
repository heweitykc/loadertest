using UnityEngine;
using System.Collections.Generic;

public class TFSMMachine {

    static public TFSMMachine CREATE()
    {
        return new TFSMMachine();
    }
    private Dictionary<string, TFSMState> _states;
    private Dictionary<string, TFSMLink> _stateLinks;

    public TFSMMachine()
    {
        _states = new Dictionary<string, TFSMState>();
        _stateLinks = new Dictionary<string, TFSMLink>();
    }

    ~TFSMMachine()
    {
        _states = null;
        _stateLinks = null;
    }

    public void process(TFSMWorkingData wData)
    {
        if (wData._isFirstUpdate) {
            wData._currentState = wData._defaultState;
            if (wData._currentState != null) {
                wData._currentState.enter(wData);
                wData._isFirstUpdate = false;
            }
        }
        TFSMState newState = wData._currentState;
        if (wData._stimulus != null) {
            newState = wData._stimulus.getGoalState();
            wData._stimulus = null;
        }
        if (newState != wData._currentState) {
            if (wData._currentState != null) {
                wData._currentState.exit(wData);
            }
            wData._currentState = newState;
            if (wData._currentState != null) { 
                wData._currentState.enter(wData);
            }
        }
        if (wData._currentState != null) {
            wData._currentState.execute(wData);
        }
    }

    public TFSMMachine setDefaultState(TFSMWorkingData wData, string id)
    {        
        if (_states.ContainsKey(id))
        {
            wData._defaultState = _states[id];
        }
        else {
            TLogger.WARNING("no state found");
        }
        return this;
    }

    public TFSMMachine addState(string id, TFSMState state)
    {
        if (_stateLinks.ContainsKey(id)) {
            TLogger.WARNING("name conflict for this state: "+ id);
            return this;
        }
        state.name = id;
        state.stateMachine = this;
        _states.Add(id, state);
        return this;
    }

    public TFSMMachine addLink(string idFrom, string idTo)
    {
        string id = TFSMLink.LINK_NAME_GENERATOR(idFrom, idTo);
        if (_stateLinks.ContainsKey(id)) {
            TLogger.WARNING("name conflict for this state: " + id);
            return this;
        }
        TFSMState fromState, toState;
        bool hasFromState = _states.TryGetValue(idFrom, out fromState);
        bool hasToState = _states.TryGetValue(idTo, out toState);
        if (hasFromState || hasToState) {
            TLogger.WARNING("state not found: " + idFrom + " and " + idTo);
            return this;
        }
        _stateLinks.Add(id, new TFSMLink(fromState, toState));
        return this;
    }

    public bool signal(TFSMSignal s, TFSMWorkingData wData)
    {
        if (wData._currentState != null)
        {
            string id = TFSMLink.LINK_NAME_GENERATOR(wData._currentState.name, s.targetState);
            TFSMLink link;
            if (!_stateLinks.TryGetValue(id, out link)) {
                TLogger.WARNING("no link found.");
                return false;
            }
            if (link.getStartState() != wData._currentState)
            {
                TLogger.WARNING("connot use this link for current state:" + wData._currentState.name + ":" + id);
                return false;
            }
            wData._stimulus = link;
        }
        else {
            if (wData._isFirstUpdate == false)
            {
                setDefaultState(wData, s.targetState);
            }
            else {
                TLogger.WARNING("state fatal error:" + s.targetState);
                return false;
            }
        }
        return true;
    }

}
