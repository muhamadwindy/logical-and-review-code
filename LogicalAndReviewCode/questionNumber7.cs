namespace LogicalAndReviewCode
{
    public class questionNumber7
    {
        public questionNumber7()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Cache.Add(i, new object());
            }
            Console.WriteLine("Cache populated");
            Console.ReadLine();
        }
    }

    internal class Cache
    {
        private static Dictionary<int, object> _cache = new Dictionary<int, object>();

        public static void Add(int key, object value)
        {
            _cache.Add(key, value);
        }

        public static object Get(int key)
        {
            return _cache[key];
        }
    }

    internal class CacheAnswer
    {
        private static readonly Dictionary<int, CacheItem> _caches = new Dictionary<int, CacheItem>();

        public static void Add(int index, object value, DateTime expired)
        {
            CacheItem cache = new CacheItem
            {
                Value = value,
                Expired = expired
            };
            _caches.Add(index, cache);
        }

        public static object Get(int index)
        {
            return _caches[index];
        }
    }

    internal class CacheItem
    {
        public object Value { get; set; }
        public DateTime Expired { get; set; }
    }
}