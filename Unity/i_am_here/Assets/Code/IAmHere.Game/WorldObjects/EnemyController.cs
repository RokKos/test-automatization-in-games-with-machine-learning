using UnityEngine;

namespace IAmHere.Game
{
    public class EnemyController : MovingEntityController
    {
        
        
        [Range(0.0f, 100.0f)] [SerializeField] protected float radiusOfFolowPlayer = 0.1f;

        private PlayerController _playerController;
        
        
        public void Init(PlayerController playerController)
        {
            base.Init();
            _playerController = playerController;

        }

        // Update is called once per frame
        new void Update()
        {
            if (state == MovingState.kMoved && burstTimer > nextBurstTime)
            {
                ReleaseBurst();
            }
            
            state = MovingState.kIdle;
            burstTimer += Time.deltaTime;

            Vector2 dirToPlayer = MoveToPlayer();
            if (dirToPlayer.magnitude > radiusOfFolowPlayer)
            {
                return;
            }
            state = MovingState.kMoved;
            Vector2 normalizedDir = dirToPlayer.normalized;
            transform.position += new Vector3(normalizedDir.x, normalizedDir.y, 0) * movementCoeficient;

        }

        private Vector2 MoveToPlayer()
        {
            if (_playerController == null) {
                return Vector2.zero;
            }
        
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