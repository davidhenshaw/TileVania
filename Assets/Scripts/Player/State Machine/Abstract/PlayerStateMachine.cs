using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IStateMachine
{
    protected IState _currState;
    protected IState _defaultState;

    public IState CurrentState { get => _currState; }

    public virtual void Awake()
    {
        
    }

    protected void Update()
    {
        _currState?.Tick();
        _currState?.CheckNextStates();

        //IState nextState = _currState?.GetNextState();
        //if(nextState != null)
        //{
        //    SetState(nextState);
        //}

    }

    public virtual void SetState(IState newState)
    {
        if (newState?.GetType() == _currState?.GetType())
        {
            //Debug.LogWarning($"Player State Machine tried to move to {newState.GetType().ToString()} twice. \n" +
            //    $"State machine will ignore this" );
            return;
        }

        if (newState == null)
        {
            Debug.LogError("Player State Machine tried to move to null state");
            return;
        }

        if (_currState != null)
            StartCoroutine(_currState.Exit());

        StartCoroutine(newState.Enter());

        _currState = newState;
        //Debug.Log("Player state set to: " + _currState.GetType().ToString());
    }
}
