using System;
using UnityEngine;

namespace IAmHere.Game
{
    public class MainCameraController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera = null;
        public void Init(int levelColumns, int levelRows)
        {
            transform.position = new Vector3((float) levelColumns / 2, -(float) levelRows / 2 + 0.5f, transform.position.z);
            int longerAxis = Math.Max(levelColumns, levelRows);
            mainCamera.orthographicSize = (float) longerAxis / 2;
        }

    }
}