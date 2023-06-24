using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    [SerializeField] protected bool m_IsDontDestroyOnLoad = true;
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
            Destroy(gameObject);
            return;
        }
        OnAwake();
       if(m_IsDontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
    protected virtual void OnAwake() { }
}
