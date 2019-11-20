using System;
using UnityEngine;
using MLAgents;

namespace IAmHere.MachineLearning
{
    public class IAmHereAgent : Agent
    {
        
        //[SerializeField] private Controller nekController = Nullable;
        public override void AgentReset()
        {
            //transform.position = WorldManager.GetRandomEmptySpace();  
        }
    }
}
