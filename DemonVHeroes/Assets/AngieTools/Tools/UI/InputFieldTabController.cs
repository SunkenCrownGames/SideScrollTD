using UnityEngine;
using TMPro;

namespace AngieTools.Tools.UI
{
    public class InputFieldTabController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_input = null;
        [SerializeField] private InputFieldTabController m_nextField = null;


        public TMP_InputField InputField => m_input;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            CheckForTab();
        }

        private void CheckForTab()
        {
            if (!m_input.isFocused) return;

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(m_nextField == null) return;

                m_nextField.m_input.Select();
                m_nextField.m_input.ActivateInputField();
            }
        }
    }
}
