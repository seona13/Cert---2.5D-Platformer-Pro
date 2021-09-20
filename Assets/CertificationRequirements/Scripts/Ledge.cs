using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Ledge : MonoBehaviour
{
	public static event Action<Vector3> onLedgeGrab;

	[SerializeField]
	private Vector3 _snapToPoint;



	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("LedgeGrabChecker"))
		{
			onLedgeGrab?.Invoke(_snapToPoint);
		}
	}
}