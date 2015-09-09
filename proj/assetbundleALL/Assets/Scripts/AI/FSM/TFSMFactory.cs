using UnityEngine;
using System.Collections.Generic;


public class TFSMFactory : TSingleton<TFSMFactory> {
    private Dictionary<string, TFSMMachine> _registeredFSMs;
    public TFSMFactory()
    {
        _registeredFSMs = new Dictionary<string, TFSMMachine>();
    }

    ~TFSMFactory()
    {
        _registeredFSMs = null;
    }

    public TFSMMachine getFSM(string name)
    {        
        return _registeredFSMs[name];
    }

    public bool registerFSM(string name, TFSMMachine fsm)
    {
        if (_registeredFSMs.ContainsKey(name)) {
            TLogger.WARNING(name + " has existed , is it intentional or not?");
            return false;
        }
        _registeredFSMs.Add(name,fsm);
        return true;
    }
}
