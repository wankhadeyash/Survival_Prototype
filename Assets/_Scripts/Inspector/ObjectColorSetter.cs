using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class helps to change material color without changing assest from inspector
public class ObjectColorSetter : MonoBehaviour
{
    MaterialPropertyBlock mpb;

    MaterialPropertyBlock Mpb 
    {

        get 
        {
            if (mpb == null)
                mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }
    public Color m_Color;
    private void OnValidate()
    {
        ApplyColor();   
    }

    void ApplyColor() 
    {
        Renderer rnd = GetComponent<Renderer>();
        Mpb.SetColor("_BaseColor", m_Color);
        rnd.SetPropertyBlock(Mpb);
    }
}
