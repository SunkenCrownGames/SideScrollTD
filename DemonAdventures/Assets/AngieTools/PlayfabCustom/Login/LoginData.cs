namespace AngieTools.Login
{
    /// <summary>
    /// This class contains the data of the user that will be passed on to the login controller
    /// </summary>
    public class LoginData
    {
        private readonly string m_login;
        private readonly string m_password;

        public LoginData(string p_login, string p_password)
        {
            m_login = p_login;
            m_password = p_password;
        }

        public string Login => m_login;

        public string Password => m_password;
    }
}
