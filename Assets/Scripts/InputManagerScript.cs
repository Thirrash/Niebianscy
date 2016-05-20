using UnityEngine;
using System.Collections;

/***************************************************************
    It is supposed to be the only script interfering with
    user input. It should call propper managers and call
    appropriate functions with needed data in them,
    depending on user actions.

    Current usage:
    wsad, arrows, edge of screen - camera movement
    qe - camera rotation
    middle mouse button - center camera to selected unit's hex
    scroll wheel - camera zoom in/out
    left click - select unit or tile
    space - move (if selected unit is allied and selected tile in range)
    enter - next turn

***************************************************************/
public class InputManagerScript : MonoBehaviour 
{
    BattlefieldCameraScript cameraMan;
    UnitManagerScript unitMan;

	void Start () 
	{
        unitMan = GetComponent<UnitManagerScript>();
		cameraMan = GameObject.Find ("Battlefield Camera").GetComponent<BattlefieldCameraScript> ();
    }

	void Update () 
	{
	    if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.transform.gameObject.tag == "Unit")
                    unitMan.SelectUnit(hitInfo.transform.gameObject.GetComponent<UnitScript>());
                if (hitInfo.transform.gameObject.tag == "Hex")
                    unitMan.SelectHex(hitInfo.transform.gameObject.GetComponent<HexScript>());
            }
        }
        if (Input.GetKeyUp("space"))
            unitMan.Move();

		//camera input
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Mouse ScrollWheel");
		float moveZ = Input.GetAxis ("Vertical");
		float rotation = Input.GetAxis ("Rotational");
		if (moveX != 0f || moveY != 0f || moveZ != 0f)
			cameraMan.Move (moveX, moveY, moveZ);
		if (rotation != 0f)
			cameraMan.Rotate (rotation);
		
		//center camera to selected unit on Middle Mouse Button click
		if (Input.GetMouseButton (2))
			cameraMan.CenterToHex (unitMan.selectedUnit.hex);

		//camera movement when mouse on screen edge
		if (Input.mousePosition.x < (Screen.width / 40))
			cameraMan.Move (-1f, 0f, 0f);
		else if (Input.mousePosition.x > (Screen.width - Screen.width / 40))
			cameraMan.Move (1f, 0f, 0f);
		if (Input.mousePosition.y < (Screen.height / 40))
			cameraMan.Move (0f, 0f, -1f);
		else if (Input.mousePosition.y > (Screen.height - Screen.height / 40))
			cameraMan.Move (0f, 0f, 1f);

        if (Input.GetKeyUp("escape"))
            unitMan.Deselect();
        if (Input.GetKeyUp("return"))
        {
            unitMan.EndTurn();
            Debug.Log("New turn started! Resources refreshed.");
        }
    }
}
