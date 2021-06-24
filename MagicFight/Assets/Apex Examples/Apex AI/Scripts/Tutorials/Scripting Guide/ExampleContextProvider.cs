#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System;
    using Apex.AI;
    using Apex.AI.Components;
    using UnityEngine;

    public class ExampleContextProvider : MonoBehaviour, IContextProvider
    {
        private IAIContext _context;

        private void Awake()
        {
            _context = new ExampleContext(this.gameObject);
        }

        public IAIContext GetContext(Guid aiId)
        {
            return _context;
        }
    }
}