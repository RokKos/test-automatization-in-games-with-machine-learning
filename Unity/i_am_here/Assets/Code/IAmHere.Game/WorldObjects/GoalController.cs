using UnityEngine;

namespace IAmHere.Game
{
    public class GoalController : WorldEntityController
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                // TODO(Rok Kos): Create action to game manager that is level cleared
                Burst(Color.white);
                Debug.Log("END");
            }
        }
    }
}