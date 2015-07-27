using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Threading;
using System.Diagnostics;

public class AssetTool : EditorWindow {
    [MenuItem("Tools/InstallAPK")]
    static void Install()
    {
        string[] levels = { "Assets/zufang.unity"};
        BuildPipeline.BuildPlayer(levels, @"E:\alicetest.apk", BuildTarget.Android, BuildOptions.None);        
        Thread newThread = new Thread(new ThreadStart(NewThread));
        newThread.Start();  
    }

    static void NewThread()
    {
        RunCmd(@"adb install -r e:\alicetest.apk");
    }

    [MenuItem("Tools/My Window")]
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

    static string RunCmd(string command)
    {
        //例Process
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";           //确定程序名
        p.StartInfo.Arguments = "/c " + command;    //确定程式命令行
        p.StartInfo.UseShellExecute = false;        //Shell的使用
        p.StartInfo.RedirectStandardInput = true;   //重定向输入
        p.StartInfo.RedirectStandardOutput = true; //重定向输出
        p.StartInfo.RedirectStandardError = true;   //重定向输出错误
        p.StartInfo.CreateNoWindow = true;          //设置置不显示示窗口
        p.Start();
        return p.StandardOutput.ReadToEnd();        //输出出流取得命令行结果果
    }
}
