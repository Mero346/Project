using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Vector3 _groundCheckOffset;
    [SerializeField] private Vector3 _wallCheckOffset;
    [SerializeField] private Vector3 _wallCheckDirection;

    [SerializeField] private LayerMask groundMask;

    private Vector3 _input;
    private bool _isMoving;
    private bool _isGrounded;
    [SerializeField] private bool _onWall;
    private bool _isFlying;

    private Rigidbody2D _rigidbody;
    private CharacterAnimations _animations;
    [SerializeField] private SpriteRenderer _characterSprite;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animations = GetComponent<CharacterAnimations>();
    }

    private void Update()
    {
        Move();
        CheckGround();
        CheckWall();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        _animations.IsMoving = _isMoving;
        _animations.IsGrounded = _isGrounded;
        _animations.OnWall = _onWall;
        _animations.IsFlying = IsFlying();
    }

    private bool IsFlying()
    {
            if (_rigidbody.velocity.y < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
    }

    private void CheckGround()
    {
        float rayLength = 0.3f;
        Vector3 rayStartPosition = transform.position + _groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, rayStartPosition + Vector3.down, rayLength, groundMask);

        if (hit.collider != null)
        {
            _isGrounded = hit.collider.CompareTag("Ground") ? true : false;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private void CheckWall()
    {
        float rayLength = 0.05f;
        Vector3 rayStartPosition = transform.position + _wallCheckOffset * _wallCheckDirection.x;
        RaycastHit2D wallHit = Physics2D.Raycast(rayStartPosition, rayStartPosition + _wallCheckDirection, rayLength, groundMask);

        if (wallHit.collider != null)
        {
            if (wallHit.collider.CompareTag("Ground"))
            {
                if (!_onWall)
                {
                    _rigidbody.velocity = Vector2.zero;
                }
                _onWall = true;
                if (_rigidbody.velocity.y < 0)
                {
                    _rigidbody.gravityScale = 0.1f;
                    _rigidbody.mass = 0.5f;
                }
                else
                {
                    _rigidbody.gravityScale = 1;
                    _rigidbody.mass = 1f;
                }

            }
            else
            {
                _onWall = false;
                _rigidbody.gravityScale = 1;
                _rigidbody.mass = 1f;
            }
        }
        else
        {
            _onWall = false;
            _rigidbody.gravityScale = 1f;
            _rigidbody.mass = 1f;
        }
    }

    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (!_onWall)
        {
            transform.position += _input * _speed * Time.deltaTime;
        }
        else
        {
            if (_wallCheckDirection.x > 0)
            {
                if(_input.x < 0)
                {
                    transform.position += _input * _speed * Time.deltaTime;
                }
            }
            else
            {
                if(_input.x > 0)
                {
                    transform.position += _input * _speed * Time.deltaTime;
                }
            }
        }
        _isMoving = _input.x != 0 ? true : false;

        if (_isMoving && !_onWall)
        {
            _characterSprite.flipX = _input.x > 0 ? false : true;
            _wallCheckDirection = _input.x > 0 ? transform.right : -transform.right;
        }
    }

    private void Jump()
    {

        if (_isGrounded)
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _animations.Jump();
        }
        else
        {
            if (_onWall)
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(transform.up * _jumpForce / 2 - _wallCheckDirection * _jumpForce / 7, ForceMode2D.Impulse);
                _characterSprite.flipX = _wallCheckDirection.x > 0 ? true : false;
                _input = _wallCheckDirection * -1;
                _wallCheckDirection *= -1;
                _animations.Jump();
            }
        }
    }

    public void Knockback(Vector2 vector)
    {
        _rigidbody.velocity = Vector2.zero;
        float knockbackForce = 3f;
        _rigidbody.AddForce(vector * knockbackForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 rayStartPosition = transform.position + _groundCheckOffset;
        Gizmos.DrawLine(rayStartPosition, rayStartPosition + Vector3.down * 0.3f);

        Gizmos.color = Color.red;
        Vector3 ray2StartPosition = transform.position + _wallCheckOffset * _wallCheckDirection.x;
        Gizmos.DrawLine(ray2StartPosition, ray2StartPosition + _wallCheckDirection * 1f);
    }
}