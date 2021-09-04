using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Text _livesCount;
	[SerializeField]
	private Text _coinCount;



	void OnEnable()
	{
        Player.onUpdateLifeCount += UpdateLivesCount;
        Player.onUpdateCoinCount += UpdateCoinCount;
	}


	void OnDisable()
	{
        Player.onUpdateLifeCount -= UpdateLivesCount;
		Player.onUpdateCoinCount -= UpdateCoinCount;
	}


	void UpdateLivesCount(int lives)
	{
		_livesCount.text = lives.ToString();
	}


	void UpdateCoinCount(int coins)
	{
		_coinCount.text = coins.ToString();
	}
}