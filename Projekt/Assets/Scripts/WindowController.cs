using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour {
	public GameObject noteImageMenuSelection;
	public GameObject deleteAskWindow;
	public GameObject leavingAskWindow;
	public GameObject createNoteMenu;
	public GameObject toolsMenu;
	public GameObject titleBar;

	public void OpenNoteImageSelection()
	{
		noteImageMenuSelection.SetActive(true);
	}

	public void CloseNoteImageSelection()
	{
		noteImageMenuSelection.SetActive(false);
	}

	public void OpenDeleteAskWindow()
	{
		deleteAskWindow.SetActive(true);
	}

	public void CloseDeleteAskWindow()
	{
		deleteAskWindow.SetActive(false);
	}

	public void OpenLeavingAskWindow()
	{
		leavingAskWindow.SetActive(true);
	}

	public void CloseLeavingAskWindow()
	{
		leavingAskWindow.SetActive(false);
	}

	public void OpenCreateNoteMenu()
	{
		createNoteMenu.SetActive(true);
	}

	public void CloseCreateNoteMenu()
	{
		createNoteMenu.SetActive(false);
	}

	public void OpenToolsMenu()
	{
		toolsMenu.SetActive(true);
		titleBar.SetActive(true);
	}
}
