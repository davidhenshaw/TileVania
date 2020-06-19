using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControllerSettings")]
public class PlayerControllerSettings : ScriptableObject
{

    [Header("Moving")]
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float verticalSpeed = 4f;

    [Header("Jumping")]
    [SerializeField] private float jumpVelocity = 10f;
    [Range(0, 1)] private float jumpDamping = 0.5f;

    [Tooltip("Measured in milliseconds")]
    [SerializeField] private int coyoteTime = 500;
    [Tooltip("Measured in milliseconds")]
    [SerializeField] private int ungroundedJumpTime = 1000;

    public float HorizontalSpeed { get => horizontalSpeed; }
    public float VerticalSpeed { get => verticalSpeed; }
    public float JumpVelocity { get => jumpVelocity;  }
    public float JumpDamping { get => jumpDamping; }
    public float CoyoteTime { get => coyoteTime/1000f; }
    public float UngroundedJumpTime { get => ungroundedJumpTime/1000f; }
}
