using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;


public class PlayerController : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        CameraController.SetCameraFollowAndLookAt(transform, transform);
    }

}

