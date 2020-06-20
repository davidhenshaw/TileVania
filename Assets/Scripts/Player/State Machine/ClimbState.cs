using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : PlayerMovementState
{
    [SerializeField] float _climbSpeed = 4f;
    bool _jumpBeforeExiting = false;

    public ClimbState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {
        _rigidBody = playerEntity.RigidBody;
        _animator = playerEntity.Animator;
        _groundCollider = playerEntity.GroundCollider;
    }

    public override IState CalculateNextState()
    {
        if (!CanClimb())
            return new GroundedState(_playerController, _stateMachine);
        if (JumpRequested())
            return new UngroundedState(_playerController, _stateMachine);

        return null;
    }

    public override IEnumerator Enter()
    {
        _playerController.Animator.SetTrigger(ANIM_TRIGGER_STARTCLIMB);
        _rigidBody.gravityScale = 0;
        yield break;
    }

    public override IEnumerator Exit()
    {
        _playerController.Animator.SetTrigger(ANIM_TRIGGER_ENDCLIMB);
        _playerController.Animator.ResetTrigger(ANIM_TRIGGER_ENDCLIMB);

        _animator.SetBool(ANIM_BOOL_CLIMBING, false);
        _rigidBody.gravityScale = 1;

        if (_jumpBeforeExiting)
            Jump();

        yield break;
    }

    public override void Tick()
    {
        MoveVertical();
        base.MoveHorizontal();
        UpdateAnimator();
    }

    //void Jump()
    //{
    //    _rigidBody.velocity += Vector2.up * _playerSettings.JumpVelocity;
    //}

    bool JumpRequested()
    {
        if (_input.JumpPressed)
            _jumpBeforeExiting = true;

        return _jumpBeforeExiting;
    }

    void MoveVertical()
    {
        float yInput = _input.Vertical;

        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _climbSpeed * yInput);
    }

    void UpdateAnimator()
    {
        float yInput = _input.Vertical;
        bool isMoving = Mathf.Abs(yInput) > Mathf.Epsilon;

        _animator.SetBool(ANIM_BOOL_CLIMBING, isMoving);
    }

}
