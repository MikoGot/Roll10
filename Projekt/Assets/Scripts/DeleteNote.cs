using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DeleteNote : MonoBehaviour {
	public TMP_InputField noteNameField;
	public ImageSelector imageSelector;
	public Dropdown categoryDropdown;
	public TMP_InputField descriptionField;
	public TMP_InputField historyField;
	public Notification notification;
	public void CallDeleteNote()
	{
		StartCoroutine(DeleteNoteC());
	}

	IEnumerator DeleteNoteC()
	{
		WWWForm form = new WWWForm();
		form.AddField("nickname", DBManager.nickname);
		form.AddField("noteName", noteNameField.text);
		form.AddField("cookie", DBManager.cookie);

		string url = "http://localhost:8888/sqlconnect/delete.php";
		WWW www = new WWW(url, form);

		yield return www;
		
		if(www.text == "0")
		{
			Debug.Log("Note already deleted or never existed.");
			notification.NotificationAction("Note already deleted/never existed.", notification.negativeS);
		}
		else if(www.text == "1")
		{
			Debug.Log("Note deleted successfully.");
			notification.NotificationAction("Note deleted", notification.positiveS);
		}
		else if(www.text == null || www.text == "")
		{
			Debug.Log("Is null or empty " + www.text);
		}
		else{
			Debug.Log("Testing texts: \n" + www.text);
		}
	}

	public void ClearNoteFields()
	{
		imageSelector.currentSelected = 0;
		noteNameField.text = "";
		categoryDropdown.value = 0;
		descriptionField.text = "";
		historyField.text = "";
	}
}
