using UnityEngine;

namespace IAmHere.Game
{
    public class PlayerController : ControlableEntityController
    {
        
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

            Vector2 dir = Vector2.zero;
            // Input handling
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                dir.x += -1;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                dir.x += 1;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                dir.y += 1;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                dir.y += -1;
            }
            
            MoveEntity(dir);
        }
    }
}