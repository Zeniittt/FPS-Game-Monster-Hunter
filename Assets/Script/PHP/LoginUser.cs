using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LoginUser : MonoBehaviour
{
    public TMP_InputField _usernameInput;
    public TMP_InputField _passwordInput;

    public Button _loginButton;
    public TMP_Text _loginButonText;

    public GameObject _currentPlayerObject;

    private void Awake()
    {
        //clean all session
        GameObject player = GameObject.FindGameObjectWithTag("CurrentPlayer");
        Destroy(player);
    }

    public void Login()
    {
        _loginButton.interactable = false;
        _loginButonText.text = "Sending...";
        if (_usernameInput.text.Length < 3)
        {
            ErrorOnLogInMessage("Check Username!");
        }
        else if (_passwordInput.text.Length < 3)
        {
            ErrorOnLogInMessage("Check Password!");
        }
        else
        {
            StartCoroutine(SendLoginForm());
        }
    }

    public void ErrorOnLogInMessage(string message)
    {
        _loginButton.GetComponent<Image>().color = Color.red;
        _loginButonText.text = message;
        _loginButonText.fontSize = 60;
    }

    public void ResetLogInButton()
    {
        _loginButton.GetComponent<Image>().color = Color.white;
        _loginButonText.text = "Login";
        _loginButonText.fontSize = 70;
        _loginButton.interactable = true;
    }

    IEnumerator SendLoginForm()
    {
        WWWForm loginInfo = new WWWForm();
        loginInfo.AddField("username", _usernameInput.text);
        loginInfo.AddField("password", _passwordInput.text);
        UnityWebRequest loginRequest = UnityWebRequest.Post("http://localhost/cruds/loginUser.php", loginInfo);
        yield return loginRequest.SendWebRequest();

        if (loginRequest.error == null)
        {
            //1,2,5: Server Error
            // 3: username wrong
            // 4: password wrong
            string result = loginRequest.downloadHandler.text;
            Debug.Log("result: " + result);
            if (result == "1" || result == "2" || result == "5")
            {
                ErrorOnLogInMessage("Server Error");
            }
            else if (result == "3")
            {
                _passwordInput.text = "";
                _usernameInput.text = "";
                ErrorOnLogInMessage("Wrong username or password");
            }
            else if (result == "4")
            {
                _passwordInput.text = "";
                _usernameInput.text = "";
                ErrorOnLogInMessage("Wrong username or password");
            }
            else
            {
                //Instantia and get player info
                var currentPlayer = Instantiate(_currentPlayerObject, new Vector3(0, 0, 0), Quaternion.identity);
                currentPlayer.GetComponent<CurrentPlayer>().username = result.Split(':')[0];
                currentPlayer.GetComponent<CurrentPlayer>().score = int.Parse(result.Split(':')[1]);

                //Ultilize Button
                _loginButton.GetComponent<Image>().color = Color.green;
                _loginButonText.text = "Logged In";

                //Redirect to new scene
                FindObjectOfType<SceneLoader>().LoadSelectedLevel("MultiplayerScene");
            }

        }
        else
        {
            Debug.Log(loginRequest.error);
        }
    }
}
