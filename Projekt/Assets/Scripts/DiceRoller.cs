using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour {
	public DialogueChat chat;
	public GameObject diceWindow;
	public Dropdown dd_diceCount;
	public Dropdown dd_diceType;
	System.Random rand = new System.Random();

	int Roll()
	{
		int count;
		System.Int32.TryParse(dd_diceCount.options[dd_diceCount.value].text, out count);
		switch (dd_diceType.options[dd_diceType.value].text)
        {
            case "D6":
				return rand.Next(count*1, count*6);
            case "D10":
				return rand.Next(count*1, count*10);
            case "D12":
				return rand.Next(count*1, count*12);
            case "D20":
				return rand.Next(count*1, count*20);
            default:
                return 0;
		}
	}

	public void RollAndShow()
	{
		int result = Roll();
		chat.openChat();
		chat.AddLineToChat("<"+DBManager.nickname+"> rolled " + dd_diceType.options[dd_diceType.value].text + ". Result: " + result + ".");
	}

	
	public void OpenCloseRollWindow()
	{
		diceWindow.active = !diceWindow.active;
	}

}
