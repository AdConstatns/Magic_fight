namespace AmazingTeam.MagicFight
{

    using UnityEngine;
    using Apex;
    using Apex.AI;
    using Apex.Steering.Components;
    using Apex.Units;

    public class ExtendedUnitFacade : UnitFacade
    {
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
