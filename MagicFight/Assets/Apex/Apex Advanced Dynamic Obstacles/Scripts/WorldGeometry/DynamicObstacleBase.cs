/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    public partial class DynamicObstacleBase
    {
        /// <summary>
        /// Refreshes the hull of the dynamic obstacle. Only relevant for MeshCollider obstacles and should only be called if the mesh changes at runtime (actual changes, not transform scaling or rotation).
        /// </summary>
        public virtual void RefreshHull()
        {
        }
    }
}
