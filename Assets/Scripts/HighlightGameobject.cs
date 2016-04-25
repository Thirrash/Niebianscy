using UnityEngine;
using System.Collections;

public class HighlightGameobject : MonoBehaviour 
{
	public Camera MainCamera;
	public GameObject ClickedObject, ClickedParentObject;
	private GameObject HighlightedObject, HighlightedTemp, HighlightedParentObject;
	private Material ObjectMat;
	private Color tempColor;
	private Renderer rend;
	private Ray ray;
	private RaycastHit hit;
	private bool isMaterialChanged;

	void Start() 
	{
		isMaterialChanged = false;
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

		if (Input.GetMouseButtonDown (0)) 
		{
			ClickedObject = HighlightedObject;
			ClickedParentObject = HighlightedParentObject;
			Debug.Log ("Click! " + ClickedObject);
		}
	}
}
