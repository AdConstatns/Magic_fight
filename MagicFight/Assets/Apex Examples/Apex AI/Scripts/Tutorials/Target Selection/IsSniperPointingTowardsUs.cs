#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    public sealed class IsSniperPointingTowardsUs : OptionScorerBase<GameObject>
    {
        [ApexSerialization]
        public float score = 40f;

        [ApexSerialization]
        public float maxAngle = 30f;

        public override float Score(IAIContext context, GameObject option)
        {
            var c = (ExampleContext)context;

            // components should normally be cached and not 'get' on every tick, simplified here for tutorial purposes
            var weapon = option.GetComponent<WeaponComponent>();

            if (weapon.IsSniper())
            {
                var angle = Vector3.Angle(option.transform.forward, (c.self.transform.position - option.transform.position));
                if (angle < this.maxAngle)
                {
                    return this.score;
                }
            }

            return 0f;
        }
    }
}