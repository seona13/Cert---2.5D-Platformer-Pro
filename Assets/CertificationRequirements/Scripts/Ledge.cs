using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Ledge : MonoBehaviour
{
	public static event Action onLedgeGrab;



	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("LedgeGrabChecker"))
		{
			onLedgeGrab?.Invoke();
		}
	}
}