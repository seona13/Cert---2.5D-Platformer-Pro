using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Ledge : MonoBehaviour
{
	public static event Action<Ledge> onLedgeGrab;

	[SerializeField]
	private Vector3 _snapToPoint;
	[SerializeField]
	private Vector3 _standPos;



	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("LedgeGrabChecker"))
		{
			onLedgeGrab?.Invoke(this);
		}
	}


	public Vector3 GetSnapToPoint()
	{
		return _snapToPoint;
	}


	public Vector3 GetStandPos()
	{
		return _standPos;
	}
}