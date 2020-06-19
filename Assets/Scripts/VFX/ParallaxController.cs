using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    private Transform _cameraTransform;

    [Range(0, 1)]
    [Tooltip("Values closer to zero make the image look closer to the foreground")]
    [SerializeField]
    float _parallaxStrength;

    Vector3 myStartingPos;
    Vector3 camStartingPos;

    private void Awake()
    {
        if(_camera == null)
        {
            _camera = Camera.main;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = _camera.transform;
        myStartingPos = transform.position;
        camStartingPos = _cameraTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 distance = (_cameraTransform.position - camStartingPos);

        distance = distance * _parallaxStrength;

        transform.position = myStartingPos + new Vector3(distance.x, distance.y, myStartingPos.z);
    }
}
