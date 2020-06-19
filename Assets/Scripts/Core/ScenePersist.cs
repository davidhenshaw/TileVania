using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    public static ScenePersist instance;
    public int startingSceneIndex { get; private set; }

    private void Awake()
    {
        CheckForNewInstance();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if(newScene.buildIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }

    // Deletes new instances only if they are created in the same scene
    // New scene's instance will be kept alive and the old scene's instance destroyed
    void CheckForNewInstance()
    {
        this.startingSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance.startingSceneIndex == this.startingSceneIndex)
            {
                // Destroy new instance if the scene did not change
                Destroy(gameObject);
            }
            else
            { // Destroy old instance if the scene did change
                Destroy(instance.gameObject);
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }

    
}
