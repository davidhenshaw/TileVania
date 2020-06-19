using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    int _currFrame = 0;
    [SerializeField] int _bufferLength = 20;
    bool _prevSetState = false;
    bool _prevResetState = false;
    public bool Flag { get; private set; } = false;

    Func<bool> _setCondition;
    Func<bool> _resetCondition;

    private void Update()
    {
        if (_prevResetState == false && _resetCondition() == true)
        {
            Flag = false;
            StopAllCoroutines();
            return;
        }

        if (_prevSetState == false && _setCondition() == true)
        {
            StartCoroutine(StartTimer());
        }
        _prevSetState = _setCondition();
    }

    public void SetCondition(Func<bool> func)
    {
        _setCondition = func;
    }

    public void ResetCondition(Func<bool> func)
    {
        _resetCondition = func;
    }

    public void SetBufferDuration(int value)
    {
        _bufferLength = value;
    }

    IEnumerator StartTimer()
    {
        Flag = true;
        for (int i = 0; i < _bufferLength; i++)
        {
            //Debug.Log($"Frame: {i + 1}");
            _currFrame = i;
            yield return null;
        }
        Flag = false;
    }

}
