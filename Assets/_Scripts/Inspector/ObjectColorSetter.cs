using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum RenderPipelineType
{
    Default,
    Universal,
    HDRP
}
//This class helps to change material color without changing assest from inspector
public class ObjectColorSetter : MonoBehaviour
{
    MaterialPropertyBlock mpb;
    [Tooltip("Which type of renderpipeline is used by the project")]
    [SerializeField] RenderPipelineType projectPipelineType;
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
        MeshRenderer rnd = GetComponent<MeshRenderer>();

        switch (projectPipelineType)
        {
            case RenderPipelineType.Default:
                Mpb.SetColor("_Color", m_Color);
                break;
            case RenderPipelineType.Universal:
                Mpb.SetColor("_BaseColor", m_Color);
                break;
            case RenderPipelineType.HDRP:
                Mpb.SetColor("_BaseColor", m_Color);
                break;
        }
        rnd.SetPropertyBlock(Mpb);
    }
}
