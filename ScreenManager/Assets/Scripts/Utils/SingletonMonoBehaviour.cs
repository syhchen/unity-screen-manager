using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogErrorFormat("No instance of {0} found", typeof(T).ToString());
            }

            return _instance;
        }
    }

    public static bool Exists
    {
        get
        {
            return (_instance != null);
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogErrorFormat("Instance of {0} already exists", typeof(T).ToString());
        }
        else
        {
            _instance = this as T;
        }
    }
}
