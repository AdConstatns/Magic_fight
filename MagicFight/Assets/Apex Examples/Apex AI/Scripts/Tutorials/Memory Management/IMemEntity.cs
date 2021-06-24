#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System.Collections.Generic;
    using UnityEngine;

    public interface IMemEntity
    {
        MemEntityType type { get; }

        MemMemory memory { get; }

        Vector3 position { get; }

        /// <summary>
        /// Receives a list of communicated memory observations and adds newer observations to own memory.
        /// One could also choose to send the entire context for other units to read from.
        /// </summary>
        /// <param name="observations">The observations.</param>
        void ReceiveCommunicatedMemory(IList<MemObservation> observations);
    }
}