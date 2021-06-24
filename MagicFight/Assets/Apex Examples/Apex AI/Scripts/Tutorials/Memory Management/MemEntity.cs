/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System;
    using System.Collections.Generic;
    using Apex.AI;
    using Apex.AI.Components;
    using UnityEngine;

    public class MemEntity : IMemEntity, IContextProvider
    {
        private MemContext _context;

        public MemEntity(MemEntityType type)
        {
            this.type = type;
            this.memory = new MemMemory();
            _context = new MemContext(this);
        }

        public MemEntityType type
        {
            get;
            private set;
        }

        public MemMemory memory
        {
            get;
            private set;
        }

        public Vector3 position
        {
            get;
            private set;
        }

        public void ReceiveCommunicatedMemory(IList<MemObservation> observations)
        {
            var count = observations.Count;
            for (int i = 0; i < count; i++)
            {
                if (object.ReferenceEquals(observations[i].entity, this))
                {
                    // don't store observation of "self"
                    continue;
                }

                // clone the observation and store it (Using an observation pool is recommended)
                var newObs = observations[i].Clone();
                this.memory.AddOrUpdateObservation(newObs, true);
            }
        }

        public IAIContext GetContext(Guid aiId)
        {
            return _context;
        }
    }
}