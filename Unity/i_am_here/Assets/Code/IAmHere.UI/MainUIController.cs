using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


namespace IAmHere.UI
{
    public class MainUIController : MonoBehaviour
    {
        public delegate void OnTransitionOver();
        public OnTransitionOver onTransitionOver;

        [SerializeField] private PlayableDirector seqTransition = null;
        [SerializeField] private Image imgProgressionTransition = null;
        
        public void TransitionOver()
        {
            Debug.Log("Transition Over");
            onTransitionOver();
        }

        public void StartTransition(Color color)
        {
            imgProgressionTransition.color = color;    
            seqTransition.Play();
        }
    }
}