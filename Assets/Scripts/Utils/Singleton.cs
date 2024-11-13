using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    [SerializeField] private bool destroyOnLoad;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            if (!instance.GetComponent<Singleton<T>>().destroyOnLoad)
            {
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    
}
