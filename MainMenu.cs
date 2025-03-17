using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject inputBlocker;
    public GameObject Cube;
    //public ReadCube readCube;
    //public CubeState cubeState;
    //public CubeMap cubeMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //cubeState = FindObjectOfType<CubeState>();
        //readCube = FindObjectOfType<ReadCube>();
        //cubeMap = FindObjectOfType<CubeMap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Rubik's cube");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
    public void OpenMenu()
    {
        inputBlocker.SetActive(true);
        //readCube.enabled = false;
        //cubeState.enabled = false;
        //cubeMap.enabled = false;
        Cube.SetActive(false);
    }

    public void CloseMenu()
    {
        inputBlocker.SetActive(false);
        //readCube.enabled = true;
        //cubeState.enabled = true;
        //cubeMap.enabled = true;
        Cube.SetActive(true);
    }
}
