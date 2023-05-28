using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectionControls : MonoBehaviour
{
    [SerializeField] private GameObject m_AvatarStandPosition;

    public float rotationSpeed = 10f; // Speed of rotation

    void Update()
    {
        Rotate();
    }

    void Rotate() 
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // Get scroll wheel input

        if (scrollInput != 0)
        {
            float rotationAmount = scrollInput * rotationSpeed * Time.deltaTime;
            m_AvatarStandPosition.transform.Rotate(Vector3.up, rotationAmount, Space.World);
        }
    }

    
}

