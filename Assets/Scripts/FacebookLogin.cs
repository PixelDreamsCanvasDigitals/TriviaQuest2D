using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Android;

public class FacebookLogin : MonoBehaviour
{

    public TextMeshProUGUI FB_UserName;
    public TextMeshProUGUI FB_UserID;
    public Image FB_UserDP;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if(FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the facebook SDK");
        }
    }

    public void Login()
    {
        if(!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> { "public_profile", "email" }, LoginCallBack);
        }
        else
        {
            Debug.Log("Already logged in to facebook");
        }
    }

    public void LogOut()
    {
        if (FB.IsLoggedIn)
        {
            FB.LogOut();
            ResetUserData();
        }
        else
        {
            Debug.Log("Not logged in to Facebook");
        }
    }

    private void ResetUserData()
    {
        FB_UserName.text = "New Text";
        FB_UserID.text = "New Text";
        FB_UserDP.sprite = null;
    }

    private void LoginCallBack(ILoginResult result)
    {
        if(result.Cancelled)
        {
            Debug.Log("facebook login cancelled");
        }
        else if(!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("facebook login error : " + result.Error);
        }
        else if(FB.IsLoggedIn)
        {
            Debug.Log("Facebook login successful");

            //Retrieve user data
            FB.API("/me/fields=id,first_name,last_name,email", HttpMethod.GET, UserDataCallback);
        }
    }

    private void UserDataCallback(IGraphResult result)
    {
        if(result.Error != null)
        {
            Debug.Log("Error retrieving user data: " + result.Error);
        }
        else
        {
            var userData = result.ResultDictionary;
            string userID = userData["id"].ToString();
            string firstName = userData["first_name"].ToString();
            FB_UserName.text = firstName;
            FB_UserID.text = userID;
            FB.API("/me/picture?redirect=false&type=large", HttpMethod.GET, ProfilePictureCallback);
        }
    }

    private void ProfilePictureCallback(IGraphResult result)
    {
        if(result.Error != null)
        {
            Debug.Log("Error retrieving profile picture: " + result.Error);
        }
        else
        {
            var pictureData = result.ResultDictionary["data"] as Dictionary<string, object>;
            string pictureURL = pictureData["url"].ToString();

            Debug.Log("Profile Picture URL: " + pictureURL);
            StartCoroutine(FetchProfilePicture(pictureURL));
        }
    }

    private IEnumerator FetchProfilePicture(string pictureURL)
    {
        UnityWebRequest www= UnityWebRequestTexture.GetTexture(pictureURL);
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            FB_UserDP.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            Debug.Log("Error fetching profile picture: " + www.error);
        }
    }
}
