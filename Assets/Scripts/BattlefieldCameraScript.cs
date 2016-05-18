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
// TO DO Implement functions:
//      CenterHex - to move directly to given hex

public class BattlefieldCameraScript : MonoBehaviour {
    public Vector3 limitsMin = new Vector3(0, 0, 0);
    public Vector3 limitsMax = new Vector3(0, 0, 0);
    public float zoomSpeed = 40;
    public float moveSpeed = 10;
    public float rotationSpeed = 50;
	public float pointOfView = 50;

	void Start ()
	{
		ChangePOV ();
	}

	public void Move (float moveX, float moveY, float moveZ)
	{
		float rotY = transform.eulerAngles.y * Mathf.PI / 180;
		Vector3 translation = new Vector3 (
			moveSpeed * Time.deltaTime * (moveX * Mathf.Cos(rotY) + moveZ * Mathf.Sin(rotY)), 
			moveY * zoomSpeed * Time.deltaTime,
			moveSpeed * Time.deltaTime * (-moveX * Mathf.Sin(rotY) + moveZ * Mathf.Cos(rotY)));

		transform.Translate (translation, Space.World);
	}

	public void Rotate (float rotation)
	{
		float rotationAngle = rotation * rotationSpeed * Time.deltaTime;
		transform.Rotate (Vector3.up, rotationAngle, Space.World);
	}
		
	public void ChangePOV ()
	{
		transform.Rotate (new Vector3 (pointOfView - transform.eulerAngles.x, 0, 0), Space.World);
	}
}
