using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }

    private const string SCENE_PATH = "Assets/Scenes/";
    public const string INTRO_1 = "Intro-1";
    public const string INTRO_2 = "Intro-2";
    public const string MAIN_MENU = "Main Menu";
    public const string TUTORIAL = "Tutorial";

    public static event Action sceneFinishedLoading;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        SceneManager.sceneLoaded += sceneLoaded;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if (SceneManager.sceneCount < 2)
                LoadGameUIAsync();
            else
                UnloadGameUIAsync();
        }
    }

    private void sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneFinishedLoading?.Invoke();
    }

    public static void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadFromName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadSubsceneFromSuffix(char suffix)
    {
        //Get current scene name
        string sceneName = SceneManager.GetActiveScene().name;
        //Truncate last letter
        sceneName = sceneName.Substring(0, sceneName.Length - 1);
        //Append new suffix to base Scene name
        string newSceneName = sceneName + suffix;

        Debug.Log("Original Scene: " + sceneName);
        Debug.Log("New Scene: " + newSceneName);

        SceneManager.LoadScene(newSceneName);
    }

    public static void LoadGameUIAsync()
    {
        SceneManager.LoadSceneAsync("Game UI", LoadSceneMode.Additive);
    }

    public static void UnloadGameUIAsync()
    {
        SceneManager.UnloadSceneAsync("Game UI");
    }
}
