using IAmHere.Utilities;
using IAmHere.WorldGeneration;

namespace IAmHere.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public WorldManager WorldManager = null;

        //private void Awake()
        //{
        //    // TODO(Rok Kos): Rethink this decision
        //    WorldManager = FindObjectOfType<WorldManager>();
        //}
    }
}