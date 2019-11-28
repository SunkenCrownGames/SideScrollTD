using System;
using System.Collections;
using System.Collections.Generic;
using Env;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnvManager : MonoBehaviour
{

    [SerializeField] private EdgeController m_edge;
    
    private static EnvManager _instance;
    private static bool _midCollapsed;

    private void Awake()
    {
        BindInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BindInstance()
    {
        if(_instance != null) Destroy(gameObject);

        _instance = this;
    }

    [Button("Collapse Bridge")]
    public static void CollapseMiddle()
    {
        _instance.m_edge.CollapseEdgeDestroy();
        _midCollapsed = true;
    }


    public static EnvManager Instance => _instance;

    public static bool MidCollapsed => _midCollapsed;
}
