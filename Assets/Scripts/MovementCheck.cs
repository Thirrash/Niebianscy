using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovementCheck : MonoBehaviour 
{
	public HighlightGameobject Highlight;
	private GameObject ClickedObject,ClickedParentObject, LastClickedUnit;
	private GameObject ClickedHex;
	private bool isUnitSelected;
	private int unitSpeed;
	private float curX, curZ;
	private Collider[] AvailableHex = {new Collider()};
	private List <GameObject> TrueAvailableHexes = new List<GameObject>(1);
	private int TrueHexesLength;
	private GameObject[] Units;
	private GameObject[] Obstacles;
	private bool isHexInRange;
	private bool isTaken;

	void Start () 
	{
		isUnitSelected = false;
		isHexInRange = false;
		TrueHexesLength = 0;

		Units = GameObject.FindGameObjectsWithTag ("Unit");
		//Obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			ClickedObject = Highlight.HighlightedObject;
			ClickedParentObject = Highlight.HighlightedParentObject;
			Debug.Log ("Click! " + ClickedObject.name + " child of " + ClickedParentObject.name);

			//return all hexes to unhighlighted state
			if (AvailableHex.Length != 1)	ChangeMat (1.0f, 1.0f, 1.0f);

			if (ClickedObject.tag == "Unit") 
			{
				isUnitSelected = true;
				LastClickedUnit = ClickedObject;
				unitSpeed = ClickedObject.GetComponent<UnitStats> ().Speed;
				curX = ClickedObject.transform.position.x;
				curZ = ClickedObject.transform.position.z;
				ClickedHex = ClickedObject.GetComponent<UnitStats> ().HexPos;

				//return colliders of all hexes within range; 256 means that Hex Layer will only be checked
				AvailableHex = Physics.OverlapSphere(new Vector3(curX,0.0f,curZ), (1.73205f - 0.15f)*unitSpeed, 256);
				TrueAvailableHexes.Clear();
				TrueHexesLength = 0;

				for (int i = 0; i < AvailableHex.Length; i++) 
				{
					isTaken = false;

					for (int j = 0; j < Units.Length; j++) 
					{
						if (AvailableHex[i].gameObject.Equals (Units[j].GetComponent<UnitStats>().HexPos))
							isTaken = true;
					}

					if (!isTaken) 
					{
						TrueAvailableHexes.Add (AvailableHex[i].gameObject);
						TrueHexesLength++;
					}
				}

				//highlight hexes
				ChangeMat (0.9f,0.9f,0.0f);
				Debug.Log ("Number of available hexes: " + AvailableHex.Length);
			}
			else 
			{
				if (ClickedObject.tag == "Hex" && isUnitSelected) 
				{
					isHexInRange = TrueAvailableHexes.Exists (tmp => tmp == ClickedObject);
					//isHexInRange = Array.Exists (TrueAvailableHexes, tmp => tmp == ClickedObject.GetComponent<Collider>());
					if (isHexInRange) 
					{
						Debug.Log ("In range -> moving unit " + LastClickedUnit.name);
						LastClickedUnit.transform.Translate 
							(ClickedObject.transform.position.x - LastClickedUnit.transform.position.x,
							ClickedObject.transform.position.y - LastClickedUnit.transform.position.y,
							ClickedObject.transform.position.z - LastClickedUnit.transform.position.z);
						LastClickedUnit.GetComponent<UnitStats> ().HexPos = ClickedObject;
					}
				}
			}
		}
	}

	void ChangeMat(float r, float g, float b)
	{
		for (int i = 0; i < TrueHexesLength; i++) 
		{
			//Debug.Log ("Trying");
			TrueAvailableHexes [i].GetComponent<MeshRenderer> ().material.color = new Color(r,g,b,0.0f);
		}				
	}
}
