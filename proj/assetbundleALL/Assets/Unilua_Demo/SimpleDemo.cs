using UnityEngine;
using System.Collections;
using UniLua;

public class SimpleDemo : MonoBehaviour {
    private ILuaState Lua;
    public string LuaScriptFile = "framework/main.lua";
    public string luastr = "print('do it...')";
    
    void Start () {
        Lua = LuaAPI.NewState();
        Lua.L_OpenLibs();
        var status = Lua.L_DoString(luastr);
        Debug.Log(status);
    }
		
	void Update () {
	
	}
}
