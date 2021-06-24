#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using UnityEngine;

    public sealed class WeaponComponent : MonoBehaviour
    {
        public bool isHoldingGrenade;
        public bool isSniper;
        public bool hasMachineGun;

        public bool IsHoldingGrenade()
        {
            return this.isHoldingGrenade;
        }

        public bool IsSniper()
        {
            return this.isSniper;
        }

        public bool HasMachineGun()
        {
            return this.hasMachineGun;
        }
    }
}