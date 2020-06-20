using UnityEngine;

public interface IPlayerEntity
{
    Animator Animator { get; }
    Rigidbody2D RigidBody { get; }
    CommandBuffer CoyoteTimeBuffer { get; }
    CommandBuffer UngroundedJumpBuffer { get; }
    PlayerControllerSettings PlayerControllerSettings { get; }
    Collider2D HeadCollider { get; }
    Collider2D BodyCollider { get; }
    Collider2D GroundCollider { get; }
    IPlayerInput Input { get; }
}

