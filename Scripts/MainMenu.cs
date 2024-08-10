using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void COOP()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void TEST()
    {
        SceneManager.LoadSceneAsync(2);

    }

    public void INGAME()
    {
        SceneManager.LoadSceneAsync(0);

    }

    public void EXITGAME()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        
    }

    public void CONTROLS()
    {
        SceneManager.LoadSceneAsync(3);

    }
}
