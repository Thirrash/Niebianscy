using UnityEngine;
using System.Collections;
using System;

public class MovementCheck : MonoBehaviour 
{
	public HighlightGameobject Highlight;
	private GameObject ClickedObject,ClickedParentObject, LastClickedUnit;
	private bool isUnitSelected;
	private int unitSpeed;
	private int curRow, curCol;
	private float curX, curZ;
	private Collider[] AvailableHex;
	private bool isHexHighlighted;
	private bool isHexInRange;

	void Start () 
	{
		isUnitSelected = false;
		isHexInRange = false;
		isHexHighlighted = false;
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			ClickedObject = Highlight.HighlightedObject;
			ClickedParentObject = Highlight.HighlightedParentObject;
			Debug.Log ("Click! " + ClickedObject.name + " child of " + ClickedParentObject.name);

			if (ClickedObject.tag == "Unit") {
				isUnitSelected = true;
				LastClickedUnit = ClickedObject;
				unitSpeed = ClickedObject.GetComponent<UnitStats> ().Speed;
				curX = ClickedObject.transform.position.x;
				curZ = ClickedObject.transform.position.z;
				curRow = ClickedObject.GetComponent<UnitStats> ().CurrentRow;
				curCol = ClickedObject.GetComponent<UnitStats> ().CurrentCol;

				//return colliders of all hexes within range; 256 means that Hex Layer will only be checked
				AvailableHex = Physics.OverlapSphere(new Vector3(curX,0.0f,curZ), (1.73205f - 0.15f)*unitSpeed, 256);
				isHexHighlighted = true;

				for (int i = 0; i < AvailableHex.Length; i++) 
				{
					Collider tmpCollider = AvailableHex [i];
					//Debug.Log ("Trying");
					GameObject gmo = tmpCollider.gameObject;
					gmo.GetComponent<MeshRenderer> ().material.color = new Color(0.5f,0.5f,0.0f,0.0f);
				}
			}
			else 
			{
				if (isHexHighlighted) 
				{
					for (int i = 0; i < AvailableHex.Length; i++) 
					{
						Collider tmpCollider = AvailableHex [i];
						GameObject gmo = tmpCollider.gameObject;
						gmo.GetComponent<MeshRenderer> ().material.color = Color.white;
					}
				}

				if (ClickedObject.tag == "Hex" && isUnitSelected) 
				{
					isHexInRange = Array.Exists (AvailableHex, tmp => tmp == ClickedObject.GetComponent<Collider>());
					if (isHexInRange) 
					{
						Debug.Log ("In range -> moving unit " + LastClickedUnit.name);
						LastClickedUnit.transform.Translate 
							(ClickedObject.transform.position.x - LastClickedUnit.transform.position.x,
							ClickedObject.transform.position.y - LastClickedUnit.transform.position.y,
							ClickedObject.transform.position.z - LastClickedUnit.transform.position.z);
					}
				}
			}
		}
	}
}
