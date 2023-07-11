using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatsView : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI m_Title;
    [SerializeField] Image m_FillImage;
    [SerializeField] TextMeshProUGUI m_Percentage;
  

    public void SetData(float value, string statTitle) 
    {
        m_Title.text = statTitle;
        m_FillImage.fillAmount = value / 100f;

        m_Percentage.text = (m_FillImage.fillAmount * 100f).ToString() + "%";
    }
}

