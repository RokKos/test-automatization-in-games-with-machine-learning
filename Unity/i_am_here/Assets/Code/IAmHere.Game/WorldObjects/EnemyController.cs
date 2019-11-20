using UnityEngine;

namespace IAmHere.Game
{
    
    enum EnemyState
    {
        kIdle,
        kMoved,
        kLast
    }

    public class EnemyController : WorldEntityController
    {
        
        [Range(0.0f, 5.0f)] [SerializeField] private float nextBurstTime = 0.3f;
        [Range(0.0f, 100.0f)] [SerializeField] protected float movementCoeficient = 0.1f;
        
        [Range(0.0f, 100.0f)] [SerializeField] protected float radiusOfFolowPlayer = 0.1f;

        private PlayerController _playerController;
        private float _burstTimer = 0.0f;
        private EnemyState _enemyState = EnemyState.kIdle;
        
        
        public void Init(PlayerController playerController)
        {
            _burstTimer = 0.0f;
            _playerController = playerController;

        }

        // Update is called once per frame
        void Update()
        {
            if (_enemyState == EnemyState.kMoved && _burstTimer > nextBurstTime)
            {
                onBurst(this, transform.position, gradient, fadeTrails, burstSeparationAngle, bursOffsetAngle, burstOffsetVector, forceStrenght, maxTimeAlive);
                _burstTimer = 0.0f;
            }
            
            _enemyState = EnemyState.kIdle;
            
            _burstTimer += Time.deltaTime;

            Vector2 dirToPlayer = MoveToPlayer();
            if (dirToPlayer.magnitude > radiusOfFolowPlayer)
            {
                return;
            }
            _enemyState = EnemyState.kMoved;
            Vector2 normalizedDir = dirToPlayer.normalized;
            transform.position += new Vector3(normalizedDir.x, normalizedDir.y, 0) * movementCoeficient;

        }

        private Vector2 MoveToPlayer()
        {
            // Purposefully casting to Vector2
            Vector2 playerPos = _playerController.transform.position;
            Vector2 enemyPos = transform.position;

            return playerPos - enemyPos;
        }
        
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            //TODO(Something)
        }
    }
}