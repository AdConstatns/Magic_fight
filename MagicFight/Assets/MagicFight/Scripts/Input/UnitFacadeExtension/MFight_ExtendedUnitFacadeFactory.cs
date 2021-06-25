namespace AmazingTeam.MagicFight {

    using Apex.Units;
    using UnityEngine;

    [AddComponentMenu("MagicFight/Input/UnitFacades/Extended Unit Facade Factory", 1012)]
    public class MFight_ExtendedUnitFacadeFactory : MonoBehaviour, IUnitFacadeFactory {
        public IUnitFacade CreateUnitFacade() {
            return new MFight_ExtendedUnitFacade();
        }
    }
}



  
   