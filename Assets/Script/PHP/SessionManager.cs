using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    CurrentPlayer _currentPlayer = null;
    [SerializeField]
    GameObject _signOutBtn;
    SceneLoader _sceneLoader;
    void Start()
    {
        _sceneLoader = SceneLoader.Instance();

        _currentPlayer = FindObjectOfType<CurrentPlayer>();

        if (_currentPlayer == null)
        {
            _signOutBtn.SetActive(false);
        }
        else
        {
            _signOutBtn.SetActive(true);
        }
    }

    public void SignOut()
    {
        GameObject player = GameObject.FindGameObjectWithTag("CurrentPlayer");
        Destroy(player);
        _sceneLoader.LoadSelectedLevel("MainMenu");
    }

    public void MultiPlayer()
    {
        if(_currentPlayer == null)
        {
            _sceneLoader.LoadSelectedLevel("WelcomeScene");
        }
        else
        {
            _sceneLoader.LoadSelectedLevel("MainMenu");
        }
    }
}
