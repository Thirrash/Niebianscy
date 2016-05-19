using UnityEngine;
using System.Collections;

/***************************************************************
    Script responsible for camera movement.
    It contains camera limitations and transition speeds.
    Other scripts should call its functions
    to safely move camera.

***************************************************************/

public class BattlefieldCameraScript : MonoBehaviour {
	private HexfieldManagerScript HexField;
    public Vector3 limitsMin = new Vector3(0, 0, 0);
    public Vector3 limitsMax = new Vector3(0, 0, 0);
    public float zoomSpeed = 40;
    public float moveSpeed = 10;
    public float rotationSpeed = 50;
	public float pointOfView = 50;

	void Start ()
	{
		ChangePOV ();
		HexField = GameObject.Find ("ManagerScripts").GetComponent<HexfieldManagerScript> ();
		SetLimits ();
	}

	public void Move (float moveX, float moveY, float moveZ)
	{
		float limitAddition = 0.23f * transform.position.y;
		float rotY = transform.eulerAngles.y * Mathf.PI / 180;
		Vector3 translation = new Vector3 (
			moveSpeed * Time.deltaTime * (moveX * Mathf.Cos(rotY) + moveZ * Mathf.Sin(rotY)), 
			0,
			moveSpeed * Time.deltaTime * (-moveX * Mathf.Sin(rotY) + moveZ * Mathf.Cos(rotY)));
		transform.Translate (translation, Space.World); // x and z axes
		transform.Translate (-Vector3.forward * moveY * zoomSpeed * Time.deltaTime); // zoom

		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, limitsMin.x - limitAddition, limitsMax.x + limitAddition),
			Mathf.Clamp (transform.position.y, limitsMin.y, limitsMax.y),
			Mathf.Clamp (transform.position.z, limitsMin.z - limitAddition, limitsMax.z + limitAddition));

	}

	public void Rotate (float rotation)
	{
		float rotationAngle = rotation * rotationSpeed * Time.deltaTime;
		transform.Rotate (Vector3.up, rotationAngle, Space.World);
	}
		
	public void ChangePOV ()
	{
		transform.eulerAngles = new Vector3 (pointOfView, transform.rotation.y, 0);
	}

	public void SetLimits ()
	{
		limitsMin.Set (-4.5f, 5f, -4.5f);
		limitsMax.Set (
			1.5f * (HexField.vMax + 2), 
			0.8f * ((HexField.vMax > HexField.wMax) ? HexField.vMax : HexField.wMax),
			1.73f * (HexField.wMax + 2));
	}

	public void CenterToHex (HexScript Hex)
	{
		Vector3 direction = new Vector3 (
			                    Hex.GetPosition ().x - transform.position.x,
			                    0,
			                    Hex.GetPosition ().z - transform.position.z);
		direction = direction * (1f - transform.position.y / Mathf.Tan (pointOfView * Mathf.PI / 180f) / direction.magnitude);
		transform.Translate (direction, Space.World);
		transform.LookAt (Hex.GetPosition ());
	}
}
