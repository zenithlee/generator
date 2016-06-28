using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Browser : MonoBehaviour {

  string URL;
  string AssetName = "";

  public InputField urlField;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void GO()
  {
    URL = urlField.text;
    if ( URL == "~start")
    {    
    }
    StartCoroutine(LoadCache());
  }

  IEnumerator LoadCache()
  {
    while (!Caching.ready)
      yield return null;

    var www = WWW.LoadFromCacheOrDownload("http://test.com/myassetBundle.unity3d", 1);
    yield return www;
    if (!string.IsNullOrEmpty(www.error))
    {
      Debug.Log(www.error);
      yield return null;
    }
    var myLoadedAssetBundle = www.assetBundle;

    var asset = myLoadedAssetBundle.mainAsset;
  }

  IEnumerator LoadPage()
  {
    using (WWW www = new WWW(URL))
    {
      yield return www;
      if (www.error != null)
        throw new Exception("WWW download had an error:" + www.error);
      AssetBundle bundle = www.assetBundle;
      if (AssetName == "")
        Instantiate(bundle.mainAsset);
      else
        Instantiate(bundle.LoadAsset(AssetName));
      // Unload the AssetBundles compressed contents to conserve memory
      bundle.Unload(false);
  }
}



}
