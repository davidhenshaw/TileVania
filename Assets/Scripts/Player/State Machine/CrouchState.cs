using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : PlayerMovementState
{
    public CrouchState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {
    }

    public override IState CalculateNextState()
    {
        if (!IsHoldingCrouch())
            return new GroundedState(_playerController, _stateMachine);

        return null;
    }

    public override IEnumerator Enter()
    {
        _nextStates.Add(
            () => !IsHoldingCrouch(),
            new GroundedState(_playerController,_stateMachine));

        _playerController.HeadCollider.enabled = false;

        _animator.SetBool(ANIM_BOOL_CROUCHING, true);

        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);

        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        _playerController.HeadCollider.enabled = true;
        _animator.SetBool(ANIM_BOOL_CROUCHING, false);
        return base.Exit();
    }

    public override void Tick()
    {
        FlipSprite(Input.GetAxis("Horizontal"));
        base.Tick();
    }

    bool IsHoldingCrouch()
    {
        return (Input.GetAxis("Vertical") < 0);
    }
}
