using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateMapToolbar : MonoBehaviour {
	public GameObject toolbarWindow;
	public TMP_InputField mapNameInput;
	public TMP_InputField mapDescriptionField;
	public TMP_Dropdown maxXdropdown;
	public TMP_Dropdown maxYdropdown;

	public void OpenWindow()
	{
		toolbarWindow.SetActive(true);
	}

	public void CloseWindow()
	{
		toolbarWindow.SetActive(false);
	}

	public void OpenAndCloseWindow()
	{
		toolbarWindow.active = !toolbarWindow.active;
	}
}
