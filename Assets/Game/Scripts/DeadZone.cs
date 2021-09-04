using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadZone : MonoBehaviour
{
	public static event Action onPlayerFell;



	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			onPlayerFell?.Invoke();
		}
	}
}