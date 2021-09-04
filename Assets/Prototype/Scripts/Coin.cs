using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Prototype
{
	public class Coin : MonoBehaviour
	{
		public static event Action onCoinCollected;



		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				onCoinCollected?.Invoke();
				Destroy(gameObject);
			}
		}
	}
}