using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighlightGameobject : MonoBehaviour 
{
	public Canvas canvas;
	private Text text;
	public Camera MainCamera;
	public GameObject ClickedObject, ClickedParentObject;
	private GameObject HighlightedObject, HighlightedTemp, HighlightedParentObject;
	private Material ObjectMat;
	private Color tempColor;
	private Renderer rend;
	private Ray ray;
	private RaycastHit hit;
	private bool isMaterialChanged;
	private UnitStats stats;
	private string statText;

	void Start() 
	{
		canvas.enabled = false;
		isMaterialChanged = false;
		text = canvas.transform.FindChild("Text").GetComponent<Text>();
	}

	void Update()
	{
		ray = MainCamera.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, 100))
		{
			HighlightedObject = hit.transform.gameObject;
			HighlightedParentObject = HighlightedObject.transform.parent.gameObject;
			//Debug.Log ("Highlighted object - " + HighlightedParentObject.name + " / " + HighlightedObject.name);

			if (isMaterialChanged) 
			{
				HighlightedTemp.GetComponent<Renderer>().material.color = tempColor;
				isMaterialChanged = false;
			}

			if (HighlightedObject.tag == "Hex") 
			{
				isMaterialChanged = true;
				rend = HighlightedObject.GetComponent<Renderer>();
				ObjectMat = rend.material;
				tempColor = ObjectMat.color;
				ObjectMat.color = Color.red;
			}

			HighlightedTemp = HighlightedObject;
		}

		if (HighlightedObject.tag == "Unit") 
		{
			stats = HighlightedObject.GetComponent<UnitStats> ();
			if (Input.GetMouseButtonDown(1)) 
			{
				statText = "Type: " + stats.UnitType + "\nHP: " + stats.CurrentHP + "/20";
				statText += "\nAttack: " + stats.Attack + "\nDefense: " + stats.Defense;
				statText += "\nSpeed: " + stats.Speed + "\nRange: " + stats.Range;
				text.text = statText;
				canvas.enabled = true;
			}
		}

		if (canvas.enabled && Input.GetMouseButtonUp(1))	canvas.enabled = false;

		if (Input.GetMouseButtonDown (0)) 
		{
			ClickedObject = HighlightedObject;
			ClickedParentObject = HighlightedParentObject;
			Debug.Log ("Click! " + ClickedObject);
		}

	}
}
