using UnityEngine;
using System.Collections;

//Script responsible for camera movement using WSAD keys during battle
public class CameraBattleMovement : MonoBehaviour 
{
	public Transform MainCamera;
	public float CameraSpeed;
	public float ZoomSpeed;
	private Vector3 CameraPosition;

	void Start () 
	{
		CameraPosition = MainCamera.position;
	}


	void Update () 
	{
		//minuses here and there because axis vector direction is different; nothing to worry about
		CameraPosition.x -= Input.GetAxis ("Vertical") * CameraSpeed;
		CameraPosition.y -= Input.GetAxis ("Mouse ScrollWheel") * ZoomSpeed; 
		CameraPosition.z += Input.GetAxis ("Horizontal") * CameraSpeed;

		//camera limits
		if (CameraPosition.x > 50.0f)	CameraPosition.x = 50.0f;
		if (CameraPosition.x < 20.0f)	CameraPosition.x = 20.0f;
		if (CameraPosition.y > 31.0f)	CameraPosition.y = 31.0f;
		if (CameraPosition.y < 7.0f)	CameraPosition.y = 7.0f;
		if (CameraPosition.z > 43.0f)	CameraPosition.z = 43.0f;
		if (CameraPosition.z < 20.0f)	CameraPosition.z = 20.0f;

		//save transform

		MainCamera.position = CameraPosition;
	}

}
