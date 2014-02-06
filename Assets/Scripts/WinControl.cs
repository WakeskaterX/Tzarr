using UnityEngine;
using System.Collections;

public class WinControl : MonoBehaviour {

	public Texture win_tex;
	public Texture enter_tex;
	
	void Start () {
		
	}
	
	void Update () {
		if (Input.GetButtonDown ("Enter"))
		{
			Application.LoadLevel ("TitleScreen");
		}
	}
	
	void OnGUI(){
		GUI.DrawTexture(new Rect(Screen.width/2-256,Screen.height/2-256,512,512),win_tex);
		GUI.DrawTexture(new Rect(Screen.width/2-256,Screen.height-108,512,128),enter_tex);
	}
}
