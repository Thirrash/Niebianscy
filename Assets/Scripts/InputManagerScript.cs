using UnityEngine;
using System.Collections;

/***************************************************************
    It is supposed to be the only script interfering with
    user input. It should call propper managers and call
    appropriate functions with needed data in them,
    depending on user actions.

    Current usage:
    wsad, qe, arrows - camera movements
    left click - select unit or tile
    space - move (if selected unit is allied and selected tile in range)
    enter - next turn

***************************************************************/
public class InputManagerScript : MonoBehaviour {
    BattlefieldCameraScript cameraMan;
    UnitManagerScript unitMan;

	void Start () {
        unitMan = GetComponent<UnitManagerScript>();
    }
	
	// Update is called once per frame
	void Update () {
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
        if (Input.GetKeyUp("escape"))
            unitMan.Deselect();
        if (Input.GetKeyUp("return"))
        {
            unitMan.EndTurn();
            Debug.Log("New turn started! Resources refreshed.");
        }
    }
}
