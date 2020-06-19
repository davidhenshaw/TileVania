using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UngroundedState : PlayerMovementState
{
    public UngroundedState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {
    }

    public override IEnumerator Exit()
    {
        _animator.SetBool(ANIM_BOOL_JUMPING, false);
        _animator.SetBool(ANIM_BOOL_FALLING, false);
        return base.Exit();
    }

    public override IEnumerator Enter()
    {
        _nextStates.Add(
            () => IsGrounded(),
            new GroundedState(_playerEntity, _stateMachine));

        _nextStates.Add(
            () => CanClimb() && ClimbRequested(),
            new ClimbState(_playerEntity, _stateMachine));

        UpdateAnimator();
        return base.Enter();
    }

    public override void Tick()
    {
        ProcessJumpRequest();
        UpdateAnimator();
        base.MoveHorizontal();
        base.HandleJump();
    }

    void UpdateAnimator()
    {
        _animator.SetBool(ANIM_BOOL_JUMPING, IsJumping());
        _animator.SetBool(ANIM_BOOL_FALLING, IsFalling());
    }

    void ProcessJumpRequest()
    {
        if(Input.GetButtonDown("Jump"))
            _playerEntity.UngroundedJumpBuffer.SetFlag(true);
    }

    protected bool ClimbRequested()
    {
        float yAxis = Input.GetAxis("Vertical");

        return (Mathf.Abs(yAxis) > Mathf.Epsilon);
    }
}
