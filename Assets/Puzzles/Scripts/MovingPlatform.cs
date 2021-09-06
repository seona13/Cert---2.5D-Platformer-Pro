using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Puzzles
{
    public class MovingPlatform : MonoBehaviour
    {
        public static event Action<Transform> onChangeParent;

        [SerializeField]
        private Transform _pointA;
        [SerializeField]
        private Transform _pointB;
        [SerializeField]
        private float _speed;

        private Vector3 _target;
        private float _step;



        void Start()
        {
            _step = _speed * Time.deltaTime;
        }


        void FixedUpdate()
        {
            if (transform.position == _pointA.position)
            {
                _target = _pointB.position;
            }
            else if (transform.position == _pointB.position)
            {
                _target = _pointA.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, _target, _step);
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onChangeParent?.Invoke(this.transform);
            }
        }


        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onChangeParent?.Invoke(null);
            }
        }
    }
}