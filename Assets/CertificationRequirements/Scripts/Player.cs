using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _gravity = 0.3f;
    [SerializeField]
    private float _jumpHeight = 20f;

    private Vector3 _direction;
    private Vector3 _velocity;
    private float _yVelocity;



    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
		{
            Debug.LogError("Player missing CharacterController.");
		}
    }


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded)
		{
            _direction = new Vector3(0, 0, horizontalInput);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
			{
                _yVelocity = _jumpHeight;
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