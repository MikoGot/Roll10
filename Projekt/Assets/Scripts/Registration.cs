using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Registration : MonoBehaviour {
	public GameController gameController;
	public TMP_InputField emailField;
	public TMP_InputField nicknameField;
	public TMP_InputField passwordField;

	public Button submitButton;

	public void CallRegister()
	{
		StartCoroutine(Register());
	}

	IEnumerator Register()
	{
		WWWForm form = new WWWForm();
		form.AddField("email", emailField.text);
		form.AddField("nickname", nicknameField.text);
		form.AddField("password", passwordField.text);

		string url = "http://localhost:8888/sqlconnect/register.php";
		WWW www = new WWW(url, form);

		yield return www;
		
		if(www.text == "0")
		{
			Debug.Log("User created successfully.");
			gameController.GoToMainMenu();
		}
		else if(www.text == null || www.text == "")
		{
			Debug.Log("Is null or empty " + www.text);
		}
		else{
			Debug.Log("User creation failed. Error #" + www.text);
		}
	}

	public void VerifyInputs()
	{
		submitButton.interactable = (emailField.text.Length >= 8 && nicknameField.text.Length >= 8 && passwordField.text.Length >= 8);
	}
}
