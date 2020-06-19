public class Timer
{
    float _remainingTime;
    float _duration;
    bool _isDone;
    bool _isRunning;

    public bool IsRunning { get => _isRunning; }

    public Timer(float duration)
    {
        _duration = duration;
        _remainingTime = duration;
        _isDone = false;
    }

    public bool IsDone()
    {
        return _isDone;
    }

    public void Tick(float deltaTime)
    {
        if (_remainingTime > 0)
        {
            _remainingTime -= deltaTime;
        }
        else
        {
            _remainingTime = 0;
            _isDone = true;
        }
    }

    public void Reset()
    {
        _remainingTime = _duration;
        _isDone = false;
    }
}
