using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBuffer
{
    float _duration;
    Timer timer;
    bool isFlagRaised;

    public CommandBuffer(float duration)
    {
        _duration = duration;
        timer = new Timer(_duration);
    }

    public void Tick(float deltaTime)
    {
        if(isFlagRaised)
        {
            timer.Tick(deltaTime);
            if(timer.IsDone())
            {
                isFlagRaised = false;
                timer.Reset();
            }
        }
        else
        {
            timer.Reset();
        }
    }

    public void SetFlag(bool value)
    {
        isFlagRaised = value;
    }

    public bool GetFlag()
    {
        return isFlagRaised;
    }
}
