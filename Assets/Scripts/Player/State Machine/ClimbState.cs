using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : PlayerMovementState
{
    [SerializeField] float _climbSpeed = 4f;
    bool _jumpCancel = false;

    public ClimbState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {
        _rigidBody = playerEntity.RigidBody;
        _animator = playerEntity.Animator;
        _groundCollider = playerEntity.GroundCollider;
    }

    public override IEnumerator Enter()
    {
        _nextStates.Add(
            () => !CanClimb(),
            new GroundedState(_playerController, _stateMachine));

        _nextStates.Add(
            () => JumpRequested(),
            new UngroundedState(_playerController, _stateMachine));


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

        if (_jumpCancel)
            JumpOff();

        yield break;
    }

    public override void Tick()
    {
        MoveVertical();
        base.MoveHorizontal();
        UpdateAnimator();
    }

    void JumpOff()
    {
        _rigidBody.velocity += Vector2.up * _playerSettings.JumpVelocity;
    }

    bool JumpRequested()
    {
        if (_input.JumpPressed)
            _jumpCancel = true;

        return _jumpCancel;
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
