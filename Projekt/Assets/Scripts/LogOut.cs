using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOut : MonoBehaviour {
	public Notification notification;

	public void CallLogOut()
	{
		StartCoroutine(LogOutPlayer());
	}

	IEnumerator LogOutPlayer()
	{
		WWWForm form = new WWWForm();
		form.AddField("nickname", DBManager.nickname);
		form.AddField("cookie", DBManager.cookie);

		string url = "http://localhost:8888/sqlconnect/logout.php";
		WWW www = new WWW(url, form);

		yield return www;
		
		if(www.text[0] == '0')
		{
			string logInInfo = "Log out successfull.";
			notification.NotificationAction(logInInfo, notification.positiveS);
		}
		else{
			Debug.Log("User log our failed. Error #" + www.text);
			notification.NotificationAction("User log out failed.", notification.negativeS);
		}
	}
}
