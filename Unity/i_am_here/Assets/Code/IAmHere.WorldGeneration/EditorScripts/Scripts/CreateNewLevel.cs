using UnityEngine;
using UnityEditor;


namespace IAmHere.WorldGeneration
{
    public class CreateNewLevel
    {
        [MenuItem("Assets/Create/Level")]
        public static Levels Create()
        {
            Levels asset = ScriptableObject.CreateInstance<Levels>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/LevelData/Levels.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
    }
}