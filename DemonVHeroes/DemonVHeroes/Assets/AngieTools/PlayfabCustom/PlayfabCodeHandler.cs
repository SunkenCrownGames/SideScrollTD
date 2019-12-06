/*using AngieTools.PlayfabCustom.Login;
using PlayFab;
using UnityEngine;

namespace AngieTools.PlayfabCustom
{
    public class PlayfabCodeHandler : MonoBehaviour
    {
        public static PlayfabErrorEvent LoginErrorHandler { get; private set; }
        public static PlayfabErrorEvent RegisterErrorHandler { get; private set; }
        
        public PlayfabCodeHandler(PlayfabErrorEvent p_loginErrorHandler)
        {
            LoginErrorHandler = p_loginErrorHandler;
        }

        private void Awake()
        {
            BindInstance();
        }

        private void BindInstance()
        {
            if (LoginErrorHandler == null)
            {
                LoginErrorHandler = new PlayfabErrorEvent();
                LoginErrorHandler.AddListener(LoginHandleError);
            }

            if (RegisterErrorHandler == null)
            {
                RegisterErrorHandler = new PlayfabErrorEvent();
                RegisterErrorHandler.AddListener(RegisterHandleError);
            }
        }


        private static void LoginHandleError(PlayFabErrorCode p_errorCode)
        {
            string error;
            error = "";

            switch (p_errorCode)
            {
                case PlayFabErrorCode.AccountNotFound:
                    error = "Account not Found";
                    break;
                case PlayFabErrorCode.InvalidUsernameOrPassword:
                    error = "Invalid username or password";
                    break;
                case PlayFabErrorCode.InvalidParams:
                    error = "Password must be 6 characters or more";
                    break;
                case PlayFabErrorCode.EmailAddressNotAvailable:
                    error = "Email Already in use";
                    break;
                case PlayFabErrorCode.UsernameNotAvailable:
                    error = "Username already in use";
                    break;
            }
            
            LoginUIController.Instance.SetErrorLabel(error);
        }
        
        private static void RegisterHandleError(PlayFabErrorCode p_errorCode)
        {
            string error;
            error = "";

            switch (p_errorCode)
            {
                case PlayFabErrorCode.AccountNotFound:
                    error = "Account not Found";
                    break;
                case PlayFabErrorCode.InvalidUsernameOrPassword:
                    error = "Invalid username or password";
                    break;
                case PlayFabErrorCode.InvalidParams:
                    error = "Password must be 6 characters or more";
                    break;
                case PlayFabErrorCode.EmailAddressNotAvailable:
                    error = "Email Already in use";
                    break;
                case PlayFabErrorCode.UsernameNotAvailable:
                    error = "Username already in use";
                    break;
            }
            
            RegisterUIController.Instance.SetErrorLabel(error);
        }
    }
}
*/