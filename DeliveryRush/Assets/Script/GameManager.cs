using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }
    

    //spawning waypoint

    void Awake()
    {
        Instance = this;
        InputController = GetComponentInChildren<InputController>();

    }


    public void SwitchSceneAsyncronously(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
