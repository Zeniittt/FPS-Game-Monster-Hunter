using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayer : MonoBehaviour
{
    public string username;
    public string email;
    public int score;

    private void Awake()
    {
        var player = FindObjectsOfType<CurrentPlayer>();
        if(player.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
