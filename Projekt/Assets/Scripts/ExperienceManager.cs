using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceManager : MonoBehaviour {
	public GameController gameController;
	public TextMeshProUGUI playerDisplay;
	public TextMeshProUGUI scoreDisplay;

	public void CallSaveData()
	{
		if(DBManager.cookie != null){
			Debug.Log("User logged in, continue...");
			StartCoroutine(SavePlayerData());
		}
		else{
			Debug.Log("User not logged in");
		}
	}

	public void DisplayScoreAndPlayer()
	{
		playerDisplay.SetText(DBManager.nickname);
		scoreDisplay.SetText(DBManager.score.ToString());
	}

	IEnumerator SavePlayerData()
	{
		WWWForm form = new WWWForm();
		form.AddField("nickname", DBManager.nickname);
		form.AddField("score", DBManager.score);
		form.AddField("cookie", DBManager.cookie);

		WWW www = new WWW("http://localhost:8888/sqlconnect/experienceup.php", form);
		yield return www;

		if(www.text[0] == '0')
		{
			DBManager.score = int.Parse(www.text.Split('\t')[1]);

			Debug.Log("Reply: " + www.text);
			DisplayScoreAndPlayer();
		}
		else{
			Debug.Log("User login failed. Error #" + www.text);
		}
	}
}
