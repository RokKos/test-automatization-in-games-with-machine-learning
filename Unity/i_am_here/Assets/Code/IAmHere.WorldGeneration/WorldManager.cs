using System;

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using IAmHere.Game;
using IAmHere.UI;


namespace IAmHere.WorldGeneration
{

    public class WorldManager : MonoBehaviour
    {
        [Header("Level Generation")] 
        [SerializeField] private Levels Levels = null;
        [SerializeField] private List<ColliderController> CaseColliders = null;
        [SerializeField] private GameObject WorldParent = null;


        readonly MarchingSquares marchingSquares = new MarchingSquares();
        private int levelIndex = 0;
        private List<ColliderController> levelColliders = null;

        [Header("Player")] 
        [SerializeField] private PlayerController playerControllerPrefab = null;
        [SerializeField] private GoalController goalControllerPrefab = null;
        [SerializeField] private SoundWaveController soundWaveControllerPrefab = null;
        [SerializeField] private GameObject soundWaveParent = null;

        private PlayerController _playerController = null;
        private GoalController _goalController = null;
        private List<SoundWaveController> _soundWaveControllers = null;
        
        [Header("Camera")]
        [SerializeField] private MainCameraController mainCameraController = null;
        
        [Header("UI")]
        [SerializeField] private MainUIController mainUiController = null;

        void Start()
        {
            _soundWaveControllers = new List<SoundWaveController>();
            levelColliders = new List<ColliderController>();
            Level level = Levels.levels[levelIndex];
            CreateGrid(marchingSquares.ParseGrid(marchingSquares.ConvertLevelToGrid(level)));
            SpawnEntities();
            mainCameraController.Init(level.columns, level.rows);
            mainUiController.onTransitionOver += LoadNewLevel;
            
        }

        public void CreateGrid(byte[,] res)
        {
            GameObject temp = new GameObject();
            for (int y = 0; y < res.GetLength(0); y++)
            {
                GameObject row = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity, WorldParent.transform);
                row.name = "row " + y;
                for (int x = 0; x < res.GetLength(1); x++)
                {
                    // TODO(Rok Kos): Unsibsribe from delegate
                    ColliderController collider = Instantiate(CaseColliders[res[y, x]], new Vector3(x, -y, 0), Quaternion.identity, row.transform);
                    collider.onBurst += Burst;
                    levelColliders.Add(collider);
                    
                }
            }

            Destroy(temp, 0);
        }
        
        private void SpawnEntities()
        {
            // TODO(Rok Kos): Test how many starts and how many ends are there
            Level level = Levels.levels[levelIndex];
            Square[] grid = level.board;
            for (int y = 0; y < level.rows; ++y)
            {
                for (int x = 0; x < level.columns; ++x)
                {
                    int index = y * level.rows + x;

                    switch (level.board[index])
                    {
                        case Square.kStart:
                        {
                            // TODO(Rok Kos): Unsibsribe from delegate
                            _playerController = Instantiate(playerControllerPrefab,
                                new Vector3(y - 0.5f, -x + 0.5f, 0), Quaternion.identity, WorldParent.transform);
                            _playerController.onBurst += Burst;
                            _playerController.onPlayerDead += GameOver;
                            break;
                        }
                        case Square.kEnd:
                        {
                            // TODO(Rok Kos): Unsibsribe from delegate
                            _goalController = Instantiate(goalControllerPrefab,
                                new Vector3(y - 0.5f, -x + 0.5f, 0), Quaternion.identity, WorldParent.transform);
                            _goalController.onBurst += Burst;  
                            break;
                        }

                        case Square.kEnemy:
                        case Square.kEmtpy:
                        default:
                            continue;
                    }

                }
            }
        }

        public Level GetCurrLevel()
        {
            return Levels.levels[levelIndex];
        }
        
        private void Burst(WorldEntityController entity, Vector3 position, Color color, int burstSeparationAngle, int bursOffsetAngle, float burstOffsetVector,
            float forceStrenght, float maxTimeAlive)
        {
            for (int angle = 0; angle < 360; angle += burstSeparationAngle)
            {
                float angleInRad = (float) (angle + bursOffsetAngle) * Mathf.Deg2Rad;
                Vector2 dir = new Vector2((float) Math.Cos(angleInRad), (float) Math.Sin(angleInRad));

                SoundWaveController soundWaveController =
                    Instantiate(soundWaveControllerPrefab,
                        position + new Vector3(dir.x, dir.y, 0) * burstOffsetVector, Quaternion.identity,
                        soundWaveParent.transform);
                soundWaveController.Init(entity, maxTimeAlive);
                soundWaveController.GetRigidbody().AddForce(dir * forceStrenght, ForceMode2D.Impulse);
                soundWaveController.SetLineColor(color);
            }

        }

        private void GameOver(WorldEntityController killingEntity)
        {
            foreach (var soundWaveController in _soundWaveControllers)
            {
                if (soundWaveController.GetOriginEntity() != killingEntity)
                {
                    soundWaveController.GetRigidbody().velocity = Vector2.zero;
                }
            }
            
            mainUiController.StartTransition(Color.red);
        }
        
        private void LoadNewLevel() {
            Debug.Log("Loading: " + SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}