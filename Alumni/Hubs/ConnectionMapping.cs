using System.Collections.Concurrent;

namespace Alumni.Hubs
{
    // Thread-safe: user တစ်ယောက်ကို connection (tab/device) များစွာ track လုပ်ပေးတယ်
    public class ConnectionMapping<T> where T : notnull
    {
        private readonly ConcurrentDictionary<T, HashSet<string>> _connections = new();

        public int Add(T key, string connectionId)
        {
            var connections = _connections.GetOrAdd(key, _ => new HashSet<string>());
            lock (connections)
            {
                connections.Add(connectionId);
                return connections.Count;
            }
        }

        // Returns remaining connection count after removal
        public int Remove(T key, string connectionId)
        {
            if (!_connections.TryGetValue(key, out var connections))
            {
                return 0;
            }

            lock (connections)
            {
                connections.Remove(connectionId);
                var count = connections.Count;
                if (count == 0)
                {
                    _connections.TryRemove(key, out _);
                }
                return count;
            }
        }

        public bool IsOnline(T key) => _connections.ContainsKey(key);
    }
}