using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateNote : MonoBehaviour {
	public ImageSelector imageSelector;
	public TMP_InputField noteNameField;
	public Dropdown categoryDropdown;
	public TMP_InputField descriptionField;
	public TMP_InputField historyField;
	public Toggle isPublicToogle;
	string isPublicFlagText = " ";
	public Notification notification;
	public void CallCreateNote()
	{
		StartCoroutine(CreateNoteC());
	}

	IEnumerator CreateNoteC()
	{
		WWWForm form = new WWWForm();
		form.AddField("nickname", DBManager.nickname);
		form.AddField("imageId", imageSelector.currentSelected);
		form.AddField("noteName", noteNameField.text);
		form.AddField("category", categoryDropdown.options[categoryDropdown.value].text);
		form.AddField("description", descriptionField.text);
		form.AddField("history", historyField.text);
		AssignProperTextForPublicFlagText();
		form.AddField("visibility", isPublicFlagText);
		form.AddField("cookie", DBManager.cookie);

		string url = "http://localhost:8888/sqlconnect/createnote.php";
		WWW www = new WWW(url, form);

		yield return www;
		
		if(www.text == "0")
		{
			Debug.Log("Note created successfully.");
			notification.NotificationAction("Note created", notification.positiveS);
		}
		else if(www.text == "1")
		{
			Debug.Log("Note updated successfully.");
		}
		else if(www.text == null || www.text == "")
		{
			Debug.Log("Is null or empty " + www.text);
		}
		else{
			Debug.Log("Note creation failed. Error #" + www.text);
		}
	}

	void AssignProperTextForPublicFlagText()
	{
		if(isPublicToogle.isOn) isPublicFlagText = "PUBLIC";
		else if(!isPublicToogle.isOn) isPublicFlagText = "PRIVATE";
	}
}
