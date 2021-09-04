using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorPanel : MonoBehaviour
{
	[SerializeField]
	private Renderer _callButtonRenderer;



	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
		{
			_callButtonRenderer.material.color = Color.green;
		}
	}
}