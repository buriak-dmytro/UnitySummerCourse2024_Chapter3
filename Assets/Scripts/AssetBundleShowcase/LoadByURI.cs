using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AssetBundleShowcase
{
    public class LoadByURI : MonoBehaviour
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
                    "winter");
            UnityWebRequest uwr = 
                UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);

            yield return uwr.SendWebRequest();

            AssetBundle bundle =
                DownloadHandlerAssetBundle.GetContent(uwr);
            GameObject snowman = 
                bundle.LoadAsset<GameObject>("dude");

            Instantiate(
                snowman,
                new Vector3(3.0f, 0.0f, 3.0f),
                Quaternion.identity);
        }
    }
}
