#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    public sealed class GameObjectProximityScorer : OptionScorerBase<GameObject>
    {
        [ApexSerialization]
        public float score = 10f;

        [ApexSerialization]
        public float weight = 1f;

        public override float Score(IAIContext context, GameObject option)
        {
            var c = (ExampleContext)context;
            var distance = (c.self.transform.position - option.transform.position).magnitude;
            return Mathf.Max(0f, (this.score - distance) * this.weight);
        }
    }
}