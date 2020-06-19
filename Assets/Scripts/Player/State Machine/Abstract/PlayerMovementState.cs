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
    protected IPlayerEntity _playerEntity;
    protected PlayerControllerSettings _playerSettings;

    public PlayerMovementState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(stateMachine)
    {
        _playerEntity = playerEntity;

        _rigidBody = playerEntity.RigidBody;
        _animator = playerEntity.Animator;
        _groundCollider = playerEntity.GroundCollider;
        _coyoteTimeBuffer = playerEntity.CoyoteTimeBuffer;
        _playerSettings = playerEntity.PlayerControllerSettings;
    }

    protected void HandleJump()
    {
        bool jumpRequested = Input.GetButtonDown("Jump");
        bool canJump = IsGrounded() || _coyoteTimeBuffer.GetFlag() || IsSwimming();
        bool jumpWasBuffered = _playerEntity.UngroundedJumpBuffer.GetFlag();

        if (jumpRequested && canJump || jumpWasBuffered && canJump)
        {
            //Kill vertical velocity
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            // Do Jump
            _rigidBody.velocity += Vector2.up * _playerSettings.JumpVelocity;
            //Reset jump buffers so that you don't keep jumping
            _playerEntity.UngroundedJumpBuffer.SetFlag(false);
            _playerEntity.CoyoteTimeBuffer.SetFlag(false);
        }

        if (Input.GetButtonUp("Jump") && !IsGrounded() && _rigidBody.velocity.y > 0)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y * _playerSettings.JumpDamping);
        }
    }

    protected void MoveHorizontal()
    {
        float xAxis = Input.GetAxis("Horizontal");

        FlipSprite(xAxis);

        var dx = xAxis * _playerSettings.HorizontalSpeed * Time.deltaTime;
        Vector3 movement = Vector2.right * dx;

        _rigidBody.velocity = new Vector2(_playerSettings.HorizontalSpeed * xAxis, _rigidBody.velocity.y);
    }

    protected void FlipSprite(float horizontalAxis)
    {
        SpriteRenderer renderer = _playerEntity.Animator.GetComponentInChildren<SpriteRenderer>();
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
}
