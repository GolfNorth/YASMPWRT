using System;
using System.Collections;
using System.Collections.Generic;

namespace YASMPWRT.Managers
{
    public sealed class Director : Singleton<Director>
    {
        private Dictionary<Type, object> _objects;
        private UpdateManager _updateManager;

        public void Set<T>(T obj)
        {
            var type = typeof(T);
            
            if (_objects.ContainsKey(type))
                return;
            
            _objects.Add(type, obj);
        }

        public T Get<T>()
        {
            var type = typeof(T);
            
            _objects.TryGetValue(type, out var result);

            if (result is null)
            {
                var constructor = type.GetConstructor(Type.EmptyTypes);

                if (constructor != null)
                {
                    result = Activator.CreateInstance(type);
                }
            }

            return (T) result;
        }

        public void Remove<T>(T obj)
        {
            var type = typeof(T);
            
            if (!_objects.ContainsKey(type))
                return;
            
            _objects.Remove(type);
        }

        private void Awake()
        {
            _objects = new Dictionary<Type, object>();

            _updateManager = Instance.Get<UpdateManager>();
        }

        private void Update()
        {
            _updateManager.Tick();
        }

        private void LateUpdate()
        {
            _updateManager.TickLate();
        }

        private void FixedUpdate()
        {
            _updateManager.TickFixed();
        }
        
        public void RunCoroutine(IEnumerator method)
        {
            StartCoroutine(method);
        }
    }
}