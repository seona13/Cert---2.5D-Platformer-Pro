using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _anim;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _gravity = 0.3f;
    [SerializeField]
    private float _jumpHeight = 20f;

    private Vector3 _direction;
    private Vector3 _velocity;
    private float _yVelocity;
    private bool _isJumping;



    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
		{
            Debug.LogError("Player missing CharacterController.");
		}

        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
		{
            Debug.LogError("Player Model missing Animator.");
		}
    }


    void Update()
    {
        if (_isJumping)
        {
                _isJumping = false;
            _anim.SetBool("IsJumping", _isJumping);
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (_controller.isGrounded)
		{
            _direction = new Vector3(0, 0, horizontalInput);
            _velocity = _direction * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

            if (horizontalInput != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (_isJumping)
            {
                 _isJumping = false;
               _anim.SetBool("IsJumping", _isJumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
			{
                _yVelocity = _jumpHeight;
                _isJumping = true;
                _anim.SetBool("IsJumping", _isJumping);
			}
		}
        else
		{
            _yVelocity -= _gravity;
		}

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }
}