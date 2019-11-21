using UnityEngine;

namespace IAmHere.Game
{
    

    public class PlayerController : MovingEntityController
    {

        public delegate void OnPlayerDead(WorldEntityController killingEntity);
        public OnPlayerDead onPlayerDead;
        
        public delegate void OnLevelClear();
        public OnLevelClear onLevelClear;
        
        
        private bool playerDead = false;

        // Update is called once per frame
        void Update() {

            if (state == MovingState.kMoved && burstTimer > nextBurstTime)
            {
                ReleaseBurst();
            }

            state = MovingState.kIdle;

            if (playerDead)
            {
                return;
            }

            burstTimer += Time.deltaTime;


            // Input handling
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * movementCoeficient;
                state = MovingState.kMoved;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * movementCoeficient;
                state = MovingState.kMoved;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * movementCoeficient;
                state = MovingState.kMoved;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += Vector3.down * movementCoeficient;
                state = MovingState.kMoved;
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
                state = MovingState.kHurt;
            }
        }
    }
}