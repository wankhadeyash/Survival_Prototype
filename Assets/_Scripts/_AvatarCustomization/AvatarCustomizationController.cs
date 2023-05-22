using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AvatarCustomizationController : MonoBehaviour
{
    [SerializeField] private HairCustomization m_HairCustomization;
    // Start is called before the first frame update
    void Start()
    {
        m_HairCustomization.SetBodyPart(0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
            m_HairCustomization.SetBodyPart(0);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            m_HairCustomization.SetBodyPart(1);

    }

    void SetHair() 
    {
        
    }

}
