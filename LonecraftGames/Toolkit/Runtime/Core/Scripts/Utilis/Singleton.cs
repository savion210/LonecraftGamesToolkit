using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                // This checks if the instance already exists in the scene
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();

                    // If instance still not found, create a new GameObject and add the component
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                        obj.name = typeof(T) + " (Singleton)";
                    }
                }

                return _instance;
            }
        }

        // Optionally, make sure the singleton instance is not destroyed on scene load
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                if (this != _instance)
                    Destroy(this.gameObject);
            }
        }
    }
}