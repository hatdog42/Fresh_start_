using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 12f;
    private Vector2 _desiredVelocity;

    [Header("CoyoteTime")] 
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("JumpBuffer")] 
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    
    [Header("acceleration")]
    public float accelerationTime = 0.02f;
    public float groundFriction = 0.03f;
    public float airFriction;
    
    [Header("isGrounded")]
    public LayerMask whatIsGround;
    
    [Header("Components")]
    private Rigidbody2D _rigidbody2D;
    private InputManager _input;
    private AudioSource _audioSource;
    
    public AudioClip[] jumpClips;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputManager>();
    }

    private void Update()
    {
        _desiredVelocity = _rigidbody2D.velocity;
        
        //animator.SetFloat("Speed", Mathf.Abs(_desiredVelocity.x));
        
        
        if (IsPlayerGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= 1 * Time.deltaTime;
        }
        
        
        if (_input.jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= 1 * Time.deltaTime;
        }
        
        
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            _audioSource.PlayOneShot(jumpClips[Random.Range(0,jumpClips.Length)]);
            _desiredVelocity.y = jumpSpeed;
            jumpBufferCounter = 0f;
        }
        
        if (_input.jumpReleased && _desiredVelocity.y > 0f)
        {
            _desiredVelocity.y *= 0.5f;
            coyoteTimeCounter = 0f;
        }
        
        
        
        _rigidbody2D.velocity = _desiredVelocity;
        
        
    }
    
    private void FixedUpdate()
    {
        if (-_input.moveDirection.x != 0)
        {
            _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x,moveSpeed * _input.moveDirection.x,accelerationTime);
        }
        else
        {
            _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x,0f,IsPlayerGrounded() ? groundFriction : airFriction);
        }

        _rigidbody2D.velocity = _desiredVelocity;
    }

    public bool IsPlayerGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, whatIsGround);
    }

    
}