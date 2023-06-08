using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneLoaderInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject _sceneLoaderPrefab;

    public static GameObject _staticSceneLoaderPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        _staticSceneLoaderPrefab = _sceneLoaderPrefab;
        SceneLoader sceneLoaderObject = FindObjectOfType<SceneLoader>();
        //GameObject sceneLoaderObject = GameObject.FindGameObjectWithTag("SceneLoader");

        if (sceneLoaderObject == null)
        {
            Debug.Log("Create new object");
            SceneLoader.Instance();
        }
        else
        {
            Debug.Log("Use object existed");
        }
    }

}
