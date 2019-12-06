namespace AngieTools.Login
{
    /// <summary>
    /// This class contains the data of the user that will be passed on to the login controller
    /// </summary>
    public class RegisterData
    {
        public RegisterData(string p_email, string p_password, string p_username)
        {
            Username = p_username;
            Email = p_email;
            Password = p_password;
        }

        public string Username { get; }

        public string Password { get; }

        public string Email { get; }
    }
}
