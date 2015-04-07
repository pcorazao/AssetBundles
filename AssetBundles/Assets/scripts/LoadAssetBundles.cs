using UnityEngine;
using System.Collections;

public class LoadAssetBundles : MonoBehaviour {

	public string assetBundleFolder = "file:///Users/pcorazao/Documents/Unity/AssetBundlesRound2/AssetBundles/";
	public string cubeAssetBundle = "cube.unity3d";
	
	IEnumerator Start()
	{
		string manifestABPath = assetBundleFolder + "AssetBundles";
		
		var wwwManifest = new WWW (manifestABPath);
		yield return wwwManifest;
		
		AssetBundle manifestBundle = wwwManifest.assetBundle;
		
		AssetBundleManifest manifest = manifestBundle.LoadAsset ("AssetBundleManifest") as AssetBundleManifest;
		manifestBundle.Unload (false);
		
		//get dependencies
		string[] dependentAssetBundles = manifest.GetAllDependencies (cubeAssetBundle);
		for (int i = 0; i < dependentAssetBundles.Length; i++) {
			Debug.Log(dependentAssetBundles[i]);
		}
		
		//load dependent assetbundles
		AssetBundle[] assetBundles = new AssetBundle[dependentAssetBundles.Length];
		for (int i = 0; i < dependentAssetBundles.Length; i++) {
			string assetBundlePath = assetBundleFolder + dependentAssetBundles[i];
			
			//Get the hash
			Hash128 hash = manifest.GetAssetBundleHash(dependentAssetBundles[i]);
			var www = WWW.LoadFromCacheOrDownload(assetBundlePath, hash);
			yield return www;
			assetBundles[i] = www.assetBundle;
		}
		
		//load game1 assetBundle
		var wwwCube = WWW.LoadFromCacheOrDownload (assetBundleFolder + cubeAssetBundle, manifest.GetAssetBundleHash (cubeAssetBundle));
		yield return wwwCube;
		AssetBundle cubeBundle = wwwCube.assetBundle;
		
		// Load game1
		GameObject cubePrefab = (GameObject)cubeBundle.LoadAsset("Cube");
		GameObject.Instantiate(cubePrefab);
		
		for (int i = 0; i < dependentAssetBundles.Length; i++) {
			//assetBundles[i].Unload(false);
		}
		
		//Unload cube assetBundle
		cubeBundle.Unload(false);
	}
}
