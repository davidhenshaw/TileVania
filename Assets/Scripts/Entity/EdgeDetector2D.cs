using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EdgeDetector
{
    public bool Flag { get => currFlag; }
    public event Action FallingEdge;
    public event Action RisingEdge;

    bool prevFlag = false;
    bool currFlag = false;

    Func<bool> conditionFunc;

    public EdgeDetector(Func<bool> func)
    {
        conditionFunc = func;
    }

    public void Poll()
    {
        prevFlag = currFlag;
        currFlag = conditionFunc();
    }

    public void Tick()
    {
        currFlag = conditionFunc();

        if ((prevFlag == true) && (currFlag == false))
            FallingEdge?.Invoke();

        if ((prevFlag == false) && (currFlag == true))
            RisingEdge?.Invoke();

        prevFlag = currFlag;
    }

    public bool IsRisingEdge()
    {
        return ((prevFlag == false) && (currFlag == true));
    }

    public bool IsFallingEdge()
    {
        return ((prevFlag == true) && (currFlag == false));
    }
}

public class EdgeDetector2D : MonoBehaviour
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
