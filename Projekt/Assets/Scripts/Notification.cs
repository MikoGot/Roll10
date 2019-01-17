using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Notification : MonoBehaviour {
	public GameObject obj;
	public Image notificationImage;
	public Sprite positiveS;
	public Sprite negativeS;
	public Sprite warningS;
	public TextMeshProUGUI notificationText;
	public Animator animator;

	public void NotificationAction(string newString, Sprite newSprite)
	{
		notificationText.SetText(newString);
		notificationImage.sprite = newSprite;
		StartCoroutine(SlowlyDisappear());
	}

	IEnumerator SlowlyDisappear()
	{
		animator.SetTrigger("notOff");
		yield return new WaitForSecondsRealtime(5f);
		obj.SetActive(false);
	}

	public void CloseImmiediete()
	{
		obj.SetActive(false);
	}
}
