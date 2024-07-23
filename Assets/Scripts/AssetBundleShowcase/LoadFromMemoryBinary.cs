using System.IO;
using System.Collections;
using UnityEngine;

namespace AssetBundleShowcase
{
    public class LoadFromMemoryBinary : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(LoadFromMemoryAsync());
        }

        private IEnumerator LoadFromMemoryAsync()
        {
            string path =
                Path.Combine(
                    Application.dataPath,
                    "AssetBundles",
                    "winter");
            byte[] data =
                File.ReadAllBytes(path);
            AssetBundleCreateRequest createRequest =
                AssetBundle.LoadFromMemoryAsync(data);

            yield return createRequest;

            AssetBundle bundle = createRequest.assetBundle;
            GameObject prefab = 
                bundle.LoadAsset<GameObject>("tree");
            Instantiate(
                prefab,
                new Vector3(-3.0f, -5.0f, 0.0f),
                Quaternion.identity);
        }
    }
}
