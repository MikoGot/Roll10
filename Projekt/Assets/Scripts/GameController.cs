using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
	public GameObject mainMenu;
	public GameObject registerMenu;
	public GameObject loginMenu;
	public GameObject gameMenu;
	public List<GameObject> menusList = new List<GameObject>();

	public TextMeshProUGUI playerNicknameDisplay;
	void Start()
	{
		menusList[0] = mainMenu;
		menusList[1] = registerMenu;
		menusList[2] = loginMenu;
		menusList[3] = gameMenu;
	}

	public void PlayerDisplay()
	{
		if(DBManager.LoggedIn) playerNicknameDisplay.SetText("Player: " + DBManager.nickname);
	}

	public void GoToMainMenu()
	{
		foreach(GameObject go in menusList) go.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void GoToRegisterMenu()
	{
		foreach(GameObject go in menusList) go.SetActive(false);
		registerMenu.SetActive(true);
	}

	public void GoToLoginMenu()
	{
		foreach(GameObject go in menusList) go.SetActive(false);
		loginMenu.SetActive(true);
	}

	public void GoToGame()
	{
		foreach(GameObject go in menusList) go.SetActive(false);
		gameMenu.SetActive(true);
	}
}
