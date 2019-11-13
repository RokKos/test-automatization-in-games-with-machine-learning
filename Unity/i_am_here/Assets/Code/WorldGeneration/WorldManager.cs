using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Level Generation")]
    [SerializeField] private Levels Levels = null;
    [SerializeField] private MarchingSquares marchingSquares = null;
    private int levelIndex = 0;
    
    [Header("Player")]
    [SerializeField] private PlayerController playerController = null;
    // Start is called before the first frame update
    void Start()
    {
        marchingSquares.CreateGrid(marchingSquares.ParseGrid(marchingSquares.ConvertLevelToGrid(Levels.levels[levelIndex])));
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        // TODO(Rok Kos): Test how many starts and how many ends are there
        Level level = Levels.levels[levelIndex];
        Square[] grid = level.board;
        for (int y = 0; y < level.rows; ++y) {
            for (int x = 0; x < level.columns; ++x)
            {
                int index = y * level.rows + x;
                if (level.board[index] == Square.kStart)
                {
                    Instantiate(playerController, new Vector3(y - 0.5f, -x + 0.5f, 0), Quaternion.identity, null);
                    return;
                }

            }
        }
    }

    public Level GetCurrLevel()
    {
        return Levels.levels[levelIndex];
    }
}
