using System;
using IAmHere.WorldGeneration;
using UnityEngine;
using MLAgents;

namespace IAmHere.MachineLearning
{
    public class IAmHereAcademy : Academy
    {
        
        [SerializeField] private WorldManager WorldManager = null;

        public override void InitializeAcademy()
        {
            base.InitializeAcademy();
            WorldManager.Init();
        }

        public override void AcademyReset()
        {
            base.AcademyReset();
            WorldManager.Reset();
        }
    }
}
