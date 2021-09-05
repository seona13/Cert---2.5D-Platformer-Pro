using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Elevator : MonoBehaviour
{
	public static event Action<Transform> onChangeParent;

    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;
    [SerializeField]
    private float _speed;
    
    private bool _isCalled;
	private bool _isRiding;
	private float _step;



	void OnEnable()
	{
        ElevatorPanel.onElevatorCalled += CallElevator;
	}


	void OnDisable()
	{
		ElevatorPanel.onElevatorCalled -= CallElevator;
	}


	void Start()
    {
        _step = _speed * Time.deltaTime;
    }


    void FixedUpdate()
    {
        if (_isCalled)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _step);
        }
		if (_isCalled && transform.position == _pointA.position)
		{
            _isCalled = false;
		}

        if (_isRiding)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _step);
        }
		if (_isRiding && transform.position == _pointB.position)
		{
			_isRiding = false;
			onChangeParent?.Invoke(null);
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(RideElevatorRoutine());
		}
	}


	void CallElevator()
	{
		_isCalled = true;
	}


	IEnumerator RideElevatorRoutine()
	{
		yield return new WaitForSeconds(0.2f);
		_isRiding = true;
		onChangeParent?.Invoke(this.transform);
	}
}