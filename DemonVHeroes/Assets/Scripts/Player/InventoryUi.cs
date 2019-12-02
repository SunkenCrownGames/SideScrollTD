using System.Collections;
using System.Collections.Generic;
using AngieTools.UI;
using Turrets;
using UnityEngine;

public class InventoryUi : UIMonoBehaviour
{
    [SerializeField] private Sprite m_lockedSprite;
    [SerializeField] private List<TurretSlotUiController> m_turretSlotUiControllers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSlots(List<TurretData> p_currentTurrets)
    {
        for (var i = 0; i < m_turretSlotUiControllers.Count; i++)
        {
            if (i < p_currentTurrets.Count)
            {
               m_turretSlotUiControllers[i].UpdateData(p_currentTurrets[i]); 
            }
            else
            {
                m_turretSlotUiControllers[i].ResetData(m_lockedSprite);
            }
        }
    }

    public void DisableSlot(int p_slotId)
    {
        m_turretSlotUiControllers[p_slotId].ResetData(m_lockedSprite);
    }

    public void UpdateSlot(int p_slotId, TurretData p_turretData)
    {
        m_turretSlotUiControllers[p_slotId].UpdateData(p_turretData);
    }

    public List<TurretSlotUiController> TurretSlotUiControllers => m_turretSlotUiControllers;
}
