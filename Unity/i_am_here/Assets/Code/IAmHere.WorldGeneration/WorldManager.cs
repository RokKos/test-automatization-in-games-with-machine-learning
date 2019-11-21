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
        private int levelIndex = 4;
        private List<ColliderController> levelColliders = null;

        [Header("Player")] 
        [SerializeField] private PlayerController playerControllerPrefab = null;
        [SerializeField] private GoalController goalControllerPrefab = null;
        [SerializeField] private EnemyController enemyControllerPrefab = null;
        [SerializeField] private SoundWaveController soundWaveControllerPrefab = null;
        [SerializeField] private GameObject soundWaveParent = null;

        private PlayerController _playerController = null;
        private GoalController _goalController = null;
        private List<SoundWaveController> _soundWaveControllers = null;
        private List<EnemyController> _enemyControllers = null;
        
        [Header("Camera")]
        [SerializeField] private MainCameraController mainCameraController = null;
        
        [Header("UI")]
        [SerializeField] private MainUIController mainUiController = null;
        
        static System.Random random = new System.Random();

        void Start()
        {
            mainUiController.onTransitionOver += Reset;
            LoadNewLevel();
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
                    collider.name = "y: " + y + " x: " + x;
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
                    int index = GetGridIndex(level.columns, y, x);

                    switch (level.board[index])
                    {
                        case Square.kStart:
                        {
                            _playerController = Instantiate(playerControllerPrefab,
                                new Vector3(x - 0.5f,-y + 0.5f , 0), Quaternion.identity, WorldParent.transform);
                            _playerController.onBurst += Burst;
                            _playerController.onPlayerDead += GameOver;
                            _playerController.onLevelClear += NextLevel;
                            break;
                        }
                        case Square.kEnd:
                        {
                            _goalController = Instantiate(goalControllerPrefab,
                                new Vector3(x - 0.5f,-y + 0.5f , 0), Quaternion.identity, WorldParent.transform);
                            _goalController.onBurst += Burst;  
                            break;
                        }

                        case Square.kEnemy:
                        {
                              
                            EnemyController enemyController = Instantiate(enemyControllerPrefab,
                                new Vector3(x - 0.5f,-y + 0.5f , 0), Quaternion.identity, WorldParent.transform);
                            enemyController.Init(_playerController);
                            enemyController.onBurst += Burst;  
                            _enemyControllers.Add(enemyController);
                            break;
                        }
                        
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

        // TODO(Rok Kos): Create helper class with this kind of functions
        public static int GetGridIndex(int columns, int y, int x)
        {
            return y * columns + x;
        }

        private void NextLevel()
        {
            levelIndex++;
            mainUiController.StartTransition(Color.white);
        }
        
        private void Burst(WorldEntityController entity, Vector3 position, Gradient gradient, bool fadeTrails, int burstSeparationAngle, int bursOffsetAngle, float burstOffsetVector,
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
                soundWaveController.Init(entity, maxTimeAlive, gradient, fadeTrails, dir, forceStrenght);
                soundWaveController.onDestroy += CleanSoundWave;
                _soundWaveControllers.Add(soundWaveController);
            }

        }

        private void CleanSoundWave(SoundWaveController entity)
        {
            entity.onDestroy -= CleanSoundWave;
            _soundWaveControllers.Remove(entity);

        }

        private void GameOver(WorldEntityController killingEntity)
        {
            foreach (var soundWaveController in _soundWaveControllers)
            {
                if (soundWaveController != null && soundWaveController.GetOriginEntity() != killingEntity)
                {
                    soundWaveController.GetRigidbody().velocity = Vector2.zero;
                }
            }
            
            mainUiController.StartTransition(Color.red);
        }
        
        private void ClearOldLevel() {
            foreach (var soundWaveController in _soundWaveControllers)
            {
                if (soundWaveController != null)
                {
                    Destroy(soundWaveController.gameObject);    
                }
            }
            
            foreach (var enemyController in _enemyControllers)
            {
                if (enemyController != null)
                {
                    Destroy(enemyController.gameObject);    
                }
            }
            
            foreach (var levelCollider in levelColliders)
            {
                levelCollider.onBurst -= Burst;
                Destroy(levelCollider.gameObject);   
            }
            _playerController.onBurst -= Burst;
            _playerController.onPlayerDead -= GameOver;
            _playerController.onLevelClear -= NextLevel;
            Destroy(_playerController.gameObject);
            
            _goalController.onBurst -= Burst;  
            Destroy(_goalController.gameObject);
        }
        
        private void LoadNewLevel() {
            _soundWaveControllers = new List<SoundWaveController>();
            _enemyControllers = new List<EnemyController>();
            levelColliders = new List<ColliderController>();
            Level level = GetCurrLevel();
            CreateGrid(marchingSquares.ParseGrid(marchingSquares.ConvertLevelToGrid(level)));
            SpawnEntities();
            mainCameraController.Init(level.columns, level.rows);
        }
        
        private void Reset() {
            ClearOldLevel();
            LoadNewLevel();
        }

        private List<Vector2> GetEmptyCoordinates()
        {
            List<Vector2> emptyCoordinates = new List<Vector2>();
            Level level = GetCurrLevel();
            for (int y = 0; y < level.rows; ++y)
            {
                for (int x = 0; x < level.columns; ++x)
                {
                    int index = GetGridIndex(level.columns, y, x);
                    if (level.board[index] == Square.kEmtpy)
                    {
                        emptyCoordinates.Add(new Vector2(x, -y));
                    }
                }
            }

            return emptyCoordinates;
        }

        public Vector2 GetRandomEmptyCoordinate(bool playerEntity = true)
        {
            List<Vector2> emptyCoordinates = GetEmptyCoordinates();
            int index = random.Next(emptyCoordinates.Count);
            Vector2 emptyCoordinate = emptyCoordinates[index];
            
            if (playerEntity)
            {
                emptyCoordinate += new Vector2(-0.5f, 0.5f);
            }

            return emptyCoordinate;
        }

        public Vector2 GetGoalPosition()
        {
            if (_goalController == null)
            {
                return Vector2.zero;
            }

            return _goalController.transform.position;
        }
    }
}