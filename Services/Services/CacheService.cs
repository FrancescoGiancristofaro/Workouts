using MonkeyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyCache.SQLite;

namespace Services.Services
{
    public interface ICacheService
    {
        bool ScopeExists();
        void SetScope(string key);
        T Get<T>(string key);
        void Add<T>(string key, T payload);
        void Add<T>(string key, T payload, TimeSpan expiration);
        void Remove(string key);
    }
    public class CacheService : ICacheService
    {
        private readonly string _chaceDir;
        private bool _innerScopeExists { get; set; }

        public CacheService(string chaceDir)
        {
            _chaceDir = chaceDir;
        }

        public bool ScopeExists()
        {
            return _innerScopeExists;
        }

        public void SetScope(string key)
        {
            Barrel.ApplicationId = $"Workouts_{key}";
            BarrelUtils.SetBaseCachePath(_chaceDir);
            Barrel.Current.EmptyExpired();
            _innerScopeExists = true;
        }

        public T Get<T>(string key)
        {
            if (!Barrel.Current.IsExpired(key))
                return Barrel.Current.Get<T>(key: key);

            Barrel.Current.Empty(key: key);
            return default;
        }

        public void Add<T>(string key, T payload)
        {
            Add<T>(key, payload, TimeSpan.FromDays(20));
        }

        public void Add<T>(string key, T payload, TimeSpan expiration)
        {
            if (payload == null) return;

            Barrel.Current.Add(key: key, data: payload, expireIn: expiration);
        }

        public void Remove(string key)
        {
            Barrel.Current.Empty(key);
        }
    }
}
