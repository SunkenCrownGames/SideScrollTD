using System;
using System.Collections;
using System.Collections.Generic;
using Player.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] [Required] private SetUpUi m_setupUi;

    [SerializeField] private GameObject m_gameUi;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void ToggleUi(GameManager.GameState p_state)
    {
        switch (p_state)
        {
            case GameManager.GameState.Build:
                if(m_setupUi != null)
                    m_setupUi.gameObject.SetActive(true);
                
                if(m_gameUi != null)
                    m_gameUi.gameObject.SetActive(false);
                break;
            case GameManager.GameState.Play:
                if(m_setupUi != null)
                    m_setupUi.gameObject.SetActive(false);
                
                if(m_gameUi != null)
                    m_gameUi.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(p_state), p_state, null);
        }
    }

    public SetUpUi SetupUi => m_setupUi;

    public GameObject GameUi => m_gameUi;
}
