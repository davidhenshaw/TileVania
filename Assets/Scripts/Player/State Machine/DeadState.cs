using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerMovementState
{
    public DeadState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {

    }

    public override IEnumerator Exit()
    {
        _playerEntity.Animator.SetBool(ANIM_BOOL_HURTING, false);
        _playerEntity.GroundCollider.enabled = true;
        _playerEntity.HeadCollider.enabled = true;
        _playerEntity.BodyCollider.enabled = true;
        return base.Exit();
    }

    public override IEnumerator Enter()
    {
        float pushForce = _playerEntity.PlayerControllerSettings.JumpVelocity;
        Rigidbody2D rb = _playerEntity.RigidBody;

        _playerEntity.Animator.SetBool(ANIM_BOOL_HURTING, true);
        _playerEntity.Animator.SetTrigger(ANIM_TRIGGER_DIE);

        //Make the player do a little jump
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * pushForce , ForceMode2D.Impulse);

        //Disable colliders so player falls through the map
        _playerEntity.GroundCollider.enabled = false;
        _playerEntity.HeadCollider.enabled = false;
        _playerEntity.BodyCollider.enabled = false;

        return base.Enter();
    }

    public override void Tick()
    {
        base.Tick();
    }
}
