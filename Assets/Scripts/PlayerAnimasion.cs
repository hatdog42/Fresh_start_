using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private InputManager _input;
        private Rigidbody2D _rigidbody2D;
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _input = GetComponent<InputManager>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
      
            if (_playerMovement.IsPlayerGrounded())
            {
                _animator.Play(_input.moveDirection.x != 0 
                    ? "walk" : "idle");
            }
            else
            {
                _animator.Play(_rigidbody2D.velocity.y > 0 
                    ? "jump_up" : "jump_down");
            }

            if (_input.moveDirection.x == 0) return;
            var transform1 = transform;
            transform1.localScale = new Vector3(
                _input.moveDirection.x, 1,1);
        }
    }
}