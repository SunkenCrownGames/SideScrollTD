using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewritterEffect : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text = null;
    [SerializeField]
    private bool m_playOnStart = true;


    [SerializeField]
    private int m_letterCounter = 0;
    [SerializeField]
    private int m_totalLetters = 0;
    [SerializeField]
    private float m_effectSpeed = 1;

    private void Awake()
    {
        m_totalLetters = m_text.text.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_text.maxVisibleCharacters = m_letterCounter;

        if (m_playOnStart)
        {
            StartCoroutine("StartEffect");
        }
    }

    public void StartTextEffect()
    {
        StartCoroutine("StartEffect");
    }


    public void EndTextEffect()
    {
        StartCoroutine("EndEffect");
    }

    IEnumerator StartEffect()
    {
        while(m_letterCounter <= m_totalLetters)
        {
            m_text.maxVisibleCharacters = m_letterCounter;
            m_letterCounter++;

            yield return new WaitForSeconds(m_effectSpeed);
        }
    }

    IEnumerator EndEffect()
    {
        while(m_letterCounter >= 0)
        {
            m_text.maxVisibleCharacters = m_letterCounter;
            m_letterCounter--;

            yield return new WaitForSeconds(m_effectSpeed);
        }
    }
}
