using System.Collections;

public interface IState
{
    IEnumerator Enter();
    IEnumerator Exit();
    void Tick();
    void CheckNextStates();
}
