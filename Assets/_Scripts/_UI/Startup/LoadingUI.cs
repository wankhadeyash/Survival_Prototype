using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class LoadingUI : SingletonBase<LoadingUI>
{

    [SerializeField] GameObject m_Container;
    [SerializeField] TextMeshProUGUI m_LoadingText;
    [SerializeField] float m_DotInterval;
    bool m_IsEnabled;

    void Start()
    {
        
    }

    public void Show(string message)
    {
        m_LoadingText.text = message;
        m_Container.SetActive(true);
        m_IsEnabled = true;
        StopAllCoroutines();
        StartCoroutine(Co_Animate());
    }

    public IEnumerator Show(string message, float time) 
    {
        m_LoadingText.text = message;
        m_Container.SetActive(true);
        m_IsEnabled = true;
        StopAllCoroutines();
        StartCoroutine(Co_Animate());

        yield return new WaitForSecondsRealtime(time);
        Hide();
    }

    public void Hide() 
    {
        m_LoadingText.text = "";
        m_Container.SetActive(false);
        m_IsEnabled = false;

    }
    public IEnumerator Hide(float time)
    {
       yield return new WaitForSecondsRealtime(time);
       Hide();
    }
    private IEnumerator Co_Animate() 
    {
        string originalText = m_LoadingText.text;
        while (m_IsEnabled) 
        {
            m_LoadingText.text = originalText + ".";
            yield return new WaitForSecondsRealtime(m_DotInterval);
            m_LoadingText.text = originalText + "..";
            yield return new WaitForSecondsRealtime(m_DotInterval);
            m_LoadingText.text = originalText + "...";
            yield return new WaitForSecondsRealtime(m_DotInterval);
            m_LoadingText.text = originalText;
            yield return new WaitForSecondsRealtime(m_DotInterval);

        }
    }

}

