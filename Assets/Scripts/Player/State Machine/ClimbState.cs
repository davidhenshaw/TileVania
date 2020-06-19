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
            new GroundedState(_playerEntity, _stateMachine));

        _nextStates.Add(
            () => JumpRequested(),
            new UngroundedState(_playerEntity, _stateMachine));


        _playerEntity.Animator.SetTrigger(ANIM_TRIGGER_STARTCLIMB);
        _rigidBody.gravityScale = 0;
        yield break;
    }

    public override IEnumerator Exit()
    {
        _playerEntity.Animator.SetTrigger(ANIM_TRIGGER_ENDCLIMB);
        _playerEntity.Animator.ResetTrigger(ANIM_TRIGGER_ENDCLIMB);

        _animator.SetBool(ANIM_BOOL_CLIMBING, false);
        _rigidBody.gravityScale = 1;

        if (_jumpCancel)
            JumpOff();

        yield break;
    }

    public override void Tick()
    {
        MoveVertical();
        MoveHorizontal();
    }

    void JumpOff()
    {
        _rigidBody.velocity += Vector2.up * _playerSettings.JumpVelocity;
    }

    bool JumpRequested()
    {
        if (Input.GetButtonDown("Jump"))
            _jumpCancel = true;

        return _jumpCancel;
    }

    void MoveVertical()
    {
        float yInput = Input.GetAxis("Vertical");

        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _climbSpeed * yInput);

        bool isMoving = Mathf.Abs(yInput) > Mathf.Epsilon;

        _animator.SetBool(ANIM_BOOL_CLIMBING, isMoving);
    }

}
