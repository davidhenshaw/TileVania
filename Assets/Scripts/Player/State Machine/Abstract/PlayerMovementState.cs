using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementState : PlayerState
{
    protected Rigidbody2D _rigidBody;
    protected Animator _animator;
    protected Collider2D _groundCollider;
    protected CommandBuffer _coyoteTimeBuffer;
    protected IPlayerEntity _playerController;
    protected IPlayerInput _input;
    protected PlayerControllerSettings _playerSettings;

    public PlayerMovementState(IPlayerEntity playerController, IStateMachine stateMachine) : base(stateMachine)
    {
        _playerController = playerController;
        _rigidBody = playerController.RigidBody;
        _animator = playerController.Animator;
        _groundCollider = playerController.GroundCollider;
        _coyoteTimeBuffer = playerController.CoyoteTimeBuffer;
        _input = playerController.Input;
        _playerSettings = playerController.PlayerControllerSettings;
    }

    protected void HandleJump()
    {
        bool jumpRequested = _input.JumpPressed;
        bool canJump = IsGrounded() || _coyoteTimeBuffer.GetFlag() || IsSwimming();
        bool jumpWasBuffered = _playerController.UngroundedJumpBuffer.GetFlag();

        if (jumpRequested && canJump || jumpWasBuffered && canJump)
        {
            Jump();
        }

        if (_input.JumpReleased && !IsGrounded() && _rigidBody.velocity.y > 0)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y * _playerSettings.JumpDamping);
        }
    }

    protected void Jump()
    {
            //Play Sound

            //Kill all previous vertical velocity
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            // Do Jump
            _rigidBody.velocity += Vector2.up * _playerSettings.JumpVelocity;
            //Reset jump buffers so that you don't keep jumping
            _playerController.UngroundedJumpBuffer.SetFlag(false);
            _playerController.CoyoteTimeBuffer.SetFlag(false);
    }

    protected void MoveHorizontal()
    {
        //float xAxis = Input.GetAxis("Horizontal");
        float xAxis = _input.Horizontal;

        FlipSprite(xAxis);

        var dx = xAxis * _playerSettings.HorizontalSpeed * Time.deltaTime;
        Vector3 movement = Vector2.right * dx;

        _rigidBody.velocity = new Vector2(_playerSettings.HorizontalSpeed * xAxis, _rigidBody.velocity.y);
    }

    protected void FlipSprite(float horizontalAxis)
    {
        SpriteRenderer renderer = _playerController.Animator.GetComponentInChildren<SpriteRenderer>();
        // If horizontal input is very small or zero, don't flip
        if (Mathf.Abs(horizontalAxis) < Mathf.Epsilon)
            return;

        // Flip sprite on the X axis if horizontal input is to the left (negative)
        renderer.flipX = horizontalAxis < 0;
    }

    protected bool CanClimb()
    {
        return _groundCollider.IsTouchingLayers(LayerMask.GetMask("Climbable"));
    }

    protected bool IsGrounded()
    {
        return _groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    protected bool IsFalling()
    {
        return _rigidBody.velocity.y <= 0 && !IsGrounded();
    }

    protected bool IsJumping()
    {
        return _rigidBody.velocity.y > 0 && !IsGrounded();
    }

    protected bool IsSwimming()
    {
        return _groundCollider.IsTouchingLayers(LayerMask.GetMask("Water"));
    }

    protected bool RequestedClimb()
    {
        return _input.ClimbPressed && _groundCollider.IsTouchingLayers(LayerMask.GetMask("Climbable"));
    }

}
