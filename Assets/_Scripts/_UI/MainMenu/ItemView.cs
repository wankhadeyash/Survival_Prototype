using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemView : MonoBehaviour
{

    [SerializeField] Image m_Icon;
    [SerializeField] TextMeshProUGUI m_DiscriptionText;

    public void SetData(ItemData data) 
    {
        m_Icon.sprite = data.icon;
        m_DiscriptionText.text = data.discription;
    }
}

