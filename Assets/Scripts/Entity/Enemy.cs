using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const string ANIM_TRIGGER_DIE = "died";

    Rigidbody2D _rigidbody;
    EdgeDetector2D _wallEdgeDetector;
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] Vector2 _direction;
    [SerializeField] Collider2D _bodyCollider;
    [SerializeField] LayerMask _walkableSurfaceMask;

    private void Awake()
    {
        _direction.Normalize();
        _wallEdgeDetector = GetComponentInChildren<EdgeDetector2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bodyCollider = GetComponent<Collider2D>();
    }


    private void Start()
    {
        _wallEdgeDetector.FallingEdge += FlipX;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHorizontal();
    }

    public void Die()
    {

    }

    private IEnumerator Co_Die()
    {
        GetComponent<Animator>().SetTrigger(ANIM_TRIGGER_DIE);
        yield return new WaitForSeconds(3);
    }

    private void MoveHorizontal()
    {
        float xSpeed = (_direction.normalized * _moveSpeed).x;
        _rigidbody.velocity = new Vector3(xSpeed, _rigidbody.velocity.y);
    }

    [ContextMenu("Flip about x")]
    void FlipX()
    {
        transform.RotateAround(transform.position, Vector3.up, 180);
        _direction.x = _direction.x * -1;
    }

    [ContextMenu("Flip about y")]
    void FlipY()
    {
        transform.RotateAround(transform.position, Vector3.right, 180);
    }

    void FlipAboutAxis(Vector3 axis)
    {
        transform.RotateAround(transform.position, axis, 180);
    }
}
