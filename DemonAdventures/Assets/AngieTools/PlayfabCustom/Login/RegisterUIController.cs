using System;
using AngieTools.Login;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AngieTools.PlayfabCustom.Login
{
    // ReSharper disable once InconsistentNaming
    public class RegisterUIController : MonoBehaviour
    {
        public static RegisterUIController Instance { get; private set; }

        [SerializeField] private TMP_InputField m_login = null;
        
        [SerializeField] private TMP_InputField m_username = null;

        [SerializeField] private TMP_InputField m_password = null;

        [SerializeField] private TextMeshProUGUI m_errorLabel = null;
        
        private void Awake()
        {
            BindInstance();;   
        }

        private void BindInstance()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void PassData()
        {
            var data = new RegisterData(m_login.text, m_password.text, m_username.text);
            //LoginController.Instance.RegisterDataEvent.Invoke(data);
            m_errorLabel.text = "Registering";
            m_errorLabel.color = Color.white;
        }

        public void SetErrorLabel(string p_errorText)
        {
            m_errorLabel.text = p_errorText;
            m_errorLabel.color = Color.red;
        }

        public void SetSuccessLabel(string p_successLabel)
        {
            m_errorLabel.text = p_successLabel;
            m_errorLabel.color = Color.green; 
        }
    }
}
