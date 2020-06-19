using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    public bool Flag { get => currFlag; }
    public event Action FallingEdge;
    public event Action RisingEdge;

    bool prevFlag;
    bool currFlag;

    Collider2D _detector;
    [SerializeField] LayerMask _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _detector = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currFlag = _detector.IsTouchingLayers(_layerMask);

        if ((prevFlag == true) && (currFlag == false))
            FallingEdge?.Invoke();

        if ((prevFlag == false) && (currFlag == true))
            RisingEdge?.Invoke();

        prevFlag = currFlag;
    }
}
