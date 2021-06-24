/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using UnityEngine;

    public class MemObservation
    {
        public MemObservation(IMemEntity entity, float time)
        {
            this.entity = entity;
            this.position = entity.position;
            this.timestamp = time;
        }

        public IMemEntity entity
        {
            get;
            private set;
        }

        public Vector3 position
        {
            get;
            private set;
        }

        public float timestamp
        {
            get;
            private set;
        }

        public MemObservation Clone()
        {
            return new MemObservation(this.entity, this.timestamp)
            {
                position = this.position
            };
        }
    }
}
