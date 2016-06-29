using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Browser : MonoBehaviour {

  string URL = "http://localhost:63342/CATSE/bundles/_main";
  string AssetName = "main";

  public InputField urlField;
  public GameObject ContentHolder;
  public Canvas mainCanvas;

	// Use this for initialization
	void Start () {
    urlField.text = URL;
  }
	
	// Update is called once per frame
	void Update () {
	
	}

  public void CleanCache()
  {
    Caching.CleanCache();
  }

  public void GO()
  {
    URL = urlField.text;    
    StartCoroutine(LoadCache());
  }

  IEnumerator LoadCache()
  {
    while (!Caching.ready)
      yield return null;

    var www = WWW.LoadFromCacheOrDownload(URL, 1);
    yield return www;
    if (!string.IsNullOrEmpty(www.error))
    {
      Debug.Log(www.error);
      yield return null;
    }
    AssetBundle bundle = www.assetBundle;
    if (AssetName == "")
      Instantiate(bundle.mainAsset);
    else
    {
      GameObject[] g = bundle.LoadAllAssets<GameObject>();
      foreach( GameObject go in g)
      {
        GameObject gi = Instantiate(go);
        gi.transform.parent = ContentHolder.transform;
        yield return new WaitForEndOfFrame();
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
      }      
    }
    // Unload the AssetBundles compressed contents to conserve memory
    bundle.Unload(false);
  }  
}
