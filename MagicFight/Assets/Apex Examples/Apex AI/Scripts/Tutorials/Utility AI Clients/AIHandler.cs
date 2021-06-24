#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System;
    using System.Collections.Generic;
    using Apex.AI;
    using Apex.AI.Components;
    using UnityEngine;

    public static class AIHandler
    {
        private static readonly Dictionary<GameObject, IUtilityAIClient> _clients;

        static AIHandler()
        {
            _clients = new Dictionary<GameObject, IUtilityAIClient>();
            AIManager.GetAIClient = (GameObject host, Guid aiId) =>
            {
                return _clients.Value(host);
            };
        }

        internal static void Start(GameObject go, AIClientType clientType)
        {
            var contextProvider = new ContextProvider(new ExampleContext(go));

            IUtilityAIClient client;
            if (clientType == AIClientType.Threaded)
            {
                client = new ThreadedUtilityAIClient(AINameMap.CustomUtilityAI, contextProvider);
            }
            else
            {
                client = new LoadBalancedUtilityAIClient(AINameMap.CustomUtilityAI, contextProvider);
            }

            _clients.Add(go, client);
            client.Start();
        }

        internal static void Stop(GameObject go)
        {
            IUtilityAIClient client;
            if (_clients.TryGetValue(go, out client))
            {
                client.Stop();
                _clients.Remove(go);
            }
        }

        private class ContextProvider : IContextProvider
        {
            private IAIContext _context;

            public ContextProvider(IAIContext context)
            {
                _context = context;
            }

            public IAIContext GetContext(Guid aiId)
            {
                return _context;
            }
        }
    }
}