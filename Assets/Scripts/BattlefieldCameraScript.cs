using UnityEngine;
using System.Collections;

/***************************************************************
    Script responsible for camera movement.
    It contains camera limitations and transition speeds.
    Other scripts should call its functions
    to safely move camera.

***************************************************************/

// TO DO Implement dynamic limits (dependent on number of hexes)
// TO DO Implement camera movement when mouse is on screen edge
// TO DO Move most of this to Input Manager
// TO DO Implement functions:
//      CenterHex - to move directly to given hex
//      Move - to move in direction given as vector
//      Rotate - to rotate

public class BattlefieldCameraScript : MonoBehaviour {
    public Vector3 limitsMin = new Vector3(0, 0, 0);
    public Vector3 limitsMax = new Vector3(0, 0, 0);
    public float zoomSpeed = 40;
    public float moveSpeed = 10;
    public float rotationSpeed = 20;
    
	
	void LateUpdate () {
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float y = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float r = Input.GetAxis("Rotational") * rotationSpeed * Time.deltaTime;

        transform.Translate(new Vector3(x, y, z), Space.World);
        transform.Rotate(Vector3.up, r, Space.World);
    }
}
