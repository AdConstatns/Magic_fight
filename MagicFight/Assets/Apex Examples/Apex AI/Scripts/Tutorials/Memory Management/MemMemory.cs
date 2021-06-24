#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System.Collections.Generic;

    public class MemMemory
    {
        private List<MemObservation> _observations;

        public MemMemory()
        {
            _observations = new List<MemObservation>(10);
        }

        public List<MemObservation> allObservations
        {
            get { return _observations; }
        }

        public void AddOrUpdateObservation(MemObservation observation, bool checkTimestamp)
        {
            var count = _observations.Count;
            for (int i = 0; i < count; i++)
            {
                var obs = _observations[i];
                if (!object.ReferenceEquals(obs.entity, observation.entity))
                {
                    continue;
                }

                if (checkTimestamp && obs.timestamp >= observation.timestamp)
                {
                    return;
                }

                _observations[i] = observation;
                return;
            }

            _observations.Add(observation);
        }

        public void RemoveObservationAt(int index)
        {
            _observations.RemoveAt(index);
        }
    }
}