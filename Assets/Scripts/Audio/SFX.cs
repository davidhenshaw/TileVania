using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX : MonoBehaviour
{
    AudioSource _audioSource;
    Camera _camera;
    public static SFX instance { get; private set; }

    private void Awake()
    {
        // Set up singleton pattern
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        _camera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneLoader.sceneFinishedLoading += BindMainCamera;
    }

    private void Update()
    {
        //Always follow the main camera
        transform.position = _camera.transform.position;
    }

    private void BindMainCamera()
    {
        _camera = Camera.main;
    }

    public void Play(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void Play(AudioClip clip, float volume)
    {
        _audioSource.PlayOneShot(clip, volume);
    }
}
