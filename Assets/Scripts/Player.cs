using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{
    public static event Action<int> onUpdateCoinCount;

    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15f;
    private float _yVelocity;
    private bool _canDoubleJump;

    private int _coins;


	void OnEnable()
	{
        Coin.onCoinCollected += CoinCollected;
	}


	void OnDisable()
	{
        Coin.onCoinCollected -= CoinCollected;
    }


    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
	    {
            if (Input.GetKeyDown(KeyCode.Space))
			{
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
			}
	    }
        else
	    {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }
            else
            {
                _yVelocity -= _gravity;
            }
	    }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }


    void CoinCollected()
	{
        _coins++;
        onUpdateCoinCount?.Invoke(_coins);
	}
}