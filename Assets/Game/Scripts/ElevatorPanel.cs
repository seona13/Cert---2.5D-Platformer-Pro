using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorPanel : MonoBehaviour
{
	[SerializeField]
	private Renderer _callButtonRenderer;
	[SerializeField]
	private int _coinsNeeded = 8;



	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				if (player.GetCoins() >= _coinsNeeded)
				{
					_callButtonRenderer.material.color = Color.green;
				}
			}
		}
	}
}