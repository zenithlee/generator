using UnityEditor;

public class CreateAssetBundles
{
  [MenuItem("Assets/Build AssetBundles")]
  static void BuildAllAssetBundles()
  {
    BuildPipeline.BuildAssetBundles(@"C:\development\Unity\NewsNet_Generator\CATSE\bundles", BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows);
  }
}