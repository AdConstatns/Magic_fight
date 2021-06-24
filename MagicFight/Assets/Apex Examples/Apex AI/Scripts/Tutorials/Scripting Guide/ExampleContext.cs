#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System.Collections.Generic;
    using Apex.AI;
    using UnityEngine;

    public class ExampleContext : IAIContext
    {
        public ExampleContext(GameObject gameObject)
        {
            this.self = gameObject;
            this.observations = new List<GameObject>();
            this.exampleObservations = new Dictionary<GameObject, ExampleObservation>();
            this.sampledPositions = new List<Vector3>();
        }

        public GameObject self
        {
            get;
            private set;
        }

        public GameObject attackTarget
        {
            get;
            set;
        }

        public Vector3 moveTarget
        {
            get;
            set;
        }

        public List<GameObject> observations
        {
            get;
            private set;
        }

        public List<Vector3> sampledPositions
        {
            get;
            private set;
        }

        public Dictionary<GameObject, ExampleObservation> exampleObservations
        {
            get;
            private set;
        }

        public void AddOrUpdateObservation(ExampleObservation obs)
        {
            this.exampleObservations[obs.gameObject] = obs;
        }
    }
}