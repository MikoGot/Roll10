using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageSelector : MonoBehaviour {
	public GameObject content;
	public GameObject imageInCreateNoteMenu;
	public int currentSelected;
	public List<GameObject> images = new List<GameObject>();

	void Start()
	{
		int i = 0;
		foreach(Transform child in content.gameObject.transform)
		{
			images.Add(content.gameObject.transform.GetChild(i).gameObject);
			images[i].AddComponent<ImageView>();
			ImageView imageView = images[i].GetComponent<ImageView>();
			imageView.imageSelector = GameObject.Find("NotesController").GetComponent<ImageSelector>();
			imageView.imageId = i+1;
			i++;
		}
	}

	public void AssignImage()
	{
		imageInCreateNoteMenu.GetComponent<Image>().sprite = images[currentSelected-1].transform.GetChild(0).GetComponent<Image>().sprite;
	}
}