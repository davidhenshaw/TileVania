using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OneWayState : PlayerMovementState
{
    new protected Dictionary<Func<bool>, PlayerMovementState> _nextStates;

    public OneWayState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {
    }

    public override IEnumerator Enter()
    {
        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        return base.Exit();
    }

    public override void Tick()
    {
        base.Tick();
    }

}
