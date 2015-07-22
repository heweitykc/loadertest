using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class MainGame : MonoBehaviour {
    string fname = "aliceast";
	void Start () {          
        AssetBundle ast =
            AssetBundleManager.getAssetBundle(fname, 0);
        GameObject obj = ast.LoadAsset<GameObject>("AliceAST");
        Instantiate(obj);
	}
}
