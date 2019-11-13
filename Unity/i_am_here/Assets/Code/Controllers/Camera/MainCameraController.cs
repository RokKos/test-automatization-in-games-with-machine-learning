using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private WorldManager WorldManager;
    // Start is called before the first frame update
    void Start()
    {
        Level level = WorldManager.GetCurrLevel();
        transform.position = new Vector3((float)level.columns / 2, -(float)level.rows / 2 , transform.position.z);
    }

}
