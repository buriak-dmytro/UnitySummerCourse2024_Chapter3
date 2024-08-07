using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TestLoadAssetBundle : MonoBehaviour
{
    private AssetBundle ab;

    void Update()
    {
        StartCoroutine(AcquireAssetBundle());
        StartCoroutine(InstantiateAsset());
    }
    
    private IEnumerator AcquireAssetBundle()
    {
        UnityWebRequest uwr = 
            UnityWebRequestAssetBundle.GetAssetBundle(
                "https://cloud.unity.com/14f22f2/2");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            ab = ((DownloadHandlerAssetBundle)uwr.downloadHandler).assetBundle;
        }
        else
        {
            Debug.Log("error happened while retreiving asset bundle by network");
        }
    }

    private IEnumerator InstantiateAsset()
    {
        AssetBundleRequest abr = 
            ab.LoadAssetAsync<GameObject>("gameObject1");
        yield return abr;
        Instantiate((GameObject)abr.asset); 
    }
}
