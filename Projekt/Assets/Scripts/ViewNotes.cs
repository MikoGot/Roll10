using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ViewNotes : MonoBehaviour {
	public List<Note> notes = new List<Note>();
	public List<GameObject> elements = new List<GameObject>();
	public ImageSelector imageSelector;
	public GameObject parentOfElements;
	public Notification notification;
	bool firstCheck;
	bool secondCheck;

	public void CallViewNote()
	{
		StartCoroutine(CreateViewC());
	}

	IEnumerator CreateViewC()
	{
		ClearList();

		WWWForm form = new WWWForm();
		form.AddField("nickname", DBManager.nickname);
		form.AddField("cookie", DBManager.cookie);

		string url = "http://localhost:8888/sqlconnect/viewnotes.php";
		WWW www = new WWW(url, form);

		yield return www;



		if(www.text[0] == '0')
		{
			int howManyRows = int.Parse(www.text.Split('\n')[1]);
			Debug.Log("Number of rows: " + howManyRows);
			Debug.Log("Success: " + www.text);

			for(int i = 0; i < howManyRows; i++)
			{
				Note note = new Note();
				note.index = i+1;
				int.TryParse(www.text.Split('\t')[1+i*8], out note.id_note);
				note.category = www.text.Split('\t')[2+i*8];
				note.visibility = www.text.Split('\t')[3+i*8];
				note.noteName = www.text.Split('\t')[4+i*8];
				note.description = www.text.Split('\t')[5+i*8];
				note.history = www.text.Split('\t')[6+i*8];
				int.TryParse(www.text.Split('\t')[7+i*8], out note.imageId);
				note.noteOwner = www.text.Split('\t')[8+i*8];
				notes.Add(note);
				note.Debbuging();
			}
			
			CompleteList();
			notification.NotificationAction("Notes loaded", notification.positiveS);
		}
		else if(www.text == null || www.text == "")
		{
			Debug.Log("Is null or empty " + www.text);
		}
		else{
			Debug.Log("Error #" + www.text);
			notification.NotificationAction("Loading notes failed", notification.negativeS);
		}
	}

	public void CallAllViewNote()
	{
		StartCoroutine(CreateAllViewC());
	}

	IEnumerator CreateAllViewC()
	{
		ClearList();
		
		WWWForm form = new WWWForm();
		form.AddField("nickname", DBManager.nickname);
		form.AddField("cookie", DBManager.cookie);

		string url = "http://localhost:8888/sqlconnect/viewallnotes.php";
		WWW www = new WWW(url, form);

		yield return www;

		if(www.text[0] == '0')
		{
			int howManyRows = int.Parse(www.text.Split('\n')[1]);
			Debug.Log("Number of rows: " + howManyRows);
			Debug.Log("Success: " + www.text);

			for(int i = 0; i < howManyRows; i++)
			{
				Note note = new Note();
				note.index = i+1;
				int.TryParse(www.text.Split('\t')[1+i*8], out note.id_note);
				note.category = www.text.Split('\t')[2+i*8];
				note.visibility = www.text.Split('\t')[3+i*8];
				note.noteName = www.text.Split('\t')[4+i*8];
				note.description = www.text.Split('\t')[5+i*8];
				note.history = www.text.Split('\t')[6+i*8];
				int.TryParse(www.text.Split('\t')[7+i*8], out note.imageId);
				note.noteOwner = www.text.Split('\t')[8+i*8];
				notes.Add(note);
				note.Debbuging();
			}
			
			CompleteList();
			notification.NotificationAction("Notes loaded", notification.positiveS);
		}
		else if(www.text == null || www.text == "")
		{
			Debug.Log("Is null or empty " + www.text);
		}
		else{
			Debug.Log("Error #" + www.text);
			notification.NotificationAction("Loading notes failed", notification.negativeS);
		}
	}

	void CompleteList()
	{
		foreach(Note note in notes)
		{
			GameObject go = Instantiate(Resources.Load("ViewNoteElement", typeof(GameObject))) as GameObject;
			go.transform.SetParent(parentOfElements.transform, false);
			go.transform.Find("IdText").GetComponent<TextMeshProUGUI>().SetText(note.index.ToString());
			go.transform.Find("Image").GetComponent<Image>().sprite = imageSelector.images[note.imageId - 1].transform.GetChild(0).GetComponent<Image>().sprite;
			go.transform.Find("NoteNameText").GetComponent<TextMeshProUGUI>().SetText(note.noteName);
			go.transform.Find("CategoryText").GetComponent<TextMeshProUGUI>().SetText(note.category);
			go.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().SetText(note.description);
			go.transform.Find("OwnerText").GetComponent<TextMeshProUGUI>().SetText(note.noteOwner);
			elements.Add(go);
		}
	}

	void ClearList()
	{
		if(notes != null && elements != null)
		{
			for(int i = 0; i < notes.Count; i++)
			{
				Debug.Log(i);
				Debug.Log("Usuwam " + elements[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
				Destroy(elements[i]);
			}
			elements.Clear();
			notes.Clear();
		}
	}
}
