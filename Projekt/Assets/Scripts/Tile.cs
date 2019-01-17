using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
	public CreateMap createMap;
	public int x;
	public int y;
	public GameObject stationingPawn;
	bool isTaken;
	public Tile(int i, int j)
	{
		this.x = i;
		this.y = j;
	}

	void Update()
	{
	//	stationingPawn = transform.Find;
	}

	public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("x: " + x + ", y: " + y);

		if(createMap.spawningPawnsMode && (stationingPawn == null))
			createMap.SpawnUnit(this.x, this.y);

		if(stationingPawn != null)
		{
			createMap.SelectPawn(stationingPawn, this.x, this.y);
		}
    }

}

