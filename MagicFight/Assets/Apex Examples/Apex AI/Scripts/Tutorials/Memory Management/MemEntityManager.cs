/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System.Collections.Generic;

    public class MemEntityManager
    {
        public static readonly MemEntityManager instance = new MemEntityManager();

        private Dictionary<MemEntityType, List<IMemEntity>> _entitiesByType;

        public MemEntityManager()
        {
            _entitiesByType = new Dictionary<MemEntityType, List<IMemEntity>>();
        }

        public void Register(IMemEntity entity)
        {
            List<IMemEntity> allies;
            if (!_entitiesByType.TryGetValue(entity.type, out allies))
            {
                allies = new List<IMemEntity>();
                _entitiesByType[entity.type] = allies;
            }

            allies.Add(entity);
        }

        public IList<IMemEntity> GetAllies(IMemEntity entity)
        {
            List<IMemEntity> allies;
            _entitiesByType.TryGetValue(entity.type, out allies);
            return allies;
        }

        public void Unregister(IMemEntity entity)
        {
            List<IMemEntity> allies;
            if (!_entitiesByType.TryGetValue(entity.type, out allies))
            {
                return;
            }

            allies.Remove(entity);
        }
    }
}