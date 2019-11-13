﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Level Generation")]
    [SerializeField] private Levels Levels = null;
    [SerializeField] private MarchingSquares marchingSquares = null;
    private int levelIndex = 0;
    
    [Header("Player")]
    [SerializeField] private PlayerController playerControllerPrefab = null;
    [SerializeField] private GoalController goalControllerPrefab = null;
    [SerializeField] private SoundWaveController soundWaveControllerPrefab = null;
    
    void Start()
    {
        marchingSquares.CreateGrid(marchingSquares.ParseGrid(marchingSquares.ConvertLevelToGrid(Levels.levels[levelIndex])));
        SpawnEntities();
    }

    private void SpawnEntities()
    {
        // TODO(Rok Kos): Test how many starts and how many ends are there
        Level level = Levels.levels[levelIndex];
        Square[] grid = level.board;
        for (int y = 0; y < level.rows; ++y) {
            for (int x = 0; x < level.columns; ++x)
            {
                int index = y * level.rows + x;

                switch (level.board[index])
                {
                    case Square.kStart:
                    {
                        PlayerController playerController = Instantiate(playerControllerPrefab, new Vector3(y - 0.5f, -x + 0.5f, 0), Quaternion.identity, null);
                        playerController.Init(soundWaveControllerPrefab);
                        break;
                    }
                    case Square.kEnd:
                    {
                        GoalController goalController = Instantiate(goalControllerPrefab, new Vector3(y - 0.5f, -x + 0.5f, 0), Quaternion.identity, null);
                        goalController.Init(soundWaveControllerPrefab);
                        break;
                    }
                    
                    case Square.kEnemy:
                    case Square.kEmtpy:
                    default:
                        continue;
                }
                
                if (level.board[index] == Square.kStart)
                {
                    
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
