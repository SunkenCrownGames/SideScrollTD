using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Player.UI;
using Player.UI.PlayerInfo;
using Sirenix.OdinInspector;
using Spawners;
using Spawners.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [SerializeField] [Required] private UiManager m_uiManager;
    [SerializeField] [Required] private PlayerManager m_playerManager;
    [SerializeField] private GameState m_state;
    
    private void Awake()
    {
        BindInstance();
    }

    // Start is called before the first frame update
    private void Start()
    {
        UpdateGameState(m_state);
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BindInstance()
    {
        if(Instance != null) Destroy(gameObject);

        Instance = this;
    }

    private void InitializeGame()
    {
        SpawnerUiController.Instance.UpdateSelection();
    }

    [Button("Toggle State")]
    public void UpdateGameState(GameState p_state)
    {
        m_state = p_state;
        m_uiManager.ToggleUi(p_state);

        if (p_state == GameState.Play)
        {
            SpawnerManager.Instance.SpawnSoldiers();
        }
    }


    public UiManager UiManager => m_uiManager;
    public PlayerManager Player => m_playerManager;
    public static GameManager Instance { get; private set; }
    
    public enum GameState
    {
        Build,
        Play
    }
}
