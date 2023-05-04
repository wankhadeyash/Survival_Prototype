using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T: SingletonBase<T>
{
    static T s_Instance;
    public static T Instance => s_Instance;

    private void Awake()
    {
        if (s_Instance == null)
        {
            //Set instance to this
            s_Instance = this as T;
        }
        else 
        {
            //Destroy and set instance to null
            s_Instance = null;
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
