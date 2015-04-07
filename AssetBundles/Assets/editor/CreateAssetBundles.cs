using UnityEditor;

public class CreateAssetBundles{

	[MenuItem ("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("AssetBundles");
	}

	[MenuItem("Assets/Create From Script")]
	public static void CreateNewAssetBundleFromScript()
	{
		string outputPath = "AssetBundlesFromScript";
		
		AssetImporter cubeImporter = AssetImporter.GetAtPath("Assets/prefab/Cube.prefab");
		cubeImporter.assetBundleName = "cube.unity3d";


		AssetImporter materialImporter = AssetImporter.GetAtPath("Assets/material/MyMaterial.mat");
		materialImporter.assetBundleName = "material.unity3d";

		AssetImporter game1Importer = AssetImporter.GetAtPath("Assets/images/UnityLogo.png");
		game1Importer.assetBundleName = "texture.unity3d";

		BuildPipeline.BuildAssetBundles(outputPath);
	}

	[MenuItem("Assets/Create From AssetBundleBuild")]
	public static void CreateNewAssetBundleFromAssetBundleBuild()
	{
		string outputPath = "AssetBundlesFromBuildInfo";
		
		AssetBundleBuild[] buildInfo = new []
		{
			new AssetBundleBuild{ assetBundleName = "cube.unity3d", assetNames = new [] {"Assets/prefab/Cube.prefab"}},
			new AssetBundleBuild{ assetBundleName = "material.unity3d", assetNames = new [] {"Assets/material/MyMaterial.mat"}},
			new AssetBundleBuild{ assetBundleName = "texture.unity3d", assetNames = new [] {"Assets/images/UnityLogo.png"}}
		};
		
		BuildPipeline.BuildAssetBundles(outputPath,buildInfo);
	}
}
