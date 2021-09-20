using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Ledge : MonoBehaviour
{
	public static event Action<Ledge> onLedgeGrab;

	[SerializeField]
	private Transform _handPos;
	[SerializeField]
	private Transform _standPos;



	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("LedgeGrabChecker"))
		{
			onLedgeGrab?.Invoke(this);
		}
	}


	public Vector3 GetHandPos()
	{
		return _handPos.position;
	}


	public Vector3 GetStandPos()
	{
		return _standPos.position;
	}
}