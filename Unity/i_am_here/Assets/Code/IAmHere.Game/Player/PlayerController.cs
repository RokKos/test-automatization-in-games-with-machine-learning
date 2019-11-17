using UnityEngine;

namespace IAmHere.Game
{
    enum PlayerState
    {
        kPlayerIdle,
        kPlayerMoved,
        kPlayerHurt,
        kLast
    }

    public class PlayerController : WorldEntityController
    {

        public delegate void OnPlayerDead(WorldEntityController killingEntity);
        public OnPlayerDead onPlayerDead;
        
        public delegate void OnLevelClear();
        public OnLevelClear onLevelClear;
        
        [Range(0.0f, 5.0f)] [SerializeField] private float nextBurstTime = 0.3f;
        [Range(0.0f, 100.0f)] [SerializeField] protected float movementCoeficient = 0.1f;

        private float burstTimer = 0.0f;

        private PlayerState playerState = PlayerState.kPlayerIdle;
        private bool playerDead = false;

        // Start is called before the first frame update
        void Start()
        {
            burstTimer = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {

            switch (playerState)
            {
                case PlayerState.kPlayerMoved:
                    if (burstTimer > nextBurstTime)
                    {
                        onBurst(this, transform.position, gradient, fadeTrails, burstSeparationAngle, bursOffsetAngle, burstOffsetVector, forceStrenght, maxTimeAlive);
                        burstTimer = 0.0f;
                    }

                    break;
                
                default:
                    break;
            }

            playerState = PlayerState.kPlayerIdle;

            if (playerDead)
            {
                return;
            }

            burstTimer += Time.deltaTime;


            // Input handling
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * movementCoeficient;
                playerState = PlayerState.kPlayerMoved;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * movementCoeficient;
                playerState = PlayerState.kPlayerMoved;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * movementCoeficient;
                playerState = PlayerState.kPlayerMoved;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += Vector3.down * movementCoeficient;
                playerState = PlayerState.kPlayerMoved;
            }
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Goal")
            {
                onLevelClear();
                return;
            }


            if (other.gameObject.tag == "Enviroment")
            {
                // TODO(Rok Kos): Create action to game manager that is game over...
                playerDead = true;
                onPlayerDead(other.gameObject.GetComponent<WorldEntityController>());
                playerState = PlayerState.kPlayerHurt;
            }
        }

    }
}