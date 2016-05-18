using UnityEngine;
using System.Collections;

/***************************************************************
    It is supposed to be the only script interfering with
    user input. It should call propper managers and call
    appropriate functions with needed data in them,
    depending on user actions.

***************************************************************/
public class InputManagerScript : MonoBehaviour {
    BattlefieldCameraScript cameraMan;
    UnitManagerScript unitMan;
	// Use this for initialization
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
	}
}
