using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Levels Levels = null;
    [SerializeField] private MarchingSquares marchingSquares = null;
    // Start is called before the first frame update
    void Start()
    {
        marchingSquares.CreateGrid(marchingSquares.ParseGrid(marchingSquares.ConvertLevelToGrid(Levels.levels[0])));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
