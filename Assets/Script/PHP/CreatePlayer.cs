using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class CreatePlayer : MonoBehaviour
{
    public TMP_InputField _usernameInput, _emailInput, _passwordInput;

    public Button _registerButton;
    public TMP_Text _registerButtonText;

    public GameObject _SignIn;

    public void RegisterNewPlayer()
    {
        _registerButton.interactable = false;
        if(_usernameInput.text.Length <5)
        {
            ErrorMessage("Username is too short!");
        } else if(_emailInput.text.Length < 5)
        {
            ErrorMessage("Email is too short!");
        }else if(_passwordInput.text.Length < 5)
        {
            ErrorMessage("Password is too short!");
        }
        else
        {
            Debug.Log("Good to go");
            SetButtonToSending();
            StartCoroutine(CreatePlayerPostRequest());
        }
    }

    public void ErrorMessage(string message)
    {
        _registerButton.GetComponent<Image>().color = Color.red;
        _registerButtonText.text = message;
        _registerButtonText.fontSize = 40f;
    }

    public void ResetRegisterButton()
    {
        _registerButton.GetComponent<Image>().color = Color.white;
        _registerButtonText.text = "Register";
        _registerButtonText.fontSize = 109.4f;
        _registerButton.interactable = true;
    }

    public void SetButtonToSending()
    {
        _registerButton.GetComponent<Image>().color = Color.grey;
        _registerButtonText.text = "Sending...";
        _registerButtonText.fontSize = 109.4f;
    }

    public void SetButtonToSuccess()
    {
        _registerButton.GetComponent<Image>().color = Color.green;
        _registerButtonText.text = "Sign up successfully";
        _registerButtonText.fontSize = 69.4f;
    }

    IEnumerator CreatePlayerPostRequest()
    {
        WWWForm newPlayerInfo = new WWWForm();
        newPlayerInfo.AddField("username", _usernameInput.text);
        newPlayerInfo.AddField("email", _emailInput.text);
        newPlayerInfo.AddField("password", _passwordInput.text);
        UnityWebRequest createPostRequest = UnityWebRequest.Post("http://localhost/cruds/newplayer.php", newPlayerInfo);
        yield return createPostRequest.SendWebRequest();
        if(createPostRequest.error == null)
        {
            Debug.Log(createPostRequest.downloadHandler.text);
            string response = createPostRequest.downloadHandler.text;
            if(response == "1" || response == "2" || response == "4" || response == "6")
            {
                ErrorMessage("Server Error");
            }else if( response == "3")
            {
                ErrorMessage("Username existed!");
            }else if( response == "5")
            {
                ErrorMessage("Email Already Exists");
            }
            else
            {
                SetButtonToSuccess();
                yield return new WaitForSeconds(2f);
                GameObject.Find("SignUp").GetComponent<Transform>().gameObject.SetActive(false);
                _SignIn.SetActive(true);
            }
        }
        else
        {
            Debug.Log(createPostRequest.error);
        }

    }
}
