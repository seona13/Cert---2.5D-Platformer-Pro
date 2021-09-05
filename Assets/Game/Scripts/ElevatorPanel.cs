using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ElevatorPanel : MonoBehaviour
{
	public static event Action onElevatorCalled;

	[SerializeField]
	private Renderer _callButtonRenderer;
	[SerializeField]
	private int _coinsNeeded = 8;



	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				if (Input.GetKeyDown(KeyCode.E) && player.GetCoins() >= _coinsNeeded)
				{
					_callButtonRenderer.material.color = Color.green;
					onElevatorCalled?.Invoke();
				}
			}
		}
	}
}