using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[ExecuteInEditMode]
public class CreateAssetBundles : MonoBehaviour
{
    // [MenuItem ("Assets/Build AssetBundles")]
    public void Start ()
    {
        string[] AsBundName = new string[1];
        AsBundName[0] = "shaders";
        BuildAssetBundlesByName(AsBundName, "Assets/AssetBundles");
        // BuildPipeline.BuildAssetBundles ("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }


    void BuildAssetBundlesByName(string[] assetBundleNames, string outputPath) 
   {
       // Argument validation
       if (assetBundleNames == null || assetBundleNames.Length == 0)
       {
           return;
       }

       // Remove duplicates from the input set of asset bundle names to build.
       //assetBundleNames = assetBundleNames.Distinct().ToArray();

       List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

       foreach (string assetBundle in assetBundleNames)
       {
           var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundle);

           AssetBundleBuild build = new AssetBundleBuild();
           build.assetBundleName = assetBundle;
           build.assetNames = assetPaths;

           builds.Add(build);
           // Debug.Log("assetBundle to build:" + build.assetBundleName);
       }
       
       BuildPipeline.BuildAssetBundles(outputPath, builds.ToArray(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
   }

}