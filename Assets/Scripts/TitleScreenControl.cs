using UnityEngine;
using System.Collections;

public class TitleScreenControl : MonoBehaviour {

	public Texture title_tex;
	public Texture enter_tex;
	public Texture left_object;
	public Texture right_object;
	
	void Start () {
	
	}

	void Update () {
		if (Input.GetButtonDown ("Enter"))
		{
			Application.LoadLevel ("TestScene");
		}
	}
	
	void OnGUI(){
		GUI.DrawTexture(new Rect(Screen.width/2-256,Screen.height/2-256,512,512),title_tex);
		GUI.DrawTexture(new Rect(Screen.width/2-256,Screen.height-108,512,128),enter_tex);
		GUI.DrawTexture (new Rect(-160,Screen.height/2-256,512,512),left_object);
		GUI.DrawTexture (new Rect(Screen.width-320,Screen.height/2-226,512,512),right_object);
	}
	
}
