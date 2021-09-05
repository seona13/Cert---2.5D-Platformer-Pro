using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressurePad : MonoBehaviour
{
	[SerializeField]
	private float _marginOfError = 0.05f;



	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Pushable"))
		{
			float distance = Vector3.Distance(transform.position, other.transform.position);

			if (distance <= _marginOfError)
			{
				other.attachedRigidbody.isKinematic = true;
				
				Renderer renderer = GetComponentInChildren<Renderer>();
				if (renderer != null)
				{
					renderer.material.color = Color.blue;
				}

				Renderer box = other.GetComponent<Renderer>();
				if (box != null)
				{
					box.material.color = Color.green;
				}
			}
		}
	}
}