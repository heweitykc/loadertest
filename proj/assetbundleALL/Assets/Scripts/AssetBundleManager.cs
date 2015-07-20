using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//管理AssetBundle
static public class AssetBundleManager {
   
   static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;
   static AssetBundleManager (){
       dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
   }
   
   private class AssetBundleRef {
       public AssetBundle assetBundle = null;
       public int version;
       public string url;
       public AssetBundleRef(string strUrlIn, int intVersionIn) {
           url = strUrlIn;
           version = intVersionIn;
       }
   };

   public static AssetBundle getAssetBundle (string name, int version){
       string url = Application.persistentDataPath + "/" + name;
       if (!dictAssetBundleRefs.ContainsKey(url)) {
           AssetBundleRef abref = new AssetBundleRef(url, version);
           abref.assetBundle = AssetBundle.CreateFromFile(url);
           dictAssetBundleRefs[url] = abref;
       }
       return dictAssetBundleRefs[url].assetBundle;
   }
    /*
   public static void Unload (string url, int version, bool allObjects){
       string keyName = Application.persistentDataPath + "/" + url;
       AssetBundleRef abRef;
       if (dictAssetBundleRefs.TryGetValue(keyName, out abRef)){
           abRef.assetBundle.Unload (allObjects);
           abRef.assetBundle = null;
           dictAssetBundleRefs.Remove(keyName);
       }
   }*/
}