using UnityEngine;

namespace IAmHere.Game
{
    public class MainCameraController : MonoBehaviour
    {
        public void Init(int levelColumns, int levelRows)
        {
            transform.position = new Vector3((float) levelColumns / 2, -(float) levelRows / 2, transform.position.z);
        }

    }
}