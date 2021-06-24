#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// This static class shows a very simple implementation of a thread manager, to be used by a custom UtilityAIClient.
    /// The thread manager controls one dedicated thread. Utility AI clients can register themselves here and get executed.
    /// </summary>
    public static class UtilityAIThreadManager
    {
        private static List<ThreadedUtilityAIClient> _aiClients = new List<ThreadedUtilityAIClient>(2);
        private static int _currentIdx;
        private static bool _isRunning;
        private static Thread _thread;

        private static void Init()
        {
            _thread = new Thread(Execute);
            _thread.Start();
            _isRunning = true;
        }

        private static void Execute()
        {
            // Normally an interval should be used to limit the frequency of AI execution, we ignore this for simplicity here
            while (_isRunning)
            {
                lock (_aiClients)
                {
                    _currentIdx = (_currentIdx + 1) % _aiClients.Count;

                    var client = _aiClients[_currentIdx];
                    if (client.isPaused)
                    {
                        continue;
                    }

                    client.Execute();
                }
            }
        }

        public static void RegisterClient(ThreadedUtilityAIClient client)
        {
            lock (_aiClients)
            {
                if (!_aiClients.Contains(client))
                {
                    _aiClients.Add(client);

                    if (_thread == null || !_isRunning)
                    {
                        // if we added a client but have not started our thread yet, start it now
                        Init();
                    }
                }
            }
        }

        public static void UnregisterClient(ThreadedUtilityAIClient client)
        {
            lock (_aiClients)
            {
                _aiClients.Remove(client);
                if (_aiClients.Count == 0)
                {
                    // if we removed our last client, stop the thread
                    _isRunning = false;
                    _thread = null;
                }
            }
        }
    }
}