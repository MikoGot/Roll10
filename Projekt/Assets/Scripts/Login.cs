using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour {
	public GameController gameController;
	public WindowController windowController;
	public TMP_InputField nicknameField;
	public TMP_InputField passwordField;
	public Notification notification;
	public Button submitButton;

	public void CallLogin()
	{
		StartCoroutine(LoginPlayer());
	}

	IEnumerator LoginPlayer()
	{
		WWWForm form = new WWWForm();
		form.AddField("nickname", nicknameField.text);
		form.AddField("password", passwordField.text);

		string url = "http://localhost:8888/sqlconnect/login.php";
		WWW www = new WWW(url, form);

		yield return www;
		
		if(www.text[0] == '0')
		{
			DBManager.nickname = nicknameField.text;
			DBManager.score = int.Parse(www.text.Split('\t')[1]);
			DBManager.cookie = www.text.Split('\t')[2];
			Debug.Log("COOKIE: " + www.text.Split('\t')[2]);
			Debug.Log("Reply: " + www.text);
			windowController.OpenToolsMenu();
			gameController.loginMenu.SetActive(false);
			gameController.PlayerDisplay();

			string logInInfo = "Successfully logged in as: " + DBManager.nickname;
			notification.NotificationAction(logInInfo, notification.positiveS);
		}
		else{
			Debug.Log("User login failed. Error #" + www.text);
			notification.NotificationAction("User login failed.", notification.negativeS);
		}
	}

	public void VerifyInputs()
	{
		submitButton.interactable = (nicknameField.text.Length >= 8 && passwordField.text.Length >= 8);
		//Debug.Log("Checking if proper length of text");
		notification.NotificationAction("At least 8 required...", notification.warningS);
	}
}
