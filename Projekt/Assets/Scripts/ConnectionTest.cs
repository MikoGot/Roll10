using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTest : MonoBehaviour {	
	public void CallTesting()
	{
		StartCoroutine(Testing());
	}

	IEnumerator Testing()
	{
		WWWForm form = new WWWForm();
		form.AddField("email", "testowy123");
		form.AddField("nickname", "testowy123");
		form.AddField("password", "testowy123");

		string url = "http://localhost:8888/sqlconnect/test.php";
		WWW www = new WWW(url, form);

		yield return www;
	
		if(www.text == null || www.text == "")
		{
			Debug.Log("Is null or empty " + www.text);
		}
		else
		{
			Debug.Log("Its replying. Anwser: " + www.text);
		}
	}
}
