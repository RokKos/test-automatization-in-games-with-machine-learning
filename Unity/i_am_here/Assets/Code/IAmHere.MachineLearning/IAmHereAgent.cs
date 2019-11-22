using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using IAmHere.Managers;
using IAmHere.Utilities;
using IAmHere.Game;

namespace IAmHere.MachineLearning
{
    public class IAmHereAgent : Agent
    {
        [SerializeField] private ControlableEntityController _controlableEntityController = null;

        public override void AgentReset()
        {
            GameManager.Instance.WorldManager.SetupControlableEntityEntity(_controlableEntityController);
            //_controlableEntityController.transform.position = GameManager.Instance.WorldManager.GetRandomEmptyCoordinate();
            _controlableEntityController.transform.position = GameManager.Instance.WorldManager.GetStartCoordinate();
            _controlableEntityController.GetRigidbody2D().velocity = Vector2.zero;
            _controlableEntityController.Init();
        }
        
        public override void CollectObservations()
        {
            Vector2 goalPos = GameManager.Instance.WorldManager.GetGoalPosition();
            AddVectorObs(goalPos);
            
            Vector2 agentPos = _controlableEntityController.transform.position;
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
            int moveX = Mathf.FloorToInt(vectorAction[0]) - 1;
            int moveY = Mathf.FloorToInt(vectorAction[1]) - 1;
            _controlableEntityController.MoveEntity(new Vector2(moveX, moveY));
            
            Vector2 goalPos = GameManager.Instance.WorldManager.GetGoalPosition();
            Vector2 distanceVec = (Vector2)_controlableEntityController.transform.position - goalPos;
            float distanceToGoal = distanceVec.magnitude;
            float maxLevelDistance = GameManager.Instance.WorldManager.GetMaxLenOfLevel();

            if (distanceToGoal > maxLevelDistance)
            {
                SetReward(0);
            }
            else
            {
                float distanceReward = 1.0f - distanceToGoal / maxLevelDistance;
                distanceReward = Mathf.Pow(distanceReward, 2);
                SetReward(distanceReward);
            }

            
            
            if (_controlableEntityController.GetPlayerDead())
            {
                SetReward(-1);
                Done();
            }
            
            if (_controlableEntityController.GetPlayerWon())
            {
                SetReward(1);
                Done();
            }
        }
    }
}
