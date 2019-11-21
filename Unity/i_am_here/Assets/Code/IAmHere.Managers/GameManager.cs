using System;
using UnityEngine;
using IAmHere.Utilities;
using IAmHere.WorldGeneration;


namespace IAmHere.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public static WorldManager WorldManager = null;

        private void Start()
        {
            // TODO(Rok Kos): Rethink this decision
            WorldManager = FindObjectOfType<WorldManager>();
        }
    }
}