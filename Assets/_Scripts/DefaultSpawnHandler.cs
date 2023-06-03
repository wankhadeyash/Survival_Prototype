using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;


public class DefaultSpawnHandler : NetworkBehaviour
{
    [SerializeField] private Vector3 m_SpawnPosition;
    [SerializeField] private Quaternion m_SpawnRotation;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        transform.position = m_SpawnPosition;
        transform.rotation = m_SpawnRotation;
    }



}

