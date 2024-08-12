using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelloScripts
{
    public class SingleMonoBehaviour<T> : MonoBehaviour where T : SingleMonoBehaviour<T>
    {
        private static volatile T _instance;

        private bool _isInitialized;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindFirstObjectByType(typeof(T)) as T;
                if (_instance != null && !_instance._isInitialized) Instance.Initialize();
                return _instance;
            }
        }

        protected virtual void Initialize()
        {
            _isInitialized = true;
        }
    }
}


