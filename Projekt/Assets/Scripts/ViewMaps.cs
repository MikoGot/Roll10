using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMaps : MonoBehaviour {
	public GameObject viewMapsWindow;


	
	public void OpenWindow()
	{
		viewMapsWindow.SetActive(true);
	}
	public void CloseWindow()
	{
		viewMapsWindow.SetActive(false);
	}
	public void OpenAndCloseWindow()
	{
		viewMapsWindow.active = !viewMapsWindow.active;
	}

}
