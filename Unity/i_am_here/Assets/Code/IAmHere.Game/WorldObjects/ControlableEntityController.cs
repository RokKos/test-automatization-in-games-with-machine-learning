
using UnityEngine;

namespace IAmHere.Game
{
    public class ControlableEntityController : MovingEntityController
    {
        public delegate void OnPlayerDead(WorldEntityController killingEntity);
        public OnPlayerDead onPlayerDead;
        
        public delegate void OnLevelClear();
        public OnLevelClear onLevelClear;
        
        
        protected bool playerDead = false;
        protected bool playerWon = false;
        
        public void Init()
        {
            base.Init();
            playerDead = false;
            playerWon = false;
        }
        
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Goal")
            {
                onLevelClear();
                playerWon = true;
                return;
            }


            if (other.gameObject.tag == "Enviroment")
            {
                playerDead = true;
                onPlayerDead(other.gameObject.GetComponent<WorldEntityController>());
                state = MovingState.kHurt;
            }
        }

        protected void MoveEntity(Vector2 dir)
        {
            if (dir != Vector2.zero)
            {
                state = MovingState.kMoved;
            }
            
            transform.position += new Vector3(dir.x, dir.y, 0) * movementCoeficient;
            
        }

    }
}