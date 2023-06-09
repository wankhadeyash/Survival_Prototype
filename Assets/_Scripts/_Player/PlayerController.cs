using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;


public class PlayerController : NetworkBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        StartCoroutine(Co_SetCameraFllowAndLookAt());
    }

    private IEnumerator Co_SetCameraFllowAndLookAt()
    {
        yield return new WaitForSeconds(2f);
        CameraController.SetCameraFollowAndLookAt(transform, transform);
    }

}

