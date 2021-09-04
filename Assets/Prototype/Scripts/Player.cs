using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


namespace Prototype
{
    public class Player : MonoBehaviour
    {
        public static event Action<int> onUpdateLifeCount;
        public static event Action<int> onUpdateCoinCount;

        private CharacterController _controller;

        private int _lives = 3;
        private int _coins;

        [SerializeField]
        private Transform _startPos;

        [Space(10)]

        [Header("Movement")]
        [SerializeField]
        private float _speed = 5f;
        [SerializeField]
        private float _gravity = 1f;
        [SerializeField]
        private float _jumpHeight = 15f;
        private float _yVelocity;
        private bool _canDoubleJump;


        void OnEnable()
        {
            Coin.onCoinCollected += CoinCollected;
            DeadZone.onPlayerFell += Died;
        }


        void OnDisable()
        {
            Coin.onCoinCollected -= CoinCollected;
            DeadZone.onPlayerFell -= Died;
        }


        void Start()
        {
            _controller = GetComponent<CharacterController>();
            if (_controller == null)
            {
                Debug.LogError("Player missing Character Controller.");
            }

            StartGame();
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


        void StartGame()
        {
            _lives = 3;
            onUpdateLifeCount?.Invoke(_lives);

            _coins = 0;
            onUpdateCoinCount?.Invoke(_coins);

            transform.position = _startPos.position;
        }


        void Died()
        {
            _lives--;

            onUpdateLifeCount?.Invoke(_lives);

            if (_lives < 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            StartCoroutine(RespawnPlayerRoutine());
        }


        void CoinCollected()
        {
            _coins++;
            onUpdateCoinCount?.Invoke(_coins);
        }


        IEnumerator RespawnPlayerRoutine()
        {
            _controller.enabled = false;
            transform.position = _startPos.position;
            yield return new WaitForSeconds(0.5f);
            _controller.enabled = true;
        }
    }
}