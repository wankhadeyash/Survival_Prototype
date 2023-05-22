using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodypartCustomizationbase : MonoBehaviour
{
    [SerializeField] private GameObject m_PivotGO;
    [SerializeField] private List<BodypartData> m_BodyPartSO;
    private GameObject m_EquipedBodyPart;

    public void SetBodyPart(int index) 
    {
        if (m_EquipedBodyPart)
            Destroy(m_EquipedBodyPart);

        BodypartData newBodypart = m_BodyPartSO[index];
        m_EquipedBodyPart = Instantiate(newBodypart.prefab,m_PivotGO.transform);
        m_EquipedBodyPart.transform.localPosition= newBodypart.positionOffset;
        m_EquipedBodyPart.transform.Rotate(Vector3.up, newBodypart.rotation.y);
        m_EquipedBodyPart.transform.Rotate(Vector3.right, newBodypart.rotation.x);
        m_EquipedBodyPart.transform.Rotate(Vector3.forward, newBodypart.rotation.z);


    }
}
