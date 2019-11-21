using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using IAmHere.Managers;
using IAmHere.Utilities;

namespace IAmHere.MachineLearning
{
    public class IAmHereAgent : Agent
    {
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
    }
}
