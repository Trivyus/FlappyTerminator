using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveDirection = 1)
    {
        _rigidbody.velocity = new Vector2(moveDirection * _moveSpeed, _rigidbody.velocity.y);
    }

    public void StopMoving()
    {
        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        enabled = false;
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}
