namespace AmazingTeam.MagicFight
{
    using Apex.Steering.Components;
    using Apex.Units;
    using UnityEngine;

    public class MFight_ExtendedUnitFacade : UnitFacade {

        private HumanoidSpeedComponent _speeder;

        public override void Initialize(GameObject unitObject) {
            base.Initialize(unitObject);
            _speeder = unitObject.GetComponent<HumanoidSpeedComponent>();
        }

        public void Run() {
            _speeder.Run();
        }
    }
}
