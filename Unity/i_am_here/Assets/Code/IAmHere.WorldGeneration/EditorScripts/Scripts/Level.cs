using UnityEngine;

namespace IAmHere.WorldGeneration
{
    public enum Square
    {
        kEmtpy,
        kWall,
        kEnemy,
        kStart,
        kEnd
    };

    [CreateAssetMenu(fileName = "Data", menuName = "WorldGeneration/Level", order = 1)]
    public class Level : ScriptableObject
    {
        public int rows = 9;
        public int columns = 9;
        public Square[] board = null;
    }
}