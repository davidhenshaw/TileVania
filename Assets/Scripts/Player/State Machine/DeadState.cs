using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerMovementState
{
    public DeadState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {

    }

    public override IState CalculateNextState()
    {
        return null;
    }

    public override IEnumerator Exit()
    {
        _playerController.Animator.SetBool(ANIM_BOOL_HURTING, false);
        _playerController.GroundCollider.enabled = true;
        _playerController.HeadCollider.enabled = true;
        _playerController.BodyCollider.enabled = true;
        return base.Exit();
    }

    public override IEnumerator Enter()
    {
        float pushForce = _playerController.PlayerControllerSettings.JumpVelocity;
        Rigidbody2D rb = _playerController.RigidBody;

        _playerController.Animator.SetBool(ANIM_BOOL_HURTING, true);
        _playerController.Animator.SetTrigger(ANIM_TRIGGER_DIE);

        //Make the player do a little jump
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * pushForce , ForceMode2D.Impulse);

        //Disable colliders so player falls through the map
        _playerController.GroundCollider.enabled = false;
        _playerController.HeadCollider.enabled = false;
        _playerController.BodyCollider.enabled = false;

        return base.Enter();
    }

    public override void Tick()
    {
        base.Tick();
    }
}
