using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {
	public int index;
	public int id_note;
	public string category;
	public string visibility;
	public string noteName;
	public string description;
	public string history;
	public int imageId;
	public string noteOwner;

	public void Debbuging()
	{
		Debug.Log(index + ", " + id_note
		+ ", " + category + ", " + visibility
		+ ", " + noteName + ", " + description
		+ ", " + history + ", " + imageId
		+ ", " + noteOwner);
	}
}
