#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    public sealed class AreWeInMachineGunKillZone : OptionScorerBase<GameObject>
    {
        [ApexSerialization]
        public float score = 30f;

        [ApexSerialization]
        public float killzoneStart = 5f;

        [ApexSerialization]
        public float killZoneEnd = 15f;

        public override float Score(IAIContext context, GameObject option)
        {
            var c = (ExampleContext)context;

            // components should normally be cached and not 'get' on every tick, simplified here for tutorial purposes
            var weapon = option.GetComponent<WeaponComponent>();
            if (weapon.HasMachineGun())
            {
                var distance = (c.self.transform.position - option.transform.position).sqrMagnitude;
                if (distance >= (this.killzoneStart * this.killzoneStart) && distance <= (this.killZoneEnd * this.killZoneEnd))
                {
                    return this.score;
                }
            }

            return 0f;
        }
    }
}