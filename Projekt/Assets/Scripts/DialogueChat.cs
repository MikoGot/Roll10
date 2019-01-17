using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueChat : MonoBehaviour {
	public GameObject chatObj;
	public GameObject parent;
	public List<GameObject> chatLines = new List<GameObject>();

	public void AddLineToChat(string newText)
	{
		GameObject go = Instantiate(Resources.Load("ChatLine", typeof(GameObject))) as GameObject;
		go.transform.SetParent(parent.transform, false);
		go.GetComponent<TextMeshProUGUI>().SetText(newText);
		chatLines.Add(go);
	}

	public void ClearChat()
	{

	}

	public void SaveChatToFile()
	{

	}

	public void openChat()
	{
		chatObj.SetActive(true);
	}

	public void closeChat()
	{
		chatObj.SetActive(false);
	}

	public void OpenCloseChat()
	{
		chatObj.active = !chatObj.active;
	}

}
