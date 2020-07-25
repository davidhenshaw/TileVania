using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    const string OPEN_SOUND = "DOOR_OPEN";
    public static event Action<Door> Opened;

    bool _wasOpened;        //Prevents spamming open button
    [SerializeField] AudioClip _openSound;
    [Tooltip("Scene that will be loaded when player interacts with it. " +
        "An empty string will default to loading the next scene in the build index.")]
    [SerializeField] string _destinationScene;

    public void Interact()
    {
        if (!_wasOpened)
        {
            Opened?.Invoke(this);
            _wasOpened = true;
            SFX.instance.Play(_openSound);
        }
    }

    public string GetDestinationScene()
    {
        return _destinationScene;
    }


}
