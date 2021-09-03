using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
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


    void Update()
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
}