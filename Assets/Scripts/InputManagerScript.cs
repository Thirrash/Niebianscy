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

	void Start () {
        unitMan = GetComponent<UnitManagerScript>();
		cameraMan = GameObject.Find ("Battlefield Camera").GetComponent<BattlefieldCameraScript> ();
    }

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

		//camera input
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Mouse ScrollWheel");
		float moveZ = Input.GetAxis ("Vertical");
		float rotation = Input.GetAxis ("Rotational");
		if (moveX != 0f || moveY != 0f || moveZ != 0f)
			cameraMan.Move (moveX, moveY, moveZ);
		if (rotation != 0f)
			cameraMan.Rotate (rotation);
	}
}
