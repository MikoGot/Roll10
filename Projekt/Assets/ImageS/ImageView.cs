using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ImageView : MonoBehaviour, IPointerClickHandler {
	public int imageId;
	public ImageSelector imageSelector;
	public bool isClicked;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
		imageSelector.currentSelected = imageId;
		isClicked = !isClicked;
		
		if(imageSelector.currentSelected == imageId && isClicked)
		{
			GetComponent<Image>().color = new Color32(255,255,0,255);
			isClicked = !isClicked;
		} 
		else
		{
			GetComponent<Image>().color = new Color32(255,255,255,255);
			imageSelector.currentSelected = 0;
		}
    }
	void Update()
	{
		if(!(imageSelector.currentSelected == imageId)) GetComponent<Image>().color = new Color32(255,255,255,255);
	}
}