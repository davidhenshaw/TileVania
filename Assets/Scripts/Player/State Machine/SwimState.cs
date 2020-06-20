using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimState : PlayerMovementState
{

    public SwimState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {
    }

    public override IState CalculateNextState()
    {
        if (!IsSwimming())
            return new UngroundedState(_playerController, _stateMachine);

        return null;
    }

    // Start is called before the first frame update
    public override IEnumerator Enter()
    {
        Debug.Log("Swimming");

        _nextStates.Add(
            () => !IsSwimming(),
            new UngroundedState(_playerController, _stateMachine)
            );

        yield break;
    }

    public override IEnumerator Exit()
    {
        Debug.Log("Stopped Swimming");

        yield break;
    }

    // Update is called once per frame
    public override void Tick()
    {
        base.MoveHorizontal();
        base.HandleJump();

        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        _animator.SetBool(ANIM_BOOL_JUMPING, IsJumping());
        _animator.SetBool(ANIM_BOOL_FALLING, IsFalling());
    }


}
