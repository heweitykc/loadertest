using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LoadTest : MonoBehaviour {
    public Text lb;
    string filename = "aliceast";

	void Start () {        
        AssetBundle ast = 
            AssetBundleManager.getAssetBundle(Application.persistentDataPath + "/aliceast",0);
        GameObject obj = ast.LoadAsset<GameObject>("AliceAST");
        Instantiate(obj);
	}
}
