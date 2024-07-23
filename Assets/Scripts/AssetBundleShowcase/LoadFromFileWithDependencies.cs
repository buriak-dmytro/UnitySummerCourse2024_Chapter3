using System.IO;
using UnityEngine;

public class LoadFromFileWithDependencies : MonoBehaviour
{
    void Start()
    {
        string manifestFilePath =
                Path.Combine(
                    "file:///",
                    Application.dataPath,
                    "AssetBundles",
                    "winter");
        string assetBundlePath = manifestFilePath;

        AssetBundle assetBundle = 
            AssetBundle.LoadFromFile(assetBundlePath);
        if (assetBundle != null) { Debug.Log("assetBundle is null"); }
        AssetBundleManifest manifest = 
            assetBundle.LoadAsset<AssetBundleManifest>("winter");
        if (manifest != null) { Debug.Log("manifest is null"); }
        string[] dependencies = 
            manifest.GetAllDependencies("tree");
        if (dependencies != null) { Debug.Log("dependencies is null"); }
        foreach (string dependency in dependencies)
        {
            AssetBundle.LoadFromFile(
                Path.Combine(
                    assetBundlePath,
                    dependency));
            Debug.Log(dependency);
        }
    }
}
