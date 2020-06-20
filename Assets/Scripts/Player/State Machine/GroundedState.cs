using System.Collections;
using UnityEngine;

public class GroundedState : PlayerMovementState
{
    // cached references
    Collider2D _headCollider;
   

    public GroundedState(IPlayerEntity playerEntity, IStateMachine stateMachine) : base(playerEntity, stateMachine)
    {
        _rigidBody = _playerController.RigidBody;
        _animator = _playerController.Animator;
        _groundCollider = _playerController.GroundCollider;
        _headCollider = _playerController.HeadCollider;
    }


    public override IState CalculateNextState()
    {
        if(RequestedClimb())
            return new ClimbState(_playerController, _stateMachine);

        if (!IsGrounded())
            return new UngroundedState(_playerController, _stateMachine);

        if (IsHoldingCrouch())
            return new CrouchState(_playerController, _stateMachine);

        return null;
    }

    public override IEnumerator Enter()
    {
        _nextStates.Add(
            () => RequestedClimb(),
            new ClimbState(_playerController, _stateMachine));

        _nextStates.Add(
            () => !IsGrounded(),
            new UngroundedState(_playerController, _stateMachine));

        _nextStates.Add(
            () => IsHoldingCrouch(),
            new CrouchState(_playerController, _stateMachine));

        return base.Enter();
    }

    public override IEnumerator Exit()
    {
        _coyoteTimeBuffer.SetFlag(true);
        _animator.SetBool(ANIM_BOOL_RUNNING, false);
        return base.Exit();
    }

    public override void Tick()
    {
        base.HandleJump();
        MoveHorizontal();
        MoveVertical();
        HandleInteractions();

        base.Tick();
    }

    void HandleInteractions()
    {
        if( !Input.GetButtonDown("Interact") )
        {
            return;
        }

        int maxCollisions = 5;
        Collider2D[] results = new Collider2D[maxCollisions];
        ContactFilter2D filter = new ContactFilter2D().NoFilter();

        if ( _headCollider.OverlapCollider(filter, results) > 0)
        {
            foreach(Collider2D collider in results)
            {
                IInteractable interactable = collider?.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact();
                    break;
                }

            }
        }
    }

    new void MoveHorizontal()
    {
        float xAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(xAxis) > Mathf.Epsilon) // if the x-axis input is greater than the smallest non-zero float
        {
            //Trigger animation
            _animator.SetBool(ANIM_BOOL_RUNNING, true);
        }
        else
        {
            //Trigger animation
            _animator.SetBool(ANIM_BOOL_RUNNING, false);
        }

        base.MoveHorizontal();
    }

    void MoveVertical()
    {
        float yAxis = Input.GetAxis("Vertical");
    }

    bool IsHoldingCrouch()
    {
        return Input.GetAxis("Vertical") < 0;
    }


}
