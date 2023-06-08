using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Animator _Scenetransition = null;
    public float _transitionTime = 1f;

    private static SceneLoader _instance;

    public static SceneLoader Instance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<SceneLoader>();
            //_instance = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();

            if (_instance == null)
            {
                GameObject singletonObject = Instantiate(SceneLoaderInitializer._staticSceneLoaderPrefab);
                _instance = singletonObject.GetComponent<SceneLoader>();
            }
        }
        DontDestroyOnLoad(_instance);
        return _instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby" && Input.GetKeyDown(KeyCode.Escape))
        {
            LoadSelectedLevel("MainMenu");
        }
    }

    public void PlayGain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToLobby()
    {
        LoadSelectedLevel("Lobby");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        _Scenetransition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(_transitionTime);
        //loadscene
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadSelectLevel(string levelName)
    {
        //play animation
        _Scenetransition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(_transitionTime);
        //loadscene
        SceneManager.LoadScene(levelName);
    }

    public void LoadSelectedLevel(string levelName)
    {
        StartCoroutine(LoadSelectLevel(levelName));
    }

    public void LoadInventory()
    {
        StartCoroutine(LoadSelectLevel("Shop"));
        DontDestroyOnLoad(gameObject);
        Invoke("ultil", 1.75f);
    }

    private void ultil()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("OpenInventory");
        Destroy(gameObject);
        //GetComponent<Animator>().SetTrigger("");
    }
}
