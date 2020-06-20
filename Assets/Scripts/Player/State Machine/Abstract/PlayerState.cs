using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState
{
    // Animator parameters
    public const string ANIM_BOOL_RUNNING = "isRunning";
    public const string ANIM_BOOL_CROUCHING = "isCrouching";
    public const string ANIM_BOOL_JUMPING = "isJumping";
    public const string ANIM_BOOL_FALLING = "isFalling";
    public const string ANIM_BOOL_CLIMBING = "isClimbing";
    public const string ANIM_BOOL_HURTING = "isHurt";
    public const string ANIM_TRIGGER_DIE = "die";
    public const string ANIM_TRIGGER_STARTCLIMB = "startClimb";
    public const string ANIM_TRIGGER_ENDCLIMB = "endClimb";

    protected Dictionary<Func<bool>, IState> _nextStates;
    protected IStateMachine _stateMachine;

    public abstract IState CalculateNextState();

    protected PlayerState(IStateMachine stateMachine)
    {
        _nextStates = new Dictionary<Func<bool>, IState>();
        _stateMachine = stateMachine;
    }

    public virtual IEnumerator Enter()
    {
        yield break;
    }

    // Update is called once per frame
    public virtual void Tick()
    {

    }

    public virtual IEnumerator Exit()
    {
        yield break;
    }

    public void CheckNextStates()
    {
        foreach (Func<bool> condition in _nextStates.Keys)
        {
            if (condition())
            {
                _stateMachine.SetState(_nextStates[condition]);
                break;
            }

        }
    }


}
