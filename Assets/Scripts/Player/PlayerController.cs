using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerEntity
{
    public static event Action<Vector2> PlayerDied;

    Animator _animator;
    Rigidbody2D _rigidBody;
    CommandBuffer _coyoteTimeBuffer;
    CommandBuffer _ungroundedJumpBuffer;
    KeyboardInput _input;
    [SerializeField] PlayerControllerSettings _playerSettings;
    [SerializeField] Collider2D _groundCollider;
    [SerializeField] Collider2D _headCollider;
    [SerializeField] Collider2D _bodyCollider;

    public Animator Animator { get => _animator; }
    public Rigidbody2D RigidBody { get => _rigidBody; }
    public PlayerControllerSettings PlayerControllerSettings { get => _playerSettings; }
    public Collider2D HeadCollider { get => _headCollider;  }
    public Collider2D BodyCollider { get => _bodyCollider;  }
    public Collider2D GroundCollider { get => _groundCollider; }
    public CommandBuffer UngroundedJumpBuffer { get => _ungroundedJumpBuffer; }
    public CommandBuffer CoyoteTimeBuffer { get => _coyoteTimeBuffer; }
    public PlayerInput Input { get => _input; }

    [Header("Sounds")]
    [SerializeField] AudioClip _deathSound;
    IStateMachine _stateMachine;

    void Awake()
    {
        _input = new KeyboardInput();
        _stateMachine = GetComponent<IStateMachine>();
        _coyoteTimeBuffer = new CommandBuffer(_playerSettings.CoyoteTime); 
        _ungroundedJumpBuffer = new CommandBuffer(_playerSettings.UngroundedJumpTime);
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine.SetState(
            new UngroundedState(this, _stateMachine));
    }

    private void FixedUpdate()
    {
        CheckOneWayStates();

        _ungroundedJumpBuffer.Tick(Time.fixedDeltaTime);
        _coyoteTimeBuffer.Tick(Time.fixedDeltaTime);

        if(_input.JumpPressed)
        {
            _coyoteTimeBuffer.SetFlag(false);
        }
    }

    private void CheckOneWayStates()
    {
        // Player Death
        if (IsTouchingHazard())
        {
            PlayerDied?.Invoke(transform.position);
            SFX.instance.Play(_deathSound);
            _stateMachine.SetState(new DeadState(this, _stateMachine));
            return;
        }

        if (IsTouchingWater())
        {
            _stateMachine.SetState(new SwimState(this, _stateMachine));
        }
    }

    bool IsTouchingHazard()
    {
        return _bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"));
    }

    bool IsTouchingWater()
    {
        return _headCollider.IsTouchingLayers(LayerMask.GetMask("Water"));
    }

}

