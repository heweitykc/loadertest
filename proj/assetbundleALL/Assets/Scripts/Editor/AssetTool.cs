using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetTool : EditorWindow {
    [MenuItem("Window/My Window")]
    static void Init()
    {        
        AssetTool window = (AssetTool)EditorWindow.GetWindow(typeof(AssetTool));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        if (GUILayout.Button("生成")) {
            BuildPipeline.BuildAssetBundles(@"E:\codejam\loadertest\server_assets\webroot",
                BuildAssetBundleOptions.UncompressedAssetBundle,BuildTarget.Android);
        }
    }
}
