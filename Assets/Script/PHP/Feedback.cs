using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    public TMP_InputField _emailInput, _feedbackInput;

    public Button _registerButton;
    public TMP_Text _registerButtonText;


    public void SendFeedback()
    {
        _registerButton.interactable = false;
        if (_emailInput.text.Length < 5)
        {
            ErrorMessage("Email is too short!");
        }
        else if (_feedbackInput.text.Length < 5)
        {
            ErrorMessage("Feedback is too short!");
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

    public void ResetFeedbackButton()
    {
        _registerButton.GetComponent<Image>().color = Color.white;
        _registerButtonText.text = "Submit";
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
        _registerButtonText.text = "Feedback successful";
        _registerButtonText.fontSize = 69.4f;
    }

    IEnumerator CreatePlayerPostRequest()
    {
        WWWForm newPlayerInfo = new WWWForm();
        newPlayerInfo.AddField("email", _emailInput.text);
        newPlayerInfo.AddField("feedback", _feedbackInput.text);
        UnityWebRequest createPostRequest = UnityWebRequest.Post("http://localhost/feedback/addFeedBack.php", newPlayerInfo);
        yield return createPostRequest.SendWebRequest();
        if (createPostRequest.error == null)
        {
            Debug.Log(createPostRequest.downloadHandler.text);
            string response = createPostRequest.downloadHandler.text;
            if (response == "1" || response == "2" || response == "4")
            {
                ErrorMessage("Server Error");
            }
            else if (response == "3")
            {
                ErrorMessage("Email Already Exists");
            }
            else
            {
                SetButtonToSuccess();
                yield return new WaitForSeconds(2f);
            }
        }
        else
        {
            Debug.Log(createPostRequest.error);
        }

    }
}
