using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using IAmHere.Managers;
using IAmHere.Utilities;

namespace IAmHere.MachineLearning
{
    public class IAmHereAgent : Agent
    {

        enum MoveInDirection
        {
            kStay = 0,
            kLeft = 1,
            kRight = 2,
            kUp = 3,
            kDown = 4
        }

        public override void AgentReset()
        {
            
            transform.position = GameManager.Instance.WorldManager.GetRandomEmptyCoordinate();
        }
        
        public override void CollectObservations()
        {
            Vector2 goalPos = GameManager.Instance.WorldManager.GetGoalPosition();
            AddVectorObs(goalPos);
            
            Vector2 agentPos = this.transform.position;
            AddVectorObs(agentPos);


            List<RayInfo> rays = RayCasting.CastRays(agentPos, 8, true);
            foreach (RayInfo ri in rays)
            {
                AddVectorObs(ri.rayLenght);
            }
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {    
            Debug.Log("Agent took action: " + textAction);
            MoveInDirection movement = (MoveInDirection)Mathf.FloorToInt(vectorAction[0]);
            //MoveAgent(movement);
            
            
            // Reward
        }
    }
}
