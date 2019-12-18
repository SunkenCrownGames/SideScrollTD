using System;
using AngieTools.Login;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AngieTools.PlayfabCustom.Login
{
    // ReSharper disable once InconsistentNaming
    public class LoginUIController : MonoBehaviour
    {
        public static LoginUIController Instance { get; private set; }

        [SerializeField] private TMP_InputField m_login = null;

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
            var data = new LoginData(m_login.text, m_password.text);
            //LoginController.Instance.LoginDataEvent.Invoke(data);
            m_errorLabel.text = "Logging in";
            m_errorLabel.color = Color.white;
        }

        public void SetErrorLabel(string p_errorText)
        {
            m_errorLabel.text = p_errorText;
            m_errorLabel.color = Color.red;
        }
        
        public void SSetSuccessLabel(string p_errorText)
        {
            m_errorLabel.text = p_errorText;
            m_errorLabel.color = Color.green;
        }
        
    }
}
