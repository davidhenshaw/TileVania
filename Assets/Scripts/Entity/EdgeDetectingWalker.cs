using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetectingWalker : MonoBehaviour
{
    [SerializeField] EdgeDetector2D _playerDetector;
    [SerializeField] EdgeDetector2D _groundDetector;
    Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _playerDetector.RisingEdge += FlipX;
        _groundDetector.FallingEdge += FlipX;
    }

    void FlipX()
    {
        _enemy.FlipX();
    }
}
