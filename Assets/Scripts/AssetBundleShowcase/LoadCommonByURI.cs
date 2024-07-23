using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AssetBundleShowcase
{
    public class LoadCommonByURI : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(InstantiateObject());
        }

        private IEnumerator InstantiateObject()
        {
            string uri =
                Path.Combine(
                    "file:///",
                    Application.dataPath,
                    "AssetBundles",
                    "material");
            UnityWebRequest uwr = 
                UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);

            yield return uwr.SendWebRequest();

            AssetBundle bundle =
                DownloadHandlerAssetBundle.GetContent(uwr);
            GameObject snowman = 
                bundle.LoadAsset<GameObject>("commonmaterial");
        }
    }
}
