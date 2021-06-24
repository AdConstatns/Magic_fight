#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using UnityEngine;

    public class ExampleObservation
    {
        public ExampleObservation(GameObject go, bool isVisible, float timestamp)
        {
            this.gameObject = go;
            this.position = go.transform.position; // position is 'snap-shot' (due to it being a struct), whereas the game object is held as a reference
            this.isVisible = isVisible;
            this.timestamp = timestamp;
        }

        public GameObject gameObject
        {
            get;
            private set;
        }

        public float timestamp
        {
            get;
            private set;
        }

        public bool isVisible
        {
            get;
            private set;
        }

        public Vector3 position
        {
            get;
            private set;
        }
    }
}