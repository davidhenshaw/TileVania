using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPounceState : PlayerMovementState
{
    EdgeDetector edgeDetector;
    float horizontalForce = 0.7f;
    float verticalForce = 0.7f;
    Vector2 jumpDirection;
    float jumpForce;

    Hitbox _hitbox;
    int _bounceCount = 0;

    public GroundedPounceState(IPlayerEntity playerController, IStateMachine stateMachine) : base(playerController, stateMachine)
    {
        playerController.FeetHitbox.enabled = false;

        _hitbox = playerController.FeetHitbox;
        jumpDirection = new Vector2(horizontalForce, verticalForce);
        jumpForce = playerController.PlayerControllerSettings.JumpVelocity;
        edgeDetector = new EdgeDetector(base.IsGrounded);
    }

    public override IState CalculateNextState()
    {
        if (edgeDetector.IsRisingEdge())
            return new GroundedState(_playerController, _stateMachine);

        return null;
    }

    public override IEnumerator Enter()
    {
        Pounce();
        _playerController.FeetHitbox.hit += Bounce;
        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        _playerController.FeetHitbox.hit -= Bounce;
        _playerController.FeetHitbox.enabled = false;
        _bounceCount = 0;
        return base.Exit();
    }

    public override void Tick()
    {
        edgeDetector.Poll();

        if (_bounceCount > 0)
            MoveHorizontal();
    }

    private void Pounce()
    {
        //Kill all velocity
        _rigidBody.velocity = Vector2.zero;

        //Determine horizontal direction for jump based on the player's scale (negative values mean the player is facing left)
        jumpDirection = new Vector2(jumpDirection.x * _rigidBody.transform.localScale.x,
                                        jumpDirection.y);

        //Activate hitbox
        _hitbox.enabled = true;

        //Do Pounce
        _rigidBody.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);
    }

    private void Bounce()
    {
        _bounceCount++;
        //Kill all velocity
        _rigidBody.velocity = Vector2.zero;

        //Determine horizontal direction for jump based on the player's scale (negative values mean the player is facing left)
        jumpDirection = Vector2.up;

        //Do Pounce
        _rigidBody.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);
    }
}
