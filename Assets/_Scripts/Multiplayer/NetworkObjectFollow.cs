using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkObjectFollow : MonoBehaviour
{
    public Transform followObject;

    void Awake()
    {

    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (followObject != null)
        {
            transform.position = followObject.position;
            transform.rotation = followObject.rotation;
        }
    }

    private void LateUpdate()
    {
        
    }

}

