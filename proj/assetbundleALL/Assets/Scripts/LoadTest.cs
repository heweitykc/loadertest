using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LoadTest : MonoBehaviour {    

	void Start () {          
        AssetBundle ast = 
            AssetBundleManager.getAssetBundle("aliceast",0);
        GameObject obj = ast.LoadAsset<GameObject>("AliceAST");
        Instantiate(obj);
	}
}
