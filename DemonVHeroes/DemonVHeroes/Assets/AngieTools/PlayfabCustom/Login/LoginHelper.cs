/*using AngieTools.Login;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

// ReSharper disable once ClassNeverInstantiated.Global
namespace AngieTools.PlayfabCustom.Login
{
    public class LoginHelper
    {
        
        private string _playFabPlayerIdCache;
        
        public static void RegisterSuccess(RegisterPlayFabUserResult p_result)
        {
            Debug.Log("Congratulations Account Registered");
            RegisterUIController.Instance.SetSuccessLabel("Account Successfully Created!");
        }

        public static void RegisterFailure(PlayFabError p_error)
        {
            PlayfabCodeHandler.RegisterErrorHandler.Invoke((p_error.Error));
        }

        /// <summary>
        /// If the Login Succeeds Continue with the login process 
        /// </summary>
        /// <param name="p_result">Success Object</param>
        public static void LoginSuccess(LoginResult p_result)
        {
            Debug.Log("Congratulations, you are now logged in");
            LoginUIController.Instance.SSetSuccessLabel("Account Successfully Logged In!");
        }

        /// <summary>
        /// If the login fails grab the error code and handle the error
        /// </summary>
        /// <param name="p_error">Error Object</param>
        public static void LoginFailure(PlayFabError p_error)
        {
            if(p_error.Error == PlayFabErrorCode.AccountNotFound 
               || p_error.Error == PlayFabErrorCode.InvalidUsernameOrPassword 
               || p_error.Error == PlayFabErrorCode.InvalidParams)
            {
                PlayfabCodeHandler.LoginErrorHandler.Invoke((p_error.Error));
            }
        }
    }
}
*/