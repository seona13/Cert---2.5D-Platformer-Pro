using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;
    [SerializeField]
    private float _speed;
    
    private bool _isCalled;
	private Vector3 _target;
	private float _step;



	private void OnEnable()
	{
        ElevatorPanel.onElevatorCalled += CallElevator;
	}


	private void OnDisable()
	{
		ElevatorPanel.onElevatorCalled -= CallElevator;
	}


	void Start()
    {
        _step = _speed * Time.deltaTime;
    }


    void Update()
    {
        if (_isCalled)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _step);
        }

        if (transform.position == _pointA.position)
		{
            _isCalled = false;
		}
    }


    void CallElevator()
	{
		_isCalled = true;
	}
}