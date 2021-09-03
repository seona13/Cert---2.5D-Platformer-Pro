using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Text _coinCount;



	void OnEnable()
	{
        Player.onUpdateCoinCount += UpdateCoinCount;
	}


	void OnDisable()
	{
		Player.onUpdateCoinCount -= UpdateCoinCount;
	}


	void Start()
    {
		UpdateCoinCount(0);
    }


    void Update()
    {
        
    }


	void UpdateCoinCount(int coins)
	{
		_coinCount.text = coins.ToString();
	}
}