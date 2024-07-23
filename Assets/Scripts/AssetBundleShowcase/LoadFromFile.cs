using System.IO;
using UnityEngine;

namespace AssetBundleShowcase
{
    public class LoadFromFile : MonoBehaviour
    {
        void Start()
        {
            AssetBundle myLoadedAssetBundle =
                AssetBundle.LoadFromFile(
                    Path.Combine(
                        "file:///",
                        Application.dataPath,
                        "AssetBundles",
                        "winter"));

            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }

            GameObject prefab = 
                myLoadedAssetBundle.LoadAsset<GameObject>("snowman");
            Instantiate(prefab);
        }
    }
}
