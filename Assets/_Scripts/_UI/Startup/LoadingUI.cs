using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class LoadingUI : MonoBehaviour
{

    [SerializeField] GameObject m_Container;
    [SerializeField] TextMeshProUGUI m_LoadingText;
    bool m_IsEnabled;
    void Awake()
    {

    }

    void Start()
    {
        Show("Connecting to unity");
    }

    public void Show(string message)
    {
        m_LoadingText.text = message;
        m_Container.SetActive(true);
        m_IsEnabled = true;
        StartCoroutine(Co_Animate());
    }

    public void Hide() 
    {
        m_LoadingText.text = "";
        m_Container.SetActive(false);
        m_IsEnabled = false;

    }

    private IEnumerator Co_Animate() 
    {
        string originalText = m_LoadingText.text;
        while (m_IsEnabled) 
        {
            m_LoadingText.text = originalText + ".";
            yield return new WaitForSecondsRealtime(2f);
            m_LoadingText.text = originalText + "..";
            yield return new WaitForSecondsRealtime(1f);
            m_LoadingText.text = originalText + "....";
            yield return new WaitForSecondsRealtime(1f);
            m_LoadingText.text = originalText;
            yield return new WaitForSecondsRealtime(1f);

        }
    }

}

