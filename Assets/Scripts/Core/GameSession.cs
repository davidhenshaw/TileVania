using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    float _pauseAfterDeath = 3f;
    [SerializeField] Transition _transition;
    [SerializeField] float _minTransitionTime;
    PlayerInventory _playerInventory = new PlayerInventory() { cherries = 0, lives = 3 };

    public static event Action<PlayerInventory> InventoryChanged;

    private void Awake()
    {// Set up singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SceneLoader.LoadGameUIAsync();
        Collectable.ItemCollected += OnCollectiblePickedUp;
        Door.Opened += OnDoorOpen;
        PlayerController.PlayerDied += OnPlayerDeath;
        SceneLoader.sceneFinishedLoading += EndSceneTransition;
        SceneManager.activeSceneChanged += OnSceneChanged;

        InventoryUpdateType[] t = { InventoryUpdateType.ALL };
        InventoryChanged?.Invoke(_playerInventory);
    }

    private void OnDestroy()
    {
        Collectable.ItemCollected -= OnCollectiblePickedUp;
        Door.Opened -= OnDoorOpen;
        PlayerController.PlayerDied -= OnPlayerDeath;
        SceneLoader.sceneFinishedLoading -= EndSceneTransition;
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if(newScene.name.Equals("Main Menu"))
        {
            Destroy(gameObject);
        }
        else
        { SceneLoader.LoadGameUIAsync(); }
    }

    public void SetInventory(PlayerInventory inventory)
    {
        _playerInventory = inventory;
    }

    public PlayerInventory GetInventory()
    {
        return _playerInventory;
    }

    private void OnDoorOpen(Door door)
    {
        string sceneName = door.GetDestinationScene();

        StartCoroutine(Co_LoadNextScene(sceneName));
    }

    private void OnPlayerDeath(Vector2 position)
    {
        if (_playerInventory.lives > 1)
        {
            _playerInventory.lives--;
            InventoryChanged?.Invoke(_playerInventory);
            StartCoroutine(Co_ResetScene());
        }
        else
        {
            StartCoroutine(Co_ProcessGameOver());
        }
    }

    private void OnCollectiblePickedUp(Collectable item)
    {
        item.UpdateInventory(_playerInventory);
        InventoryChanged?.Invoke(_playerInventory);
    }


    private IEnumerator Co_ProcessGameOver()
    {
        yield return new WaitForSeconds(_pauseAfterDeath);
        SceneLoader.LoadFromName("Main Menu");
    }

    private IEnumerator Co_ResetScene()
    {
        yield return new WaitForSeconds(_pauseAfterDeath);
        ResetScene();
    }

    private void ResetScene()
    {
        SceneLoader.ReloadCurrentScene();
    }

    private IEnumerator Co_LoadNextScene(string sceneName)
    {
        _transition.PlayBeginning();

        yield return new WaitForSeconds(_minTransitionTime);

        if (sceneName.Equals(""))
            SceneLoader.LoadNextScene();
        else
            SceneLoader.LoadFromName(sceneName);
    }

    private void EndSceneTransition()
    {
        _transition.PlayEnd();
    }

}
