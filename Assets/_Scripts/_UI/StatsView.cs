using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatsView : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI m_Nameaa;
    [SerializeField] Image m_FillImage;
    [SerializeField] TextMeshProUGUI m_Percentage;
    void Awake()
    {

    }

    void Start()
    {

    }

    public void SetData(StatsData data) 
    {
        m_Nameaa.text = data.name;
        Debug.Log($"Value is {data.value * 100}");
        m_FillImage.fillAmount = data.value / 100f;

        m_Percentage.text = (m_FillImage.fillAmount * 100f).ToString() + "%";
    }
}

