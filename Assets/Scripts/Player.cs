using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;



    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
		{
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 direction = new Vector3(horizontalInput, 0, 0);

            _controller.Move(direction * Time.deltaTime);
		}
    }
}
