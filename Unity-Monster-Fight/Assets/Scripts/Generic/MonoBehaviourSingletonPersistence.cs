using UnityEngine;

namespace Game.Generic
{
    public class MonoBehaviourSingletonPersistence<T> : MonoBehaviour
        where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject(nameof(T))
                        {
                            hideFlags = HideFlags.HideAndDontSave
                        };
                        _instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake ()
        {
            if (_instance != null && _instance != this)
            {
                Destroy (gameObject);
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad (this);
            }
        }
    }
}
