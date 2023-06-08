using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallSceneLoader : MonoBehaviour
{
    public void LoadScene (string levelName)
    {
        SceneLoader.Instance().LoadSelectedLevel(levelName);
    }

    public void PlayAgain()
    {
        SceneLoader.Instance().PlayGain();
    }

    public void BackToLobby()
    {
        SceneLoader.Instance().BackToLobby();
    }
}
