using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaypointMovement : MonoBehaviour
{
    const float stopThreshold = 0.05f;

    Vector3 _startPos;
    Transform _currDest;
    int _wpIndex;
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] float _moveTime;
    [SerializeField] Ease _easeType;
    [SerializeField] bool _loop;
    [SerializeField] bool _startOnAwake;

    private void Awake()
    {
        _startPos = transform.position;
        _wpIndex = 0;
        _currDest = _waypoints[0];
    }

    private void Start()
    {
        if (_startOnAwake)
            BeginMove(_waypoints[0].position);
    }

    public Vector3 GetNextDestination()
    {

        if (_wpIndex < _waypoints.Count - 1)
            _wpIndex++;
        else
        {
            if (_loop)
                _wpIndex = 0;
        }

        return _waypoints[_wpIndex].position;
    }

    public void BeginMove(Vector3 destination)
    {
        transform.DOMove(destination, _moveTime).SetEase(_easeType).OnComplete(() => BeginMove(GetNextDestination()));
    }

    public void BeginMove(Vector3 destination, Ease ease)
    {
        transform.DOMove(destination, _moveTime).SetEase(ease).OnComplete(() => BeginMove(GetNextDestination()));
    }

    public bool IsDoneMoving()
    {
        return Mathf.Abs(Vector3.Distance(transform.position, _currDest.position)) < stopThreshold;
    }
}
