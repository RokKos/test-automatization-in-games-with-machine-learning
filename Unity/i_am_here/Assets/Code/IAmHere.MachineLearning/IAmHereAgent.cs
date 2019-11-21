using MLAgents;
using IAmHere.Managers;

namespace IAmHere.MachineLearning
{
    public class IAmHereAgent : Agent
    {
        public override void AgentReset()
        {
            
            transform.position = GameManager.Instance.WorldManager.GetRandomEmptyCoordinate();  
        }
    }
}
