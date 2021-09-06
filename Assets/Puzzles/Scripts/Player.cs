using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Puzzles
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
        private Vector3 _direction;
        private Vector3 _velocity;
        private float _yVelocity;
        private bool _canDoubleJump;
        private bool _canWallJump;
        private Vector3 _wallSurfaceNormal;
        [SerializeField]
        private float _pushPower = 2f;


        void OnEnable()
        {
            Coin.onCoinCollected += CoinCollected;
            DeadZone.onPlayerFell += Died;
            Elevator.onChangeParent += ChangeParent;
            MovingPlatform.onChangeParent += ChangeParent;
        }


        void OnDisable()
        {
            Coin.onCoinCollected -= CoinCollected;
            DeadZone.onPlayerFell -= Died;
            Elevator.onChangeParent -= ChangeParent;
            MovingPlatform.onChangeParent -= ChangeParent;
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

            if (_controller.isGrounded)
            {
                _canWallJump = false;

                _direction = new Vector3(horizontalInput, 0, 0);
                _velocity = _direction * _speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
                {
                    if (_canDoubleJump)
                    {
                        _yVelocity += _jumpHeight;
                        _canDoubleJump = false;
                    }
                }

                if (Input.GetKey(KeyCode.Space) && _canWallJump)
                {
                    _yVelocity = _jumpHeight;
                    _velocity = _wallSurfaceNormal * _speed;
                }

                _yVelocity -= _gravity;
            }

            _velocity.y = _yVelocity;
            _controller.Move(_velocity * Time.deltaTime);
        }


        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_controller.isGrounded == false && hit.transform.CompareTag("Wall"))
            {
                //Debug.DrawRay(hit.point, hit.normal, Color.blue);
                _wallSurfaceNormal = hit.normal;
                _canWallJump = true;
                return;
            }

            if (hit.transform.CompareTag("Pushable"))
            {
                Rigidbody body = hit.collider.attachedRigidbody;

                if (body == null || body.isKinematic)
                {
                    return; // The object isn't actually pushable.
                }

                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0); // Only push left/right.

                body.velocity = pushDir * _pushPower;
            }
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


        public int GetCoins()
        {
            return _coins;
        }


        IEnumerator RespawnPlayerRoutine()
        {
            _controller.enabled = false;
            transform.position = _startPos.position;
            yield return new WaitForSeconds(0.5f);
            _controller.enabled = true;
        }


        void ChangeParent(Transform newParent)
        {
            transform.parent = newParent;
        }
    }
}