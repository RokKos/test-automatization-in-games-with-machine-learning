using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateNewLevel {
    [MenuItem("Assets/Create/Level")]
    public static Levels  Create()
    {
        Levels asset = ScriptableObject.CreateInstance<Levels>();

        AssetDatabase.CreateAsset(asset, "Assets/Code/WorldGeneration/Data/Levels.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}