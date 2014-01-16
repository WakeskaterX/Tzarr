using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour {
	
	public float 	cam_angle 		= 0f;								//the ring around the board that the camera rotates on
	public float	cam_radius		= 30f;								//the distance from 0,-,0 (board center)-the radius of the camera circle
	public float	cam_height		= 20f;								//the height above 0,0,0 the camera is situated.
	
	private float 	cam_a_adj		= .5f;								//the amount the camera adjusts the angle on rotation
	private float	cam_r_adj		= 16f;								//the amount the camera adjusts the radius on zoom  (ideally these should match the angle vector)
	private float 	cam_h_adj		= 8f;								//the amount the camera adjusts the height on zoom

	
	public float 	cam_radius_min	= 30f;								//the closest distance to the board
	public float 	cam_radius_max	= 50f;								//the furthest distance from the board
	public float 	cam_height_min	= 20f;								//cam height bounds
	public float 	cam_height_max	= 40f;								//  
	
	public Vector3  look_center		= new Vector3(0f,-4f,0f);			//the vector that the camera locks on
	
	private float	cam_x			= 0f;								//the camera X location
	private float 	cam_y			= 0f;								//the camera Y location
	private float 	cam_z			= 0f;								//the camera Z location
	
	

	void Start () {
		cam_x = transform.position.x;
		cam_y = transform.position.y;
		cam_z = transform.position.z;
	}//endof Start
	
	void Update () {
	  if (Input.GetMouseButton (1))
	  {
	  	if (Input.GetAxis("Mouse X") > 0.02f || Input.GetAxis("Mouse X") < -0.02f)
	  	{
	  		PanCamera();
	  	}
	  	
		if (Input.GetAxis("Mouse Y") > 0.02f || Input.GetAxis("Mouse Y") < -0.02f)
		{
			LiftCamera();
		}
	  }
	  if (Input.GetAxis ("Mouse ScrollWheel") > 0.05f || Input.GetAxis ("Mouse ScrollWheel") < -0.05f)
	  {
	  		ZoomCamera();
	  }
	  
	  
	  CamUpdatePosition();												//updates camera transform.position
	  CamLookCenter();													//locks the camera to look at the center of the board
	  //CamAngleLock();													//keeps the angle within 0 and 360
	}//endof Update
	
	public void PanCamera(){
		//Debug.Log("Moving Camera Right");
		cam_angle += Input.GetAxis("Mouse X") * cam_a_adj;
	}
	
	public void LiftCamera(){
		//Debug.Log ("Tilting Camera");
		cam_height -= Input.GetAxis ("Mouse Y") * cam_h_adj;
	}

	public void ZoomCamera(){
		//Debug.Log ("Zooming Camera");
		
		cam_radius -= Input.GetAxis("Mouse ScrollWheel") * cam_r_adj;
		if (cam_radius > cam_radius_min && cam_radius < cam_radius_max)
		{
			cam_height -= Input.GetAxis("Mouse ScrollWheel") * cam_h_adj;
		}
	}
	
	public void CamUpdatePosition(){
		cam_height = Mathf.Clamp(cam_height,cam_height_min,cam_height_max);
		cam_radius = Mathf.Clamp(cam_radius,cam_radius_min,cam_radius_max);
	
		cam_y = cam_height;
		cam_x = Mathf.Sin (cam_angle) * cam_radius; 
		cam_z = Mathf.Cos (cam_angle) * cam_radius;
		transform.position = new Vector3(cam_x,cam_y,cam_z);				//updates position to x,y,z calculated in other functions
	}
	
	public void CamLookCenter(){
		transform.LookAt (look_center, Vector3.up);
	}
	
	public void CamAngleLock(){
		if (cam_angle < 0) cam_angle += 360;
		if (cam_angle > 360) cam_angle -= 360;
	}
}
