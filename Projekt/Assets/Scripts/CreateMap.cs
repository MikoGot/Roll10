using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMap : MonoBehaviour {
	public GameObject mapWindow;
	public GameObject parentOfElements;
	public ImageSelector imageSelector;
	public Notification notification;
	public int mapX = 10;
	public int mapY = 10;
	public List<GameObject> tiles = new List<GameObject>();
	public List<GameObject> pawns = new List<GameObject>();
	public GameObject selectedPawn;
	public bool spawningPawnsMode = true;

	public CreateMapToolbar createMapToolbar;
	
	public void CallCreateMap()
	{
		StartCoroutine(CreateMapC());
	}

	public void CallUpdateMap()
	{
		StartCoroutine(UpdateMap());
	}

	IEnumerator CreateMapC()
	{
		WWWForm form = new WWWForm();
		form.AddField("nickname", DBManager.nickname);
		form.AddField("cookie", DBManager.cookie);
		form.AddField("mapName", createMapToolbar.mapNameInput.text);
		form.AddField("description", createMapToolbar.mapDescriptionField.text);
		form.AddField("maxX", createMapToolbar.maxXdropdown.options[createMapToolbar.maxXdropdown.value].text);
		form.AddField("maxY", createMapToolbar.maxYdropdown.options[createMapToolbar.maxYdropdown.value].text);

		string url = "http://localhost:8888/sqlconnect/createmap.php";
		WWW www = new WWW(url, form);

		yield return www;
		
		if(www.text == "0")
		{
			Debug.Log("Map created successfully.");
			notification.NotificationAction("Map created", notification.positiveS);
		}
		else if(www.text == "1")
		{
			Debug.Log("Map updated successfully.");
		}
		else if(www.text == null || www.text == "")
		{
			Debug.Log("Is null or empty " + www.text);
		}
		else{
			Debug.Log("Map creation failed. Error #" + www.text);
			notification.NotificationAction("Map creation failed.", notification.negativeS);
		}
	}

	IEnumerator UpdateMap()
	{
		yield return null;
	}

	public void FillMap()
	{
		System.Int32.TryParse((createMapToolbar.maxXdropdown.options[createMapToolbar.maxXdropdown.value].text), out mapX);
		System.Int32.TryParse((createMapToolbar.maxYdropdown.options[createMapToolbar.maxYdropdown.value].text), out mapY);

		for(int i = 1; i <= mapX; i++)
		{
			for(int j = 1; j <= mapY; j++)
			{
				GameObject go = Instantiate(Resources.Load("Tile", typeof(GameObject))) as GameObject;
				go.transform.SetParent(parentOfElements.transform, false);
				go.GetComponent<Tile>().createMap = this.GetComponent<CreateMap>();
				go.GetComponent<Tile>().x = i;
				go.GetComponent<Tile>().y = j;
				tiles.Add(go);
			}
		}

		SpawnUnit(2, 2);
	}

	public void OpenCloseCreateMapWindow()
	{
		mapWindow.active = !mapWindow.active;
	}

	public void SpawnUnit(int tempX, int tempY)
	{
		GameObject go = Instantiate(Resources.Load("Pawn", typeof(GameObject))) as GameObject;
		go.GetComponent<Image>().sprite = imageSelector.images[0].transform.GetChild(0).GetComponent<Image>().sprite;
		pawns.Add(go);
		foreach(GameObject tileObj in tiles)
		{
			if(tileObj.GetComponent<Tile>().x == tempX && tileObj.GetComponent<Tile>().y == tempY)
			{
				tileObj.GetComponent<Tile>().stationingPawn = go;
				go.transform.SetParent(tileObj.transform, false);
			}
		}
	}

	public void SelectPawn(GameObject stationingPawn, int tempX, int tempY)
	{
		foreach(GameObject tileObj in tiles)
		{
			if(tileObj.GetComponent<Tile>().x == tempX && tileObj.GetComponent<Tile>().y == tempY)
				selectedPawn = stationingPawn;
		}			
	}

	public void MovePawn(int tempX, int tempY)
	{
		if(selectedPawn != null)
		{
			foreach(GameObject tileObj in tiles)
			{
				if(tileObj.GetComponent<Tile>().x == tempX && tileObj.GetComponent<Tile>().y == tempY)
				{
					tileObj.GetComponent<Tile>().stationingPawn = selectedPawn;
					selectedPawn.transform.SetParent(tileObj.transform, false);
				}
			}	
		}
	}
}

